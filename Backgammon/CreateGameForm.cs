using Backgammon.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backgammon
{
    public partial class CreateGameForm : Form
    {
        public int Zone { get; set; }
        public int PieceType { get; set; }

        public CreateGameForm()
        {
            InitializeComponent();
        }

        private void CreateGameForm_Load(object sender, EventArgs e)
        {
            switch(Settings.Default.LastGameZone)
            {
                case 1: Zone1.Image = Resources.zoneSelected; Zone = Convert.ToInt32(Zone1.Tag); break;
                case 2: Zone2.Image = Resources.zoneSelected; Zone = Convert.ToInt32(Zone2.Tag); break;
                case 3: Zone3.Image = Resources.zoneSelected; Zone = Convert.ToInt32(Zone3.Tag); break;
                case 4: Zone4.Image = Resources.zoneSelected; Zone = Convert.ToInt32(Zone4.Tag); break;
            }

            switch(Settings.Default.LastGamePiecesType)
            {
                case 1: piece1.BorderStyle = BorderStyle.FixedSingle; PieceType = Convert.ToInt32(piece1.Tag); break;
                case 2: piece2.BorderStyle = BorderStyle.FixedSingle; PieceType = Convert.ToInt32(piece2.Tag); break;
            }
        }

        private void Zone_Click(object sender, EventArgs e)
        {
            PictureBox zoneClicked = (PictureBox)sender;
            MouseEventArgs me = (MouseEventArgs)e;
            if(me.Button == MouseButtons.Left)
            {
                Zone1.Image = null;
                Zone2.Image = null;
                Zone3.Image = null;
                Zone4.Image = null;

                zoneClicked.Image = Resources.zoneSelected;
                Zone = Convert.ToInt32(zoneClicked.Tag);
            }
        }

        private void Piece_Click(object sender, EventArgs e)
        {
            PictureBox pieceClicked = (PictureBox)sender;
            MouseEventArgs me = (MouseEventArgs)e;
            if(me.Button == MouseButtons.Left)
            {
                piece1.BorderStyle = BorderStyle.None;
                piece2.BorderStyle = BorderStyle.None;

                pieceClicked.BorderStyle = BorderStyle.FixedSingle;
                PieceType = Convert.ToInt32(pieceClicked.Tag);
            }
        }

        private void Zone_MouseEnter(object sender, EventArgs e)
        {
            ((PictureBox)sender).Image = Resources.zoneHover;
        }

        private void Zone_MouseLeave(object sender, EventArgs e)
        {
            if (Convert.ToInt32(((PictureBox)sender).Tag) == Zone)
                ((PictureBox)sender).Image = Resources.zoneSelected;
            else
                ((PictureBox)sender).Image = null;
        }

        private void ButtonPlay_Click(object sender, EventArgs e)
        {
            Settings.Default.LastGameZone = Zone;
            Settings.Default.LastGamePiecesType = PieceType;
            Settings.Default.Save();
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}