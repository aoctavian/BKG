using Backgammon.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backgammon
{
    public partial class ChangePasswordForm : Form
    {
        public string Password { get; set; }
        public ChangePasswordForm()
        {
            InitializeComponent();
        }

        private void ChangePassword()
        {
            if (!string.IsNullOrEmpty(textBoxOldPassword.Text) && !string.IsNullOrEmpty(textBoxNewPassword.Text) && !string.IsNullOrEmpty(textBoxNewPasswordConfirm.Text))
            {
                if (textBoxOldPassword.Text == Settings.Default.Password)
                {
                    if (textBoxNewPassword.Text == textBoxNewPasswordConfirm.Text)
                    {
                        Password = textBoxNewPassword.Text;
                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                        MessageBox.Show("Passwords don't match.", "New password error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Old password not correct.", "Incorrect password", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Please fill all fields before changing password.", "Fields empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ButtonChangePassword_Click(object sender, EventArgs e)
        {
            ChangePassword();
        }

        private void ChangePasswordForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChangePassword();
            }
        }
    }
}
