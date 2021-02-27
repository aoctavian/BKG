using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Drawing.Text;
using System.Threading;
using System.Media;
using Backgammon.Properties;
using System.Data.SqlClient;

namespace Backgammon
{
    public partial class GameFormComputer : Form
    {
        private bool gameSound;
        private bool? myTurn;
        private PIECE selectedPiece;
        private const string WIN = "WIN", LOSE = "LOSE";
        private const string 
            SOUND_YOURTURN = "PLAYSOUNG_YOURTURN",
            SOUND_MOVEPIECE = "PLAYSOUND_MOVEPIECE",
            SOUND_ROLLDICE = "PLAYSOUND_ROLLDICE",
            SOUND_ENDGAME_WIN = "ENDGAME_WIN",
            SOUND_ENDGAME_LOST = "ENDGAME_LOST";
        private const int HOUSE_LEFT = 0, HOUSE_RIGHT = 1;
        
        public int GAME_TYPE = HOUSE_RIGHT; //used for NoPossibleMovesPanel

        PLAYER ME, COMPUTER;
        int moves;
        bool undo;
        bool DoubleDice;

        PIECE SelectedPiece
        {
            get { return selectedPiece; }
            set
            {
                selectedPiece = value;
                if (selectedPiece == null)
                {
                    foreach (Panel p in movesSuggestion)
                    {
                        if (p != null)
                            p.Controls.Clear();
                    }
                }
                else
                {
                    ShowMovesSuggestion();
                }
            }
        }
        bool? MyTurn
        {
            get { return myTurn; }
            set
            {
                myTurn = value;
                HideDice();
                switch (myTurn)
                {
                    case true:
                        undo = false;
                        buttonRollDice.Enabled = true;
                        buttonRollDice.Visible = true;
                        PlaySound(SOUND_YOURTURN);
                        ME.Panel.BorderStyle = BorderStyle.Fixed3D;
                        ME.Panel.BackColor = Color.FromArgb(154, 120, 82);
                        COMPUTER.Panel.BorderStyle = BorderStyle.None;
                        COMPUTER.Panel.BackColor = Color.FromArgb(176, 142, 106);
                        break;
                    case false:
                        buttonUndo.Visible = false;
                        buttonRollDice.Visible = false;
                        ME.Panel.BorderStyle = BorderStyle.None;
                        ME.Panel.BackColor = Color.FromArgb(176, 142, 106);
                        COMPUTER.Panel.BorderStyle = BorderStyle.Fixed3D;
                        COMPUTER.Panel.BackColor = Color.FromArgb(154, 120, 82);

                        ComputerRollDice();
                        break;
                    case null:
                        moves = 0;
                        buttonUndo.Visible = false;
                        buttonRollDice.Visible = false;
                        ME.Panel.BorderStyle = COMPUTER.Panel.BorderStyle = BorderStyle.None;
                        ME.Panel.BackColor = COMPUTER.Panel.BackColor = Color.FromArgb(176, 142, 106);
                        BlockDice();
                        break;
                }
            }
        }
        bool GameSound
        {
            get { return gameSound; }
            set
            {
                gameSound = value;
                Settings.Default.GameSound = value;
                if (gameSound)
                {
                    buttonGameSound.BackgroundImage = Resources.SoundON;
                    toolTip.SetToolTip(buttonGameSound, "Mute game sound");
                }
                else
                {
                    buttonGameSound.BackgroundImage = Resources.SoundOFF;
                    toolTip.SetToolTip(buttonGameSound, "Unmute game sound");
                }
            }
        }

        SoundPlayer rollDiceSound, movePieceSound, yourTurn, endGameWIN, endGameLOST;
        DateTime startTime;
        Random random;
        private readonly Panel[] movesSuggestion;

        #region GAME FORM COMPUTER
        public GameFormComputer(int zone, int piecesType)
        {
            InitializeComponent();

            if (zone == 1 || zone == 3)
            {
                SetTableLeft();
                movesSuggestion = new Panel[] { panelOut, panel1, panel2, panel3, panel4, panel5, panel6, null, panel8, panel9, panel10, panel11, panel12, panel13 };
            }
            else
            {
                movesSuggestion = new Panel[] { null, panel1, panel2, panel3, panel4, panel5, panel6, null, panel8, panel9, panel10, panel11, panel12, panel13, panelOut };
            }
            SetTable(piecesType, zone);
        }

        private void GameFormComputer_Load(object sender, EventArgs e)
        {
            random = new Random();
            GameSound = Settings.Default.GameSound;
            rollDiceSound = new SoundPlayer(Resources.rolldice);
            movePieceSound = new SoundPlayer(Resources.movepiece);
            yourTurn = new SoundPlayer(Resources.yourTurn);
            endGameWIN = new SoundPlayer(Resources.endgameWin);
            endGameLOST = new SoundPlayer(Resources.endgameLost);
        }

