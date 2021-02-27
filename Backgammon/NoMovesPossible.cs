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
    public partial class NoMovesPossible : Form
    {
        GameForm GameForm;

        public NoMovesPossible(GameForm GF)
        {
            InitializeComponent();
            GameForm = GF;
        }

        private void NoMovesPossible_Shown(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void NoMovesPossible_Load(object sender, EventArgs e)
        {
            int GFx = GameForm.Location.X;
            int GFy = GameForm.Location.Y;
            int GFw = GameForm.ClientSize.Width - 250 - 85;
            int GFh = GameForm.ClientSize.Height - 45;
            
            int X = GFw / 2 + 85 + GFx + 13;
            int Y = GFh / 2 + 45 + GFy + 2;
            
            if (GameForm.GAME_TYPE == 0)
                X += GFw / 14;

            this.Location = new Point(X, Y);
        }
    }
}
