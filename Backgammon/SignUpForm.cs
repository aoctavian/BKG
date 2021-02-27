using Backgammon.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;

namespace Backgammon
{
    public partial class SignUpForm : Form
    {
        public string User { get; set; }
        public string Password { get; set; }

        LogInForm LIF;
        MySqlCommand command;
        List<string> usersList;

        public SignUpForm(LogInForm LIF)
        {
            this.LIF = LIF;
            InitializeComponent();
            usersList = new List<string>();
        }

        private void SignUpForm_Load(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                DEF.connection.Open();
                command = new MySqlCommand("select User from accounts", DEF.connection);
                MySqlDataReader DR = command.ExecuteReader();
                while (DR.Read())
                {
                    usersList.Add(DR[0].ToString());
                }
                DEF.connection.Close();
            }).Start();
        }

        private void SignUpForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SignUp();
            }
        }

        private void TextBoxUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void TextBoxUser_TextChanged(object sender, EventArgs e)
        {
            userCheck.Visible = true;
            foreach (string user in usersList)
            {
                if (textBoxUser.Text == user)
                {
                    userCheck.Image = Resources.ic_wrong;
                    userCheck.Tag = 0;

                    return;
                }

            }
            userCheck.Image = Resources.ic_good;
            userCheck.Tag = 1;
        }

        private async void SignUp()
        {
            if (!string.IsNullOrEmpty(textBoxName.Text) && !string.IsNullOrEmpty(textBoxEmail.Text))
            {
                if (!string.IsNullOrWhiteSpace(textBoxUser.Text) && Convert.ToInt32(userCheck.Tag) == 1)
                {
                    if (!string.IsNullOrWhiteSpace(textBoxPassword.Text))
                    {
                        if (textBoxPassword.Text == textBoxConfirmPassword.Text)
                        {
                            if (LIF.ServerConnection == DEF.CONNECTED && LIF.InternetConnection == DEF.CONNECTED)
                            {
                                this.UseWaitCursor = true;
                                string password = Crypto.Encrypt(textBoxPassword.Text);
                                await DEF.connection.OpenAsync();
                                command = DEF.connection.CreateCommand();
                                command.CommandText = "insert into accounts values (null,'" + textBoxUser.Text + "','" + password + "','" + textBoxName.Text.Trim() + "','" + textBoxEmail.Text.Trim() + "')";
                                await command.ExecuteNonQueryAsync();

                                int id;
                                command = new MySqlCommand("select Id from accounts where User = '" + textBoxUser.Text + "' AND Password = '" + password + "'", DEF.connection);
                                DbDataReader DR = await command.ExecuteReaderAsync();
                                if (DR.Read())
                                {
                                    id = Convert.ToInt32(DR[0]);
                                    DR.Close();
                                    command.CommandText = "insert into usersscore values (" + id + ", 0, 0, 0, 0, 0)";
                                    await command.ExecuteNonQueryAsync();
                                }
                                DEF.connection.Close();

                                User = textBoxUser.Text;
                                Password = textBoxPassword.Text;

                                DialogResult = DialogResult.OK;

                                this.UseWaitCursor = false;
                                this.Close();
                            }
                            else
                            {
                                if (LIF.InternetConnection == DEF.NOT_CONNECTED)
                                    MessageBox.Show("Internet connection error. Please check your internet connection and try again.", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                else if (LIF.ServerConnection == DEF.NOT_CONNECTED)
                                    MessageBox.Show("Connection to server lost. Server is offline for the moment.", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Passwords don't match.", "Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Password field is empty. Please choose a password", "Password empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please verify again the user.", "Error user", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please fill al fields.", "Cannot create account", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonSignUp_Click(object sender, EventArgs e)
        {
            SignUp();
        }
    }
}
