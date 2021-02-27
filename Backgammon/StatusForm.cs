using Backgammon.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Backgammon
{
    public partial class StatusForm : Form
    {
        private const string WIN = "WIN", LOSE = "LOSE", ABANDON = "ABANDON";
        private int dataGridViewMatchesHeight;

        public StatusForm()
        {
            InitializeComponent();
            dataGridViewMatchesHeight = dataGridViewAllGames.Height;
        }

        private void StatusForm_Load(object sender, EventArgs e)
        {
            labelWins.Text += MyStatus.Wins.ToString();
            labelWinningStreak.Text += MyStatus.WinningStreak.ToString(); ;
            labelLongestWinningStreak.Text += MyStatus.LongestWinningStreak.ToString();
            labelLoses.Text += MyStatus.Loses.ToString();
            labelAbandons.Text += MyStatus.Abandons.ToString();
            labelAllGames.Text += "(" + MyStatus.MatchesList.Count + ")";
            SetDataGridView(MyStatus.MatchesList);
        }

        private void StatusForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.Height - this.MinimumSize.Height > 0)
                dataGridViewAllGames.Height = dataGridViewMatchesHeight + this.Height - this.MinimumSize.Height;
        }

        private void StatusForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void SetDataGridView(List<MyStatus.Match> MatchesList)
        {
            dataGridViewAllGames.DataSource = MatchesList;
            dataGridViewAllGames.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewAllGames.Columns[0].HeaderText = "Date | Time (end game)";
            dataGridViewAllGames.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void ButtonFilters_Click(object sender, EventArgs e)
        {
            if (!panelFilters.Visible)
            {
                dataGridViewAllGames.Location = new Point(dataGridViewAllGames.Location.X, dataGridViewAllGames.Location.Y + 45);
                this.MinimumSize = new Size(this.MinimumSize.Width, this.MinimumSize.Height + 45);
                panelFilters.Visible = true;
            }
        }

        private void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            FilterMatches();
        }

        private void FilterMatches()
        {
            List<MyStatus.Match> matches = new List<MyStatus.Match>(MyStatus.MatchesList);
            
            if (dateTimePicker.Checked)
                matches = matches.FindAll(m => m.Date.Contains(dateTimePicker.Value.ToString("d MMM yyyy")));
            
            if (textBoxFilterOpponent.Text.Length > 0 && textBoxFilterOpponent.ForeColor == SystemColors.WindowText)
                matches = matches.FindAll(m => m.Opponent.ToLower().Contains(textBoxFilterOpponent.Text));
            
            if (radioButtonWins.Checked)
                matches = matches.FindAll(m => m.Result == WIN);
            else if (radioButtonLoses.Checked)
                matches = matches.FindAll(m => m.Result == LOSE);
            else if (radioButtonAbandons.Checked)
                matches = matches.FindAll(m => m.Result == ABANDON);

            SetDataGridView(matches);
        }

        private void TextBoxFilterOpponent_TextChanged(object sender, EventArgs e)
        {
            if (textBoxFilterOpponent.Text != "Find Opponent" && textBoxFilterOpponent.Text.Length > 0)
            {
                textBoxFilterOpponent.ForeColor = SystemColors.WindowText;
                FilterMatches();
            }
            else if (textBoxFilterOpponent.Text.Length == 0)
            {
                FilterMatches();
            }
        }

        private void TextBoxFilterOpponent_Enter(object sender, EventArgs e)
        {
            if (textBoxFilterOpponent.Text == "Find Opponent" && textBoxFilterOpponent.ForeColor == SystemColors.ControlDark)
            {
                textBoxFilterOpponent.Text = "";
                textBoxFilterOpponent.ForeColor = SystemColors.WindowText;
            }
        }

        private void TextBoxFilterOpponent_Leave(object sender, EventArgs e)
        {
            if (textBoxFilterOpponent.Text.Length == 0)
            {
                textBoxFilterOpponent.ForeColor = SystemColors.ControlDark;
                textBoxFilterOpponent.Text = "Find Opponent";
            }
        }

        private void RadioButtonResult_Click(object sender, EventArgs e)
        {
            FilterMatches();
        }

        private void TextBoxFilterOpponent_Click(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
    }
}
