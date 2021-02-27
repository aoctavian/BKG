using Backgammon.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backgammon
{
    public partial class OfflineMainForm : Form
    {
        public OfflineMainForm()
        {
            InitializeComponent();
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

        private void OfflineMainForm_Load(object sender, EventArgs e)
        {
            SetQuote();
            checkBoxGameSound.Checked = Settings.Default.GameSound;
        }

        private void OfflineMainForm_Shown(object sender, EventArgs e)
        {
            this.BringToFront();
        }

        private void StartGame()
        {
            using (var CGF = new CreateGameForm())
            {
                if (CGF.ShowDialog() == DialogResult.OK)
                {
                    this.Hide();
                    new GameFormComputer(CGF.Zone, CGF.PieceType).Show();
                }
            }
        }

        private void ButtonPlay_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void FormMenuHelp_aboutBackgammon_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Copyright Octavian Axente " + DateTime.Now.ToString("yyyy") + ".", "Backgammon");
        }

        private void CheckBoxGameSound_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.GameSound = checkBoxGameSound.Checked;
        }

        private bool IsInternetAvailable()
        {
            return DEF.InternetGetConnectedState(out int description, 0);
        }

        private void FormMenuGame_playOnline_Click(object sender, EventArgs e)
        {
            if(IsInternetAvailable())
            {
                this.Hide();
                LogInForm LIF = new LogInForm();
                LIF.FormClosed += (s, arg) => this.Close();
                LIF.Show();
            }
            else
            {
                MessageBox.Show("Internet connection not available on your device. Please connect to a network and try again.", "No internet connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormMenuGame_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
