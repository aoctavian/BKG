using Backgammon.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backgammon
{
    public partial class MainForm : Form
    {
        public class Invitation
        {
            public string Id { get; set; }
            public int Zone { get; set; }
            public int PiecesType { get; set; }
            public Invitation(string id, int zone, int piecesType)
            {
                Id = id;
                Zone = zone;
                PiecesType = piecesType;
                InvitedPlayers.Add(this);
            }
        }
        private const string 
            SOUND_RECEIVED_INVITATION = "SOUND_RECEIVED_INVITATION", 
            SOUND_CHATMESSAGE = "PLAYSOUND_CHATMESSAGE";
        private bool globalChatSound, showGlobalChat;
        private int internetConnection = DEF.CONNECTED, serverConnection;
        
        public static List<Invitation> InvitedPlayers { get; set; }

        SoundPlayer globalChatMessageSound, receivedInvitationSound;

        bool ReceivedInvitationSound { get; set; }
        bool GlobalChatSound
        {
            get { return globalChatSound; }
            set
            {
                globalChatSound = value;
                if (value)
                {
                    buttonGlobalChatSound.BackgroundImage = Resources.SoundON;
                    toolTip.SetToolTip(buttonGlobalChatSound, "Mute chat sound");
                }
                else
                {
                    buttonGlobalChatSound.BackgroundImage = Resources.SoundOFF;
                    toolTip.SetToolTip(buttonGlobalChatSound, "Unmute chat sound");
                }
            }
        }
        bool ShowGlobalChat
        {
            get { return showGlobalChat; }
            set
            {
                showGlobalChat = value;
                if (value)
                {
                    panelGlobalChat.Visible = true;
                    if (layoutPanelGlobalChatMessages.Controls.Count > 0)
                        panelGlobalChatMessages.ScrollControlIntoView(layoutPanelGlobalChatMessages.Controls[layoutPanelGlobalChatMessages.Controls.Count - 1]);
                    panelChatOptions.Location = new Point(756, 208);
                    buttonShowGlobalChat.Visible = false;
                    buttonShowGlobalChat.Text = "";
                    buttonHideGlobalChat.Visible = true;

                }
                else
                {
                    panelGlobalChat.Visible = false;
                    panelChatOptions.Location = new Point(414, 208);
                    buttonHideGlobalChat.Visible = false;
                    buttonShowGlobalChat.Text = "";
                    buttonShowGlobalChat.Visible = true;
                }
            }
        }
        internal int InternetConnection
        {
            get { return internetConnection; }
            set
            {
                internetConnection = value;
                switch (internetConnection)
                {
                    case DEF.CONNECTED:
                        InternetRecovered();
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
                switch(serverConnection)
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
        private int PlayerType { get; set; }

        private void NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            //Console.WriteLine("InternetConnection= " + InternetConnection);
            if (e.IsAvailable)
                InternetConnection = DEF.CONNECTED;
            else
                InternetConnection = DEF.NOT_CONNECTED;
        }

        public MainForm(int playerType)
        {
            PlayerType = playerType;
            InitializeComponent();
            NetworkChange.NetworkAvailabilityChanged += NetworkAvailabilityChanged;
            InvitedPlayers = new List<Invitation>();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.S)
                {
                    if (PlayerType == DEF.TYPE_PLAYER)
                        StatusFormShow();
                }
                else if (e.KeyCode == Keys.P)
                {
                    PreferencesFormShow();
                }
                else if (e.KeyCode == Keys.N)
                {
                    ChangeNameDialog();
                }
            }
        }

        private void SetQuote()
        {
            string allText = Resources.Quotes;
            string[] quotes = allText.Split('\n');
            Thread.Sleep(2); //will make sure the Random is based on a other time and will give a unique value
            int r = new Random().Next(quotes.Length);
            labelQuote.Text = quotes[r].Substring(0, quotes[r].IndexOf('-'));
            labelAuthorQuote.Text = " - " + quotes[r].Substring(quotes[r].IndexOf('-') + 1).Trim() + " -";

            labelAuthorQuote.Location = new Point(labelQuote.Location.X, labelQuote.Location.Y + labelQuote.Height + 10);
            labelAuthorQuote.Width = labelQuote.Width;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SendToServer(DEF.LOGGED_IN);
            DEF.NET.MainForm = this;
            ServerConnection = DEF.CONNECTED;

            switch (PlayerType)
            {
                case DEF.TYPE_PLAYER:
                    MyStatus.GetMyStatus();
                    break;
                case DEF.TYPE_GUEST:
                    formMenuStatus.Visible = false;
                    formMenuOptions_changePassword.Visible = false;
                    break;
            }

            SetQuote();

            ReceivedInvitationSound = Settings.Default.AppSounds;
            GlobalChatSound = Settings.Default.GlobalChatSound;
            globalChatMessageSound = new SoundPlayer(Resources.globalChatMessage);
            receivedInvitationSound = new SoundPlayer(Resources.invitation);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            this.BringToFront();
            this.ActiveControl = null;
            WelcomeMessage("*Hello " + Settings.Default.Name);
            WelcomeMessage("*Welcome to BACKGAMMON");
        }

        private void WelcomeMessage(string message)
        {
            layoutPanelGlobalChatMessages.RowCount++;
            RowStyle rs = new RowStyle(SizeType.AutoSize);
            layoutPanelGlobalChatMessages.RowStyles.Add(rs);
            Label lb = MessageLabel(message, Color.DarkBlue);
            layoutPanelGlobalChatMessages.Controls.Add(lb, 0, layoutPanelGlobalChatMessages.RowCount - 1);
            panelGlobalChatMessages.ScrollControlIntoView(lb);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GameForm GF = Application.OpenForms.OfType<GameForm>().SingleOrDefault();
            if (GF != null)
            {
                GF.BringToFront();
                GF.exitFromMain = true;
                GF.Close();

                GF = Application.OpenForms.OfType<GameForm>().SingleOrDefault();
                if (GF != null)
                {
                    GF.exitFromMain = false;
                    e.Cancel = true;
                }
            }
            Settings.Default.Save();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<GameForm>().SingleOrDefault() == null && Application.OpenForms.OfType<GameFormComputer>().SingleOrDefault() == null)
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    timerMinimized.Start();
                }
                else if (this.WindowState == FormWindowState.Normal)
                {
                    if (timerMinimized.Enabled)
                        timerMinimized.Stop();
                    else
                        //TODO WAKE UP FAILED -> Unable to read connection stream
                        SendToServer(DEF.ONLINE);
                }
            }
        }

        private void TimerMinimized_Tick(object sender, EventArgs e)
        {
            timerMinimized.Stop();
            SetAvailability(DEF.AWAY);
        }

        private void PlaySound(string sound)
        {
            //if(!this.Focused)
                ExtensionMethods.FlashNotification(this);
            if (sound == SOUND_RECEIVED_INVITATION && ReceivedInvitationSound)
                receivedInvitationSound.Play();
            else if (sound == SOUND_CHATMESSAGE && GlobalChatSound)
                globalChatMessageSound.Play();
        }

        #region SERVER <-> CLIENT COMMUNICATION ---------------------------------------------------------------------------------------------------
        private void SendToServer(string send)
        {
            //Console.WriteLine(send);
            DEF.NET.STW.WriteLine(Crypto.Encrypt(send));
        }
        #endregion

        #region SERVER CONNECTION
        public async void StartClient()
        {
            TcpClient client = new TcpClient();
            try
            {
                await client.ConnectAsync(Dns.GetHostAddresses(DEF.SERVER), DEF.PORT_SERVER);
                Console.WriteLine("----- CONNECTED TO SERVER -----");
                ServerConnection = DEF.CONNECTED;
                //SetAvailability(DEF.ONLINE);
                DEF.NET = new NET(client, this);
            }
            catch (Exception)
            {
                ServerConnection = DEF.NOT_CONNECTED;
            }
        }

        private void GameFunctionsEnabled(bool value)
        {
            formMenuOptions.Enabled = value;
            formMenuStatus.Enabled = value;
            dataGridViewOnlinePlayers.Visible = value;
            if (!value)
                dataGridViewOnlinePlayers.Clear();
            else
                GetOnlinePlayers();
            panelGlobalChat.Visible = value;
            panelChatOptions.Visible = value;
        }

        private void ServerError()
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                pictureBoxSignal.Visible = false;
                pictureBoxServer.Visible = true;
                labelErrorConnection.Text = "Cannot connect to server\nServer down";
                iconRetry.Visible = true;
                labelRetry.Visible = true;
                ConnectionError();
            }));
        }

        private void InternetError()
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                //DEF.NET.Client.Close(); DOESN'T WORK

                pictureBoxServer.Visible = false;
                pictureBoxSignal.Visible = true;
                labelErrorConnection.Text = "Disconnected from server\nPlease check your internet connection";
                iconRetry.Visible = false;
                labelRetry.Visible = false;
                ConnectionError();
            }));
        }

        private void ConnectionError()
        {
            GameFunctionsEnabled(false);
            panelErrorConnection.Visible = true;
            panelLoading.Visible = false;

            if (DEF.NET.GameForm != null)
                DEF.NET.GameForm.ConnectionError();
        }

        private void InternetRecovered()
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                RetryConnection();
            }));
        }

        private void ServerConnected()
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                panelErrorConnection.Visible = false;
                GameFunctionsEnabled(true);
            }));
        }

        public void RetryConnection()
        {
            labelConnecting.Visible = true;
            panelLoading.Visible = true;
            StartClient();
        }

        private void RetryToConnect_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                RetryConnection();
            }
        }

        private void RetryToConnect_MouseEnter(object sender, EventArgs e)
        {
            panelErrorConnection.BackColor = Color.White;
        }

        private void RetryToConnect_MouseLeave(object sender, EventArgs e)
        {
            panelErrorConnection.BackColor = Color.WhiteSmoke;
        }
        #endregion

        public void GetOnlinePlayers()
        {
            labelConnecting.Visible = false;
            panelLoading.Visible = true;
            SendToServer(DEF.GET_ONLINE_PLAYERS);
        }

        public void ShowOnlinePlayers(string onPlayers)
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                string[] players = onPlayers.Split('.').Skip(1).ToArray();
                for (int i = 0; i < players.Length; i++)
                {
                    string[] s = players[i].Split('*');
                    if (s[0] != "")
                    {
                        dataGridViewOnlinePlayers.Add(s[0], s[1], s[2]);
                    }
                }
                panelLoading.Visible = false;
            }));
        }

        public void NewOnlinePlayer(string id, string name)
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                dataGridViewOnlinePlayers.Add(id, name, DEF.ONLINE);
            }));
        }

        public void PlayerLeavingGame(string id)
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                dataGridViewOnlinePlayers.Remove(id);
            }));
        }

        public void SetOnline()
        {
            buttonPlayAgainstComputer.Visible = true;
            dataGridViewOnlinePlayers.Me_ONLINE();
        }

        public void SetAvailability(string availability)
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                if (availability == DEF.ONLINE)
                {
                    SetOnline();
                    SendToServer(DEF.ONLINE);
                }
                else if (availability == DEF.PLAYING)
                {
                    buttonPlayAgainstComputer.Visible = false;
                    dataGridViewOnlinePlayers.Me_PLAYING();
                    SendToServer(DEF.PLAYING);
                }
                else if (availability == DEF.AWAY)
                {
                    dataGridViewOnlinePlayers.Me_AWAY();
                    InvitedPlayers.Clear();
                    SendToServer(DEF.AWAY);
                }
            }));
        }

        public void PlayerChangedAvailability(string id, string availability)
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                dataGridViewOnlinePlayers.ChangeAvailability(id, availability);
            }));
        }

        public void PlayerChangedName(string id, string name)
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                dataGridViewOnlinePlayers.ChangeName(id, name);
            }));
        }

        public bool SendInvitation(string id)
        {
            return CreateGame(id);
        }

        private bool CreateGame(string opponentId)
        {
            using (var CGF = new CreateGameForm())
            {
                if (CGF.ShowDialog() == DialogResult.OK)
                {
                    if (dataGridViewOnlinePlayers.CheckCanPlay(opponentId))
                    {
                        int zone = CGF.Zone;
                        int piecesType = CGF.PieceType;

                        int z = zone;
                        int pt = piecesType;
                        if (z <= 2)
                            z += 2;
                        else
                            z -= 2;
                        if (pt == 1)
                            pt += 1;
                        else
                            pt -= 1;

                        InvitedPlayers.Add(new Invitation(opponentId, zone, piecesType));
                        SendToServer($"{DEF.INVITATION}.{opponentId}.{z}.{pt}");
                        return true;
                    }
                }
            }
            return false;
        }

        public void InvitedBy(string id, int zone, int piecesType)
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                dataGridViewOnlinePlayers.ReceivedInvitation(id, zone, piecesType);
                PlaySound(SOUND_RECEIVED_INVITATION);
            }));
        }

        public void SendCancelInvitation(string id)
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                InvitedPlayers.Remove(InvitedPlayers.Find(p => p.Id == id));
                SendToServer($"{DEF.CANCEL_INVITATION}.{id}");
            }));
        }

        public void CancelInvitation(string id)
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                dataGridViewOnlinePlayers.CancelInvitation(id);
            }));
        }

        public void START_GAME(string id)
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                SetAvailability(DEF.PLAYING);
                Invitation inv = InvitedPlayers.Find(p => p.Id == id);
                new GameForm(inv.Zone, inv.PiecesType).Show();
                InvitedPlayers.Clear();
            }));
        }

        public void START_GAME(string id, int zone, int piecesType)
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                SetAvailability(DEF.PLAYING);
                SendToServer($"{DEF.STARTING_GAME}.{id}");
                new GameForm(zone, piecesType).Show();
                InvitedPlayers.Clear();
            }));
        }

        private void CreateGameAgainstComputer()
        {
            using (var CGF = new CreateGameForm())
            {
                if (CGF.ShowDialog() == DialogResult.OK)
                {
                    new GameFormComputer(CGF.Zone, CGF.PieceType).Show();
                    SetAvailability(DEF.PLAYING);
                    InvitedPlayers.Clear();
                }
            }
        }

        //int clicks = 0;
        private void ButtonPlayAgainstComputer_Click(object sender, EventArgs e)
        {
            CreateGameAgainstComputer();
            //clicks++;
            //if (clicks == 3)
            //    MessageBox.Show("Unde crezi ca intri?", "Nu ma mai apasa", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //else if (clicks == 6)
            //    MessageBox.Show("Tu n-auzi ca nu te las inca?", "Mai ho", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //else if (clicks == 8)
            //    MessageBox.Show("M-ai innebunit cu atatea click-uri. Mai opreste-te", "Esti groaznic", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //else if (clicks == 12)
            //    MessageBox.Show("Gata! Pana aici a fost cu prietenia noastra...\nAdios amigos", "Pffu", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        #region FORM ACTIONS
        private void FormMenuGame_preferences_Click(object sender, EventArgs e)
        {
            PreferencesFormShow();
        }

        private void PreferencesFormShow()
        {
            using (var PF = new PreferencesForm())
            {
                if (PF.ShowDialog() == DialogResult.OK)
                {
                    ReceivedInvitationSound = Settings.Default.AppSounds;
                    GlobalChatSound = Settings.Default.GlobalChatSound;
                }
            }
        }

        private void FormMenuGame_signOut_Click(object sender, EventArgs e)
        {
            Settings.Default.Id = 0;
            Settings.Default.Name = "";
            Settings.Default.User = "";
            Settings.Default.Password = "";
            Settings.Default.Save();
            this.Close();
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "Backgammon.exe");
        }

        private void FormMenuGame_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChangeNameDialog()
        {
            using (var CNF = new ChangeNameForm())
            {
                if (CNF.ShowDialog() == DialogResult.OK)
                {
                    ChangeName(CNF.Name);
                }
            }
        }

        private void FormMenuOptions_changeName_Click(object sender, EventArgs e)
        {
            ChangeNameDialog();
        }

        private async void ChangeName(string name)
        {
            Settings.Default.Name = name;
            Settings.Default.Save();
            layoutPanelGlobalChatMessages.Controls[0].Text = "*Hello " + Settings.Default.Name;
            if (PlayerType == DEF.TYPE_PLAYER)
            {
                await DEF.connection.OpenAsync();
                MySqlCommand command = DEF.connection.CreateCommand();
                command.CommandText = "UPDATE accounts SET Name = '" + Settings.Default.Name + "' WHERE Id = " + Settings.Default.Id;
                await command.ExecuteNonQueryAsync();
                DEF.connection.Close();
            }
            SendToServer($"{DEF.NEW_NAME}.{Settings.Default.Name}");
        }

        private void FormMenuOptions_changePassword_Click(object sender, EventArgs e)
        {
            using (var CPF = new ChangePasswordForm())
            {
                if (CPF.ShowDialog() == DialogResult.OK)
                {
                    ChangePassword(CPF.Password);
                    MessageBox.Show("Password changed successfully", "Password changed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private async void ChangePassword(string password)
        {
            Settings.Default.Password = password;
            Settings.Default.Save();

            await DEF.connection.OpenAsync();
            MySqlCommand command = DEF.connection.CreateCommand();
            command.CommandText = "UPDATE accounts SET Password = '" + Settings.Default.Password + "' WHERE Id = " + Settings.Default.Id;
            await command.ExecuteNonQueryAsync();
            DEF.connection.Close();
        }

        private async void ResetScoreAndMatchesInDB()
        {
            MyStatus.Wins = 0;
            MyStatus.WinningStreak = 0;
            MyStatus.LongestWinningStreak = 0;
            MyStatus.Loses = 0;
            MyStatus.Abandons = 0;
            MyStatus.MatchesList.Clear();

            await DEF.connection.OpenAsync();
            MySqlCommand command = DEF.connection.CreateCommand();
            command.CommandText = "update usersscore set Wins = 0, WinningStreak = 0, LongestWinningStreak = 0, Loses = 0, Abandons = 0 where IdUser = '" + Settings.Default.Id + "'";
            await command.ExecuteNonQueryAsync();

            command.CommandText = "delete from usersmatches where IdUser = '" + Settings.Default.Id + "'";
            await command.ExecuteNonQueryAsync();
            DEF.connection.Close();
        }

        private void StatusFormShow()
        {
            new StatusForm().ShowDialog();
        }

        private void FormMenuShowStatus_Click(object sender, EventArgs e)
        {
            StatusFormShow();
        }

        private void FormMenuStatus_resetYourStatus_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to reset your status?", "Reset status", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
            {
                ResetScoreAndMatchesInDB();
                MessageBox.Show("Status clear. Good luck this time!", "Fresh start", MessageBoxButtons.OK);
            }
        }

        private void FormMenuHelp_aboutBackgammon_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Copyright Octavian Axente {DateTime.Now.ToString("yyyy")}.\nVersion: {Assembly.GetExecutingAssembly().GetName().Version}", "Backgammon");
        }

        private void CreateUpdaterBAT()
        {
            string batFilePath = AppDomain.CurrentDomain.BaseDirectory + "BackgammonUpdater.bat";
            string appSetupPath = AppDomain.CurrentDomain.BaseDirectory + "BackgammonSetup.msi";
            appSetupPath = appSetupPath.Replace('/', '\\');
            string[] lines =
            {
                    "@echo off",
                    "echo Downloading setup file...",
                    "powershell -command \"& { $client = new-object System.Net.WebClient;$client.DownloadFile(\\\"https://drive.google.com/uc?export=download\"&\"id=1zzGSWNhEhkR5t26QpU2KX4Ch4m3QwcWi\\\",\\\"" + appSetupPath + "\\\")}\"",
                    $"start BackgammonSetup.msi",
                    "del BackgammonUpdater.bat"
                };
            File.WriteAllLines(batFilePath, lines);
        }

        public void CheckForUpdate(string appVersion)
        {
            if(Assembly.GetExecutingAssembly().GetName().Version.ToString() != appVersion)
            {
                CreateUpdaterBAT();
                formMenuUpdate.Visible = true;
            }
            else
            {
                string appSetupPath = AppDomain.CurrentDomain.BaseDirectory + "BackgammonSetup.msi";
                if (File.Exists(appSetupPath))
                    File.Delete(appSetupPath);
            }
        }

        private void FormMenuUpdate_Click(object sender, EventArgs e)
        {
            this.Close();
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "BackgammonUpdater.bat");
        }
        #endregion

        #region GLOBAL CHAT
        private void ChatMessageBox_Enter(object sender, EventArgs e)
        {
            if (chatMessageBox.Text == "-> Message" && chatMessageBox.ForeColor == Color.DarkGray)
            {
                chatMessageBox.Text = "";
                chatMessageBox.ForeColor = SystemColors.WindowText;
            }
        }

        private void ChatMessageBox_Leave(object sender, EventArgs e)
        {
            if (chatMessageBox.Text.Length == 0 || string.IsNullOrWhiteSpace(chatMessageBox.Text))
            {
                chatMessageBox.ForeColor = Color.DarkGray;
                chatMessageBox.Text = "-> Message";
            }
        }

        private void ChatMessageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (!string.IsNullOrWhiteSpace(chatMessageBox.Text))
                    GlobalChatSendMessage(chatMessageBox.Text.Trim());
                chatMessageBox.Text = "";
            }
        }

        private void ChatMessageBox_TextChanged(object sender, EventArgs e)
        {
            //if (chatMessageBox.Text.Length == 1)
            //    if (char.IsLetter(Convert.ToChar(chatMessageBox.Text)))
            //    {
            //        chatMessageBox.Text = chatMessageBox.Text.ToUpper();
            //        chatMessageBox.SelectionStart = 2;
            //    }
        }

        private void ButtonShowGlobalChat_Click(object sender, EventArgs e)
        {
            ShowGlobalChat = true;
        }

        private void ButtonHideGlobalChat_Click(object sender, EventArgs e)
        {
            ShowGlobalChat = false;
        }

        private void ButtonGlobalChatSound_Click(object sender, EventArgs e)
        {
            GlobalChatSound = !GlobalChatSound;
            Settings.Default.GlobalChatSound = GlobalChatSound;
        }

        public void GlobalChatNewMessageReceived(string id, string name, string message)
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                PlaySound(SOUND_CHATMESSAGE);

                layoutPanelGlobalChatMessages.RowCount++;
                RowStyle rs = new RowStyle(SizeType.AutoSize);
                layoutPanelGlobalChatMessages.RowStyles.Add(rs);
                Label lb = MessageLabel("->" + name + ": " + message, DataGridViewOnlinePlayers.PlayerColor(id));
                layoutPanelGlobalChatMessages.Controls.Add(lb, 0, layoutPanelGlobalChatMessages.RowCount - 1);
                panelGlobalChatMessages.ScrollControlIntoView(lb);

                if (!ShowGlobalChat)
                {
                    if (buttonShowGlobalChat.Text == "")
                        buttonShowGlobalChat.Text = "1";
                    else
                        buttonShowGlobalChat.Text = (Convert.ToInt32(buttonShowGlobalChat.Text) + 1).ToString();
                }
            }));
        }

        public void GlobalChatSendMessage(string message)
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                SendToServer($"{DEF.GLOBAL_CHAT_MESSAGE}.{message}");
                layoutPanelGlobalChatMessages.RowCount++;
                RowStyle rs = new RowStyle(SizeType.AutoSize);
                layoutPanelGlobalChatMessages.RowStyles.Add(rs);
                Label lb = MessageLabel("->ME: " + message, SystemColors.WindowText);
                layoutPanelGlobalChatMessages.Controls.Add(lb, 0, layoutPanelGlobalChatMessages.RowCount - 1);
                panelGlobalChatMessages.ScrollControlIntoView(lb);
            }));
        }

        private Label MessageLabel(string message, Color color)
        {
            Label l = new Label
            {
                Font = new Font("Segoe UI", 9.75F),
                Text = message,
                BackColor = Color.White,
                ForeColor = color,
                Padding = new Padding(5, 0, 5, 4),
                Margin = new Padding(2, 1, 2, 0),
                AutoSize = true,
                MaximumSize = new Size(334, int.MaxValue),
                Dock = DockStyle.Left
            };
            return l;
        }
        #endregion

        #region TEST
        private void Button1_Click(object sender, EventArgs e)
        {
            //InternetConnection = DEF.NOT_CONNECTED;
            SendToServer("Agtrsg tchs t hc");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            InternetConnection = DEF.CONNECTED;
        }
        #endregion
    }
}