        private void GameFormComputer_Shown(object sender, EventArgs e)
        {
            startTime = DateTime.Now;
            timerGame.Start();

            MyTurn = true;
        }

        private void GameFormComputer_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to leave the game?", "Leaving the game", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if(DEF.NET != null)
                    DEF.NET.BackOnline();
                OfflineMainForm OMF = Application.OpenForms.OfType<OfflineMainForm>().SingleOrDefault();
                if (OMF != null)
                    OMF.Show();
            }
            else if (dr == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
            Settings.Default.Save();
        }

        private void GameFormComputer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (SelectedPiece != null)
                {
                    UnselectPiece();
                }
            }
            else if (e.KeyCode == Keys.F1 || (e.Control && e.KeyCode == Keys.R))
            {
                RollDice();
            }
            else if (e.KeyCode == Keys.Oem3)
            {
                ChangeTurn();
            }
        }

        private void GameFormComputer_Resize(object sender, EventArgs e)
        {

        }

        private void GameFormComputer_ResizeEnd(object sender, EventArgs e)
        {
            this.Size = new Size(this.Width - GameTable.Width % 14 + 1, this.Height - GameTable.Height % 13 + 1);
            SetLocationPanelNoMovesPossible();
        }

        private void GameFormComputer_Activated(object sender, EventArgs e)
        {
            //if (Application.OpenForms.OfType<StatusForm>().SingleOrDefault() != null)
            //    Application.OpenForms.OfType<StatusForm>().Single().Activate();
        }
        #endregion

        #region SET TABLE
        private void SetLocationPanelNoMovesPossible()
        {
            int GFw = this.ClientSize.Width - 85;
            int GFh = this.ClientSize.Height - 45;

            int cellW = GFw / 14;

            int X = 85 + 10 * cellW - 164;
            int Y = GFh / 2 + 45 - 29;

            if (GAME_TYPE == HOUSE_LEFT)
                X += cellW;

            panelNoPossibleMoves.Location = new Point(X, Y);
        }

        private void SetTable(int piecesType, int zone)
        {
            ME = new PLAYER(piecesType, zone);
            if (zone <= 2)
            {
                zone += 2;
                ME.Panel = panelPlayerInfo_1;
            }
            else
            {
                zone -= 2;
                ME.Panel = panelPlayerInfo_2;
            }
            ME.Panel.Controls[0].Text = "Me";
            switch (ME.PiecesType)
            {
                case 1: ((PictureBox)ME.Panel.Controls[1]).Image = Resources.piece1; break;
                case 2: ((PictureBox)ME.Panel.Controls[1]).Image = Resources.piece2; break;
            }

            if (piecesType == 1)
                piecesType += 1;
            else
                piecesType -= 1;

            COMPUTER = new PLAYER(piecesType, zone)
            {
                Name = "COMPUTER"
            };
            if (zone <= 2)
                COMPUTER.Panel = panelPlayerInfo_1;
            else
                COMPUTER.Panel = panelPlayerInfo_2;
            COMPUTER.Panel.Controls[0].Text = "Computer";
            switch (COMPUTER.PiecesType)
            {
                case 1: ((PictureBox)COMPUTER.Panel.Controls[1]).Image = Resources.piece1; break;
                case 2: ((PictureBox)COMPUTER.Panel.Controls[1]).Image = Resources.piece2; break;
            }

            SetPieces(ME);
            SetPieces(COMPUTER);
        }

        private void SetTableLeft()
        {
            GAME_TYPE = HOUSE_LEFT;
            SetLocationPanelNoMovesPossible();

            GameTable.BackgroundImage = Resources.gameTableBackground_Left;

            GameTable.ColumnStyles[14].SizeType = SizeType.Absolute;
            GameTable.ColumnStyles[14].Width = 0;

            GameTable.ColumnStyles[0].SizeType = SizeType.Percent;
            GameTable.ColumnStyles[0].Width = 7.142148F;

            GameTable.Controls.Add(panelOut, 0, 7);
        }

        private void SetRematch(bool turn)
        {
            panelGameOver.Visible = false;

            labelTime.Text = "00:00";
            startTime = DateTime.Now;
            timerGame.Start();

            GameTable.Controls.OfType<PIECE>().ToList().ForEach(t =>
            {
                GameTable.Remove(t);
                t.Dispose();
            });

            SetPieces(ME);
            SetPieces(COMPUTER);

            MyTurn = turn;
        }

        private void SetPieces(PLAYER PLAYER)
        {
            PLAYER.CountPieces = 0;
            switch (PLAYER.Zone)
            {
                case 1:
                    SetPieces_Zone1(PLAYER, PLAYER.PiecesType);
                    break;
                case 2:
                    SetPieces_Zone2(PLAYER, PLAYER.PiecesType);
                    break;
                case 3:
                    SetPieces_Zone3(PLAYER, PLAYER.PiecesType);
                    break;
                case 4:
                    SetPieces_Zone4(PLAYER, PLAYER.PiecesType);
                    break;
            }
        }

        private void SetPieces_Zone1(PLAYER PLAYER, int pieceType)
        {
            //SetPieces_Zone1_1(PLAYER, pieceType); return;

            PIECE piece;

            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_1";
            piece.Column = 1;
            piece.Row = 13;
            GameTable.AddPiece(piece);
            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_2";
            piece.Column = 1;
            piece.Row = 12;
            GameTable.AddPiece(piece);

            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_3";
            piece.Column = 13;
            piece.Row = 13;
            GameTable.AddPiece(piece);
            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_4";
            piece.Column = 13;
            piece.Row = 12;
            GameTable.AddPiece(piece);
            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_5";
            piece.Column = 13;
            piece.Row = 11;
            GameTable.AddPiece(piece);
            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_6";
            piece.Column = 13;
            piece.Row = 10;
            GameTable.AddPiece(piece);
            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_7";
            piece.Column = 13;
            piece.Row = 9;
            GameTable.AddPiece(piece);

            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_8";
            piece.Column = 9;
            piece.Row = 1;
            GameTable.AddPiece(piece);
            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_9";
            piece.Column = 9;
            piece.Row = 2;
            GameTable.AddPiece(piece);
            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_10";
            piece.Column = 9;
            piece.Row = 3;
            GameTable.AddPiece(piece);

            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_11";
            piece.Column = 6;
            piece.Row = 1;
            GameTable.AddPiece(piece);
            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_12";
            piece.Column = 6;
            piece.Row = 2;
            GameTable.AddPiece(piece);
            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_13";
            piece.Column = 6;
            piece.Row = 3;
            GameTable.AddPiece(piece);
            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_14";
            piece.Column = 6;
            piece.Row = 4;
            GameTable.AddPiece(piece);
            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_15";
            piece.Column = 6;
            piece.Row = 5;
            GameTable.AddPiece(piece);
        }

        private void SetPieces_Zone1_1(PLAYER PLAYER, int pieceType)
        {
            PIECE piece;

            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_1";
            piece.Column = 2;
            piece.Row = 1;
            piece.PLAYER = PLAYER;
            GameTable.AddPiece(piece);
            //piece = new PIECE1(PLAYER, pieceType);
            //piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            //piece.Paint += new PaintEventHandler(Piece_Paint);
            //piece.Name = "1_2";
            //piece.Column = 1;
            //piece.Row = 2;
            //piece.PLAYER = PLAYER;
            //GameTable.AddPiece(piece);
            //piece = new PIECE1(PLAYER, pieceType);
            //piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            //piece.Paint += new PaintEventHandler(Piece_Paint);
            //piece.Name = "1_3";
            //piece.Column = 3;
            //piece.Row = 1;
            //piece.PLAYER = PLAYER;
            //GameTable.AddPiece(piece);
            //piece = new PIECE1(PLAYER, pieceType);
            //piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            //piece.Paint += new PaintEventHandler(Piece_Paint);
            //piece.Name = "1_3";
            //piece.Column = 4;
            //piece.Row = 1;
            //piece.PLAYER = PLAYER;
            //GameTable.AddPiece(piece);
        }

        private void SetPieces_Zone2(PLAYER PLAYER, int pieceType)
        {
            //SetPieces_Zone2_1(PLAYER, pieceType); return;

            PIECE piece;

            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_1";
            piece.Column = 13;
            piece.Row = 13;
            GameTable.AddPiece(piece);
            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_2";
            piece.Column = 13;
            piece.Row = 12;
            GameTable.AddPiece(piece);

            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_3";
            piece.Column = 1;
            piece.Row = 13;
            GameTable.AddPiece(piece);
            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_4";
            piece.Column = 1;
            piece.Row = 12;
            GameTable.AddPiece(piece);
            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_5";
            piece.Column = 1;
            piece.Row = 11;
            GameTable.AddPiece(piece);
            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_6";
            piece.Column = 1;
            piece.Row = 10;
            GameTable.AddPiece(piece);
            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_7";
            piece.Column = 1;
            piece.Row = 9;
            GameTable.AddPiece(piece);

            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_8";
            piece.Column = 5;
            piece.Row = 1;
            GameTable.AddPiece(piece);
            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_9";
            piece.Column = 5;
            piece.Row = 2;
            GameTable.AddPiece(piece);
            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_10";
            piece.Column = 5;
            piece.Row = 3;
            GameTable.AddPiece(piece);

            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_11";
            piece.Column = 8;
            piece.Row = 1;
            GameTable.AddPiece(piece);
            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_12";
            piece.Column = 8;
            piece.Row = 2;
            GameTable.AddPiece(piece);
            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_13";
            piece.Column = 8;
            piece.Row = 3;
            GameTable.AddPiece(piece);
            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_14";
            piece.Column = 8;
            piece.Row = 4;
            GameTable.AddPiece(piece);
            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_15";
            piece.Column = 8;
            piece.Row = 5;
            GameTable.AddPiece(piece);
        }

        private void SetPieces_Zone2_1(PLAYER PLAYER, int pieceType)
        {
            PIECE piece;

            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_1";
            piece.Column = 11;
            piece.Row = 1;
            GameTable.AddPiece(piece);

            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_2";
            piece.Column = 11;
            piece.Row = 2;
            GameTable.AddPiece(piece);

            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_3";
            piece.Column = 10;
            piece.Row = 1;
            GameTable.AddPiece(piece);

            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_4";
            piece.Column = 10;
            piece.Row = 2;
            GameTable.AddPiece(piece);

            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_5";
            piece.Column = 9;
            piece.Row = 1;
            GameTable.AddPiece(piece);

            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_6";
            piece.Column = 8;
            piece.Row = 1;
            GameTable.AddPiece(piece);

            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_7";
            piece.Column = 8;
            piece.Row = 2;
            GameTable.AddPiece(piece);

            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_8";
            piece.Column = 3;
            piece.Row = 1;
            GameTable.AddPiece(piece);

            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_9";
            piece.Column = 3;
            piece.Row = 13;
            GameTable.AddPiece(piece);

            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_10";
            piece.Column = 3;
            piece.Row = 12;
            GameTable.AddPiece(piece);
        }

        private void SetPieces_Zone3(PLAYER PLAYER, int pieceType)
        {
            //SetPieces_Zone3_1(PLAYER, pieceType); return;

            PIECE piece;

            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_1";
            piece.Column = 1;
            piece.Row = 1;
            GameTable.AddPiece(piece);
            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_2";
            piece.Column = 1;
            piece.Row = 2;
            GameTable.AddPiece(piece);

            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_3";
            piece.Column = 13;
            piece.Row = 1;
            GameTable.AddPiece(piece);
            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_4";
            piece.Column = 13;
            piece.Row = 2;
            GameTable.AddPiece(piece);
            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_5";
            piece.Column = 13;
            piece.Row = 3;
            GameTable.AddPiece(piece);
            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_6";
            piece.Column = 13;
            piece.Row = 4;
            GameTable.AddPiece(piece);
            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_7";
            piece.Column = 13;
            piece.Row = 5;
            GameTable.AddPiece(piece);

            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_8";
            piece.Column = 9;
            piece.Row = 13;
            GameTable.AddPiece(piece);
            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_9";
            piece.Column = 9;
            piece.Row = 12;
            GameTable.AddPiece(piece);
            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_10";
            piece.Column = 9;
            piece.Row = 11;
            GameTable.AddPiece(piece);

            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_11";
            piece.Column = 6;
            piece.Row = 13;
            GameTable.AddPiece(piece);
            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_12";
            piece.Column = 6;
            piece.Row = 12;
            GameTable.AddPiece(piece);
            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_13";
            piece.Column = 6;
            piece.Row = 11;
            GameTable.AddPiece(piece);
            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_14";
            piece.Column = 6;
            piece.Row = 10;
            GameTable.AddPiece(piece);
            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_15";
            piece.Column = 6;
            piece.Row = 9;
            GameTable.AddPiece(piece);
        }

        private void SetPieces_Zone3_1(PLAYER PLAYER, int pieceType)
        {
            PIECE piece;

            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_1";
            piece.Column = 1;
            piece.Row = 1;
            piece.PLAYER = PLAYER;
            GameTable.AddPiece(piece);

            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_1";
            piece.Column = 1;
            piece.Row = 2;
            piece.PLAYER = PLAYER;
            GameTable.AddPiece(piece);

            //piece = new PIECE2(PLAYER, pieceType);
            //piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            //piece.Paint += new PaintEventHandler(Piece_Paint);
            //piece.Name = "2_1";
            //piece.Column = 8;
            //piece.Row = 13;
            //piece.PLAYER = PLAYER;
            //GameTable.AddPiece(piece);

            //piece = new PIECE2(PLAYER, pieceType);
            //piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            //piece.Paint += new PaintEventHandler(Piece_Paint);
            //piece.Name = "2_1";
            //piece.Column = 5;
            //piece.Row = 13;
            //piece.PLAYER = PLAYER;
            //GameTable.AddPiece(piece);

            //piece = new PIECE2(PLAYER, pieceType);
            //piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            //piece.Paint += new PaintEventHandler(Piece_Paint);
            //piece.Name = "2_1";
            //piece.Column = 5;
            //piece.Row = 12;
            //piece.PLAYER = PLAYER;
            //GameTable.AddPiece(piece);
        }

        private void SetPieces_Zone4(PLAYER PLAYER, int pieceType)
        {
            //SetPieces_Zone4_1(PLAYER, pieceType); return;

            PIECE piece;

            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_1";
            piece.Column = 13;
            piece.Row = 1;
            GameTable.AddPiece(piece);
            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_2";
            piece.Column = 13;
            piece.Row = 2;
            GameTable.AddPiece(piece);

            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_3";
            piece.Column = 1;
            piece.Row = 1;
            GameTable.AddPiece(piece);
            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_4";
            piece.Column = 1;
            piece.Row = 2;
            GameTable.AddPiece(piece);
            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_5";
            piece.Column = 1;
            piece.Row = 3;
            GameTable.AddPiece(piece);
            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_6";
            piece.Column = 1;
            piece.Row = 4;
            GameTable.AddPiece(piece);
            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_7";
            piece.Column = 1;
            piece.Row = 5;
            GameTable.AddPiece(piece);

            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_8";
            piece.Column = 5;
            piece.Row = 13;
            GameTable.AddPiece(piece);
            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_9";
            piece.Column = 5;
            piece.Row = 12;
            GameTable.AddPiece(piece);
            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_10";
            piece.Column = 5;
            piece.Row = 11;
            GameTable.AddPiece(piece);

            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_11";
            piece.Column = 8;
            piece.Row = 13;
            GameTable.AddPiece(piece);
            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_12";
            piece.Column = 8;
            piece.Row = 12;
            GameTable.AddPiece(piece);
            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_13";
            piece.Column = 8;
            piece.Row = 11;
            GameTable.AddPiece(piece);
            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_14";
            piece.Column = 8;
            piece.Row = 10;
            GameTable.AddPiece(piece);
            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_15";
            piece.Column = 8;
            piece.Row = 9;
            GameTable.AddPiece(piece);
        }

        private void SetPieces_Zone4_1(PLAYER PLAYER, int pieceType)
        {
            PIECE piece;
            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "4_0";
            piece.Column = 8;
            piece.Row = 13;
            GameTable.AddPiece(piece);

            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "4_1";
            piece.Column = 8;
            piece.Row = 12;
            GameTable.AddPiece(piece);

            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "4_3";
            piece.Column = 13;
            piece.Row = 13;
            GameTable.AddPiece(piece);

            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "4_4";
            piece.Column = 12;
            piece.Row = 1;
            GameTable.AddPiece(piece);

            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "4_5";
            piece.Column = 2;
            piece.Row = 13;
            GameTable.AddPiece(piece);

            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "4_6";
            piece.Column = 2;
            piece.Row = 12;
            GameTable.AddPiece(piece);

            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "4_7";
            piece.Column = 5;
            piece.Row = 13;
            GameTable.AddPiece(piece);

            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "4_8";
            piece.Column = 5;
            piece.Row = 12;
            GameTable.AddPiece(piece);
        }
        #endregion

        #region GAME TABLE
        private void ButtonGameSound_Click(object sender, EventArgs e)
        {
            GameSound = !GameSound;
        }

        private void TimerGame_Tick(object sender, EventArgs e)
        {
            TimeSpan time = DateTime.Now.Subtract(startTime);
            if (time.Hours > 0)
                labelTime.Text = time.ToString(@"hh\:mm\:ss");
            else
                labelTime.Text = time.ToString(@"mm\:ss");
        }

        private void PlaySound(string sound)
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                if (Form.ActiveForm != this)
                    ExtensionMethods.FlashNotification(this);
            }));
            if (GameSound)
            {
                if (sound == SOUND_MOVEPIECE)
                    movePieceSound.Play();
                else if (sound == SOUND_ROLLDICE)
                    rollDiceSound.Play();
                else if (sound == SOUND_YOURTURN)
                    yourTurn.Play();
                else if (sound == SOUND_ENDGAME_WIN)
                    endGameWIN.Play();
                else if (sound == SOUND_ENDGAME_LOST)
                    endGameLOST.Play();
            }
            Thread.Sleep(150);
        }

        private PictureBox ArrowUp()
        {
            PictureBox arrowUp = new PictureBox
            {
                BackgroundImage = Resources.ic_ArrowUp,
                BackgroundImageLayout = ImageLayout.Center,
                Size = new Size(20, 20),
                Dock = DockStyle.Top
            };
            return arrowUp;
        }

        private PictureBox ArrowDown()
        {
            PictureBox arrowDown = new PictureBox
            {
                BackgroundImage = Resources.ic_ArrowDown,
                BackgroundImageLayout = ImageLayout.Center,
                Size = new Size(20, 20),
                Dock = DockStyle.Bottom
            };
            return arrowDown;
        }

        private void Piece_Paint(object sender, PaintEventArgs e)
        {
            PIECE piece = (PIECE)sender;

            if (piece.Painted)
            {
                string text = (piece.PieceStack.Count + 1).ToString();
                Font Font = new Font("Consolas", 20);
                SizeF textSize = e.Graphics.MeasureString(text, Font);
                PointF locationToDraw = new PointF
                {
                    X = (piece.Width / 2) - (textSize.Width / 2),
                    Y = (piece.Height / 2) - (textSize.Height / 2)
                };
                e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                e.Graphics.DrawString(text, Font, Brushes.White, locationToDraw);
            }
        }

        private void UnselectPiece()
        {
            //Console.WriteLine("selectedPiece = null");
            SelectedPiece.BorderStyle = BorderStyle.None;
            SelectedPiece = null;
        }

        private void ShowMovesSuggestion()
        {
            foreach (PossibleLocation loc in SelectedPiece.PossibleMovesList.Where(x => x.Row <= 6).GroupBy(x => x.Column).Select(x => x.First()))
            {
                if (loc.Row <= 6)
                    movesSuggestion[loc.Column].Controls.Add(ArrowUp());
                else
                    movesSuggestion[loc.Column].Controls.Add(ArrowDown());
            }
            foreach (PossibleLocation loc in SelectedPiece.PossibleMovesList.Where(x => x.Row >= 8).GroupBy(x => x.Column).Select(x => x.First()))
            {
                if (loc.Row <= 6)
                    movesSuggestion[loc.Column].Controls.Add(ArrowUp());
                else
                    movesSuggestion[loc.Column].Controls.Add(ArrowDown());
            }
        }

        private void Piece_MouseClick(object sender, MouseEventArgs e)
        {
            PIECE piece = (PIECE)sender;
            if (MyTurn == true && moves > 0 /*&& ME.PiecesType == piece.PieceType*/)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (SelectedPiece == null)
                    {
                        if (ME.PiecesType == piece.PieceType && (piece == ME.PieceFromBar || (GameTable.IsFirstPieceFromColumn(piece) && !ME.HasPiecesOnBar)))
                        {
                            SelectedPiece = piece;
                            SelectedPiece.BorderStyle = BorderStyle.FixedSingle;
                            //Console.WriteLine("selectedPiece = Piece " + SelectedPiece.Name);
                        }
                    }
                    else
                    {
                        if (SelectedPiece == piece)
                        {
                            UnselectPiece();
                        }
                        else
                        {
                            if (piece.Column != SelectedPiece.Column || (SelectedPiece.Row <= 6 && piece.Row >= 8) || (SelectedPiece.Row >= 8 && piece.Row <= 6))
                            {
                                GameTable_CellClicked(piece.Column, piece.Row);
                            }
                        }
                    }
                }
                else if (e.Button == MouseButtons.Right && SelectedPiece != null)
                {
                    UnselectPiece();
                }
            }
        }

        private void GameTable_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (SelectedPiece != null)
                {
                    int row = 0;
                    int verticalOffset = 0;
                    foreach (int h in GameTable.GetRowHeights())
                    {
                        int column = 0;
                        int horizontalOffset = 0;
                        foreach (int w in GameTable.GetColumnWidths())
                        {
                            Rectangle rectangle = new Rectangle(horizontalOffset, verticalOffset, w, h);
                            if (rectangle.Contains(e.Location))
                            {
                                Console.WriteLine(string.Format("GameTable column {0}, row {1} was clicked", column, row));
                                if (column != 7 && row != 7)
                                    if (SelectedPiece.Column != column || (SelectedPiece.Row <= 6 && row >= 8) || (SelectedPiece.Row >= 8 && row <= 6))
                                        GameTable_CellClicked(column, row);
                                return;
                            }
                            horizontalOffset += w;
                            column++;
                        }
                        verticalOffset += h;
                        row++;
                    }
                }
            }
            else if (e.Button == MouseButtons.Right && SelectedPiece != null)
            {
                UnselectPiece();
            }
        }

        private void GameTable_CellClicked(int column, int row)
        {
            if (SelectedPiece.CanMove(column, row))
            {
                PlaySound(SOUND_MOVEPIECE);
                SelectedPiece.MovePiece(column, row);
                SelectedPiece = null;

                if (ME.CountPieces == 10)
                {
                    if (GameTable.IsWinLines_2(ME))
                    {
                        GAMEOVER(WIN);
                        return;
                    }
                }
                else if (ME.CountPieces == 5)
                {
                    if (GameTable.IsWinLines_1(ME))
                    {
                        GAMEOVER(WIN);
                        return;
                    }
                }
                else if (ME.CountPieces == 0)
                {
                    GAMEOVER(WIN);
                    return;
                }

                //Undo = GameTable;
                moves = GameTable.Controls.OfType<Dice>().Where(x => x.DiceValue != 0).Count(); Console.WriteLine("MOVES: " + moves);
                if (moves > 0)
                {
                    ME.CountPossibleMoves = 0;
                    GameTable.SetPossibleMoves(ME);
                    //SendToServer(SET_POSSIBLE_MOVES);

                    if (ME.CountPossibleMoves == 0)
                        NoMovesPossible(false);
                    else
                    {
                        if (!undo)
                            buttonUndo.Visible = true;
                    }
                }
                else
                    MyTurn = false;
            }
        }

        private void ButtonUndo_Click(object sender, EventArgs e)
        {
            undo = true;
            GameTable.UndoTable();
            //SendToServer(DEF.UNDO);

            buttonUndo.Visible = false;
            if (SelectedPiece != null)
                UnselectPiece();
            GameTable.SetPossibleMoves(ME);
        }

        private async void ComputerMovePiece()
        {
            Console.WriteLine("ComputerMovePiece();");

            while (moves > 0)
            //if (moves > 0)
            {
                await Task.Delay(600);
                bool? rez = GameTable.ComputerMovePiece(COMPUTER, ME, moves, DoubleDice);

                //TEST FOR ERROR ----
                if (rez == null)
                {
                    Console.WriteLine("ERROR");
                    return;
                }
                //-----------

                if (rez == false)
                {
                    NoMovesPossible(true);
                    return;
                }

                if (COMPUTER.CountPieces == 10)
                {
                    if (GameTable.IsWinLines_2(COMPUTER))
                    {
                        GAMEOVER(WIN);
                        return;
                    }
                }
                else if (COMPUTER.CountPieces == 5)
                {
                    if (GameTable.IsWinLines_1(COMPUTER))
                    {
                        GAMEOVER(WIN);
                        return;
                    }
                }
                else if (COMPUTER.CountPieces == 0)
                {
                    GAMEOVER(LOSE);
                    return;
                }

                Console.WriteLine("------------------------------");

                moves = GameTable.Controls.OfType<Dice>().Where(x => x.DiceValue != 0).Count(); Console.WriteLine("MOVES: " + moves);
            }

            MyTurn = true;
        }

        private async void NoMovesPossible(bool value)
        {
            BlockDice();
            panelNoPossibleMoves.Visible = true;
            GameTable.Enabled = false;
            await Task.Delay(2500);
            panelNoPossibleMoves.Visible = false;
            GameTable.Enabled = true;
            moves = 0;
            MyTurn = value;
        }

        private void GAMEOVER(string WIN_OR_LOSE)
        {
            //Console.WriteLine("GAME_OVER");
            timerGame.Stop();
            MyTurn = null;
            //moves = 0;

            //buttonUndo.Visible = false;
            //buttonRollDice.Visible = false;
            //ME.Panel.BorderStyle = COMPUTER.Panel.BorderStyle = BorderStyle.None;
            //ME.Panel.BackColor = COMPUTER.Panel.BackColor = Color.FromArgb(176, 142, 106);

            //BlockDice();

            if (WIN_OR_LOSE == WIN)
            {
                PlaySound(SOUND_ENDGAME_WIN);
                labelGameResult.Text = "YOU WON!";
            }
            else
            {
                PlaySound(SOUND_ENDGAME_LOST);
                labelGameResult.Text = "YOU LOST!";
            }
            panelGameOver.Visible = true;
        }

        private void ButtonRematch_Click(object sender, EventArgs e)
        {
            if (labelGameResult.Text == "YOU WON!")
                SetRematch(true);
            else
                SetRematch(false);
        }

        private void ButtonExitGame_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void ChangeTurn()
        {
            Console.WriteLine("CHANGETURN");

            moves = 0;
            MyTurn = !MyTurn;
            BlockDice();
        }

        #region DICE
        private async void ComputerRollDice()
        {
            await Task.Delay(300);

            DoubleDice = false;
            int dice1 = random.Next(1, 7);
            int dice2 = random.Next(1, 7);

            backgroundWorker_Dice.RunWorkerAsync(dice1 + "." + dice2 + ".COMPUTER");
        }

        private void ButtonRollDice_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                RollDice();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            MyTurn = true;
        }

        private void ButtonRollDice_MouseLeave(object sender, EventArgs e)
        {
            this.ActiveControl = GameTable;
        }

        private void RollDice()
        {
            if (MyTurn == true && moves == 0 && !backgroundWorker_Dice.IsBusy)
            {
                DoubleDice = false;
                buttonRollDice.Enabled = false;
                int dice1 = random.Next(1, 7);
                int dice2 = random.Next(1, 7);

                //int dice1 = Convert.ToInt32(r1.Text);
                //int dice2 = Convert.ToInt32(r2.Text);

                backgroundWorker_Dice.RunWorkerAsync(dice1 + "." + dice2 + ".ME");
            }
        }

        private void SetRollingDice(int dice1, int dice2)
        {
            SetRollingDice(Dice1, dice1);
            SetRollingDice(Dice2, dice2);
        }

        private void SetRollingDice(Dice Dice, int dice)
        {
            switch (dice)
            {
                case 1: Dice.BackgroundImage = Resources.rollingdice1; break;
                case 2: Dice.BackgroundImage = Resources.rollingdice2; break;
                case 3: Dice.BackgroundImage = Resources.rollingdice3; break;
                case 4: Dice.BackgroundImage = Resources.rollingdice4; break;
                case 5: Dice.BackgroundImage = Resources.rollingdice5; break;
                case 6: Dice.BackgroundImage = Resources.rollingdice6; break;
            }
            Dice.Visible = true;
        }

        private void SetDice(int dice1, int dice2)
        {
            Dice1.DiceValue = dice1;
            Dice2.DiceValue = dice2;
            moves = 2;
            if (dice1 == dice2)
            {
                Dice3.DiceValue = Dice4.DiceValue = dice1;
                moves += 2;
                DoubleDice = true;
            }
        }

        private void BlockDice()
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                Dice1.DiceBlocked = true;
                Dice2.DiceBlocked = true;
                Dice3.DiceBlocked = true;
                Dice4.DiceBlocked = true;
            }));
        }

        private void HideDice()
        {
            Dice1.Visible = false;
            Dice2.Visible = false;
            Dice3.Visible = false;
            Dice4.Visible = false;
        }

        private void BackgroundWorker_Dice_DoWork(object sender, DoWorkEventArgs e)
        {
            string dice = e.Argument as string;

            PlaySound(SOUND_ROLLDICE);
            for (int i = 1; i <= 10; i++)
            {
                backgroundWorker_Dice.ReportProgress(i, random.Next(1, 7) + "." + random.Next(1, 7));
                Thread.Sleep(65);
            }
            backgroundWorker_Dice.ReportProgress(11, dice);
            Thread.Sleep(65);

            e.Result = dice.Split('.').Last();
        }

        private void BackgroundWorker_Dice_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string[] dice = ((string)e.UserState).Split('.');
            int dice1 = Convert.ToInt32(dice[0]);
            int dice2 = Convert.ToInt32(dice[1]);

            if (e.ProgressPercentage < 11)
                SetRollingDice(dice1, dice2);
            else
                SetDice(dice1, dice2);
        }

        private void BackgroundWorker_Dice_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((string)e.Result == "ME")
            {
                ME.CountPossibleMoves = 0;
                GameTable.SetPossibleMoves(ME);
                if (ME.CountPossibleMoves == 0)
                    NoMovesPossible(false);
            }
            else //COMPUTER
            {
                //COMPUTER.CountPossibleMoves = 0;
                //GameTable.SetPossibleMoves(COMPUTER);
                //if (COMPUTER.CountPossibleMoves == 0)
                //NoMovesPossible(true);
                //else
                COMPUTER.CountPossibleMoves = 0;
                ComputerMovePiece();
            }
        }
        #endregion

        private void Button1_Click(object sender, EventArgs e)
        {
            //Console.WriteLine(moves);
            //Console.WriteLine("D1: " + Dice1.DiceValue);
            //Console.WriteLine("D2: " + Dice2.DiceValue);
            ComputerMovePiece();
        }
    }
}