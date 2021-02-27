using Backgammon.Properties;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backgammon
{
    public partial class LogInForm : Form
    {
        private int internetConnection, serverConnection;

        internal int InternetConnection
        {
            get { return internetConnection; }
            set
            {
                internetConnection = value;
                switch (internetConnection)
                {
                    case DEF.CONNECTED:
                        InternetConnected();
                        break;
                    case DEF.NOT_CONNECTED:
                        InternetError();
                        break;
                }
            }
        }
        internal int ServerConnection
        {
            get { return serverConnection; }
            set
            {
                serverConnection = value;
                switch (serverConnection)
                {
                    case DEF.CONNECTED:
                        ServerConnected();
                        break;
                    case DEF.NOT_CONNECTED:
                        ServerError();
                        break;
                }
            }
        }

        private int tryToLoginTimes = 1, sec = 0;

        private bool IsInternetAvailable()
        {
            return DEF.InternetGetConnectedState(out _, 0); //out int description
        }

        //private async void CheckDatabaseAvailable(bool login = false)
        //{
        //    try
        //    {
        //        Console.WriteLine(login);
        //        await DEF.connection.OpenAsync();
        //        DEF.connection.Close();

        //        if (login)
        //            LogIn();
        //        else
        //            Error = NO_ERROR;
        //    }
        //    catch (SqlException)
        //    {
        //        Error = ERROR_SERVER;
        //    }
        //}

        private void NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            if (e.IsAvailable)
                InternetConnection = DEF.CONNECTED;
            else
                InternetConnection = DEF.NOT_CONNECTED;
        }

        public LogInForm()
        {
            InitializeComponent();
            NetworkChange.NetworkAvailabilityChanged += NetworkAvailabilityChanged;
        }

        public LogInForm(string user, string password)
        {
            InitializeComponent();
            textBoxUser.Text = user;
            textBoxPassword.Text = Crypto.Decrypt(password);
            NetworkChange.NetworkAvailabilityChanged += NetworkAvailabilityChanged;
        }

        private void LogInForm_Load(object sender, EventArgs e)
        {
            if (IsInternetAvailable())
                InternetConnection = DEF.CONNECTED; // => connect to server
            else
                InternetConnection = DEF.NOT_CONNECTED;
        }

        private void LogInForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (InternetConnection == DEF.CONNECTED && ServerConnection == DEF.CONNECTED)
                SendToServer(DEF.LEAVING_GAME);
            //DEF.NET.Client.Close();
            Thread.Sleep(500);
            Console.WriteLine("CLOSING LOGIN FORM => APP CLOSED");
        }

        private void LogIn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (InternetConnection == DEF.CONNECTED && ServerConnection == DEF.CONNECTED)
                    LogIn();
            }
        }

        //SERVER <-> CLIENT COMMUNICATION ---------------------------------------------------------------------------------------------------
        private void SendToServer(string send)
        {
            //Console.WriteLine(send);
            DEF.NET.STW.WriteLine(Crypto.Encrypt(send));
        }
        //END SERVER <-> CLIENT COMMUNICATION -----------------------------------------------------------------------------------------------

        public async void StartClient()
        {
            loading.Visible = true;
            TcpClient client = new TcpClient();
            try
            {
                await client.ConnectAsync(Dns.GetHostAddresses(DEF.SERVER), DEF.PORT_SERVER);
                Console.WriteLine("----- CONNECTED TO SERVER -----");
                ServerConnection = DEF.CONNECTED;
                DEF.NET = new NET(client);

                if (!string.IsNullOrWhiteSpace(textBoxUser.Text) && !string.IsNullOrWhiteSpace(textBoxPassword.Text))
                    LogIn();
                else
                    loading.Visible = false;
            }
            catch (Exception)
            {
                ServerConnection = DEF.NOT_CONNECTED;
            }
        }

        private void Reconnect_MouseClick(object sender, MouseEventArgs e)
        {
            StartClient();
        }

        private void InternetError()
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                if(DEF.NET != null)
                    DEF.NET.Client.Close();

                buttonGuest.Visible = false;

                pictureBoxServer.Visible = false;
                pictureBoxSignal.Visible = true;
                labelErrorConnection.Text = "Disconnected from server\nPlease check your internet connection";
                iconRetry.Visible = false;
                labelRetry.Visible = false;
                panelErrorConnection.Visible = true;

                loading.Visible = false;
            }));
        }

        private void InternetConnected()
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                StartClient();
            }));
        }

        private void ServerConnected()
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                buttonGuest.Visible = true;
                panelErrorConnection.Visible = false;
            }));
        }

        private void ServerError()
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                buttonGuest.Visible = false;

                pictureBoxSignal.Visible = false;
                pictureBoxServer.Visible = true;
                labelErrorConnection.Text = "Cannot connect to server\nServer down";
                iconRetry.Visible = true;
                labelRetry.Visible = true;
                panelErrorConnection.Visible = true;

                loading.Visible = false;
            }));
        }

        private void ButtonLogIn_Click(object sender, EventArgs e)
        {
            if (!this.UseWaitCursor)
            {
                LogIn();
            }
        }

        private void LogIn()
        {
            if (!string.IsNullOrWhiteSpace(textBoxUser.Text) && !string.IsNullOrWhiteSpace(textBoxPassword.Text))
            {
                this.UseWaitCursor = true;
                new Thread(() =>
                {
                    try
                    {
                        DEF.connection.Open();
                        MySqlCommand command = new MySqlCommand("select Id, Name from accounts where User = '" + textBoxUser.Text + "' AND Password = '" + Crypto.Encrypt(textBoxPassword.Text) + "'", DEF.connection);
                        MySqlDataReader DR = command.ExecuteReader();
                        this.UseWaitCursor = false;
                        if (DR.Read())
                        {
                            Settings.Default.User = textBoxUser.Text;
                            Settings.Default.Password = Crypto.Encrypt(textBoxPassword.Text);
                            Settings.Default.Id = Convert.ToInt32(DR[0]);
                            SendToServer($"{DEF.ID_USER}.{DR[0]}");
                            Settings.Default.Name = DR[1].ToString();
                            SendToServer($"{DEF.NAME}.{DR[1]}");
                            Settings.Default.Save();
                            DEF.connection.Close();

                            OpenBackgammon();
                        }
                        else
                        {
                            DEF.connection.Close();
                            MessageBox.Show("Incorrect user or password. Please try again.", "Invalid account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.BeginInvoke(new MethodInvoker(delegate ()
                            {
                                SendToServer($"{DEF.TRIED_TO_CONNECT}.{textBoxUser.Text}.{tryToLoginTimes}");
                                tryToLoginTimes++;
                                Settings.Default.User = "";
                                Settings.Default.Password = "";
                                Settings.Default.Save();
                                loading.Visible = false;

                                if (tryToLoginTimes > 5)
                                {
                                    BlockLogIn();
                                }
                                else
                                {
                                    textBoxUser.Focus();
                                    textBoxUser.SelectAll();
                                }
                            }));
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Cannot connect to database. Please join as Guest for the moment.", "Database down", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }).Start();
            }
        }

        private void BlockLogIn()
        {
            textBoxUser.Enabled = false;
            textBoxPassword.Enabled = false;
            buttonLogIn.Enabled = false;
            linkLabelCreateAccount.Enabled = false;
            buttonGuest.Enabled = false;
            MessageBox.Show("You are blocked to log in for 30 seconds.", "Log in failed 5 times", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            sec = 31;
            buttonLogIn.ForeColor = Color.Firebrick;

            timerCount.Start();
        }

        private void EnableLogIn()
        {
            timerCount.Stop();

            textBoxUser.Enabled = true;
            textBoxPassword.Enabled = true;
            buttonLogIn.Enabled = true;
            linkLabelCreateAccount.Enabled = true;
            buttonGuest.Enabled = true;

            tryToLoginTimes = 1;

            buttonLogIn.ForeColor = SystemColors.ControlText;
            buttonLogIn.Text = "Log In";
        }

        private void TimerCount_Tick(object sender, EventArgs e)
        {
            buttonLogIn.Text = $"{--sec} s";
            if (sec == 0)
                EnableLogIn();
        }

        private void LinkLabelCreateAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!this.UseWaitCursor)
            {
                using (var SignUpForm = new SignUpForm(this))
                {
                    if (SignUpForm.ShowDialog() == DialogResult.OK)
                    {
                        textBoxUser.Text = SignUpForm.User;
                        textBoxPassword.Text = SignUpForm.Password;
                        if (InternetConnection == DEF.CONNECTED && ServerConnection == DEF.CONNECTED)
                            LogIn();
                    }
                }
            }
        }

        private void OpenBackgammon(int playerType = 1)
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                this.Hide();
                MainForm MF = new MainForm(playerType);
                MF.FormClosed += (s, arg) => this.Close();
                MF.Show();
            }));
        }

        private void ButtonOffline_Click(object sender, EventArgs e)
        {
            if (!this.UseWaitCursor)
            {
                this.Hide();
                OfflineMainForm OMF = new OfflineMainForm();
                OMF.FormClosed += (s, arg) => this.Close();
                OMF.Show();

                if (InternetConnection == DEF.CONNECTED && ServerConnection == DEF.CONNECTED)
                {
                    SendToServer(DEF.DISCONNECT);
                    ServerConnection = DEF.NOT_CONNECTED;
                    DEF.NET = null;
                }
            }
        }

        private async void ButtonGuest_Click(object sender, EventArgs e)
        {
            if (!this.UseWaitCursor)
            {
                this.UseWaitCursor = true;
                SendToServer(DEF.GUEST);
                await Task.Delay(500);
                OpenBackgammon(DEF.TYPE_GUEST);
            }
        }

        //TEST
        private void Button1_Click(object sender, EventArgs e)
        {
            //InternetConnection = DEF.NOT_CONNECTED;
            SendToServer("AFD");
            //DEF.NET.Client.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //InternetConnection = DEF.CONNECTED;
            string s = "AFD";
            byte[] arr = Encoding.ASCII.GetBytes(s);
            foreach (byte b in arr)
                Console.Write(b + " ");
            Console.WriteLine(Encoding.ASCII.GetString(arr));
        }
    }
}
