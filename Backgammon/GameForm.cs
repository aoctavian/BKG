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
using System.Net.NetworkInformation;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace Backgammon
{
    public partial class GameForm : Form
    {
        private const string
            SOUND_YOURTURN = "PLAYSOUND_YOURTURN",
            SOUND_MOVEPIECE = "PLAYSOUND_MOVEPIECE",
            SOUND_ROLLDICE = "PLAYSOUND_ROLLDICE",
            SOUND_CHATMESSAGE = "PLAYSOUND_CHATMESSAGE",
            SOUND_ENDGAME_WIN = "ENDGAME_WIN",
            SOUND_ENDGAME_LOST = "ENDGAME_LOST";
        private const string WIN = "WIN", LOSE = "LOSE", ABANDON = "ABANDON";
        private const int HOUSE_LEFT = 0, HOUSE_RIGHT = 1;

        public int GAME_TYPE = HOUSE_RIGHT; //used for NoPossibleMovesPanel

        private bool chatSound, gameSound, typing;
        private bool? myTurn;
        private PIECE selectedPiece;
        public PLAYER ME, OPPONENT;
        int moves;
        bool undo;

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
                switch(myTurn)
                {
                    case true:
                        undo = false;
                        buttonRollDice.Enabled = true;
                        buttonRollDice.Visible = true;
                        PlaySound(SOUND_YOURTURN);
                        ME.Panel.BorderStyle = BorderStyle.Fixed3D;
                        ME.Panel.BackColor = Color.FromArgb(154, 120, 82);
                        OPPONENT.Panel.BorderStyle = BorderStyle.None;
                        OPPONENT.Panel.BackColor = Color.FromArgb(176, 142, 106);
                        break;
                    case false:
                        buttonUndo.Visible = false;
                        buttonRollDice.Visible = false;
                        ME.Panel.BorderStyle = BorderStyle.None;
                        ME.Panel.BackColor = Color.FromArgb(176, 142, 106);
                        OPPONENT.Panel.BorderStyle = BorderStyle.Fixed3D;
                        OPPONENT.Panel.BackColor = Color.FromArgb(154, 120, 82);
                        break;
                    case null:
                        moves = 0;
                        buttonUndo.Visible = false;
                        buttonRollDice.Visible = false;
                        ME.Panel.BorderStyle = OPPONENT.Panel.BorderStyle = BorderStyle.None;
                        ME.Panel.BackColor = OPPONENT.Panel.BackColor = Color.FromArgb(176, 142, 106);
                        BlockDice();
                        break;
                }
            }
        }
        bool ChatSound
        {
            get { return chatSound; }
            set
            {
                chatSound = value;
                Settings.Default.InGameChatSound = value;
                if (chatSound)
                {
                    buttonChatSound.BackgroundImage = Resources.SoundON;
                    toolTip.SetToolTip(buttonChatSound, "Mute chat sound");
                }
                else
                {
                    buttonChatSound.BackgroundImage = Resources.SoundOFF;
                    toolTip.SetToolTip(buttonChatSound, "Unmute chat sound");
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
        bool OpponentTyping
        {
            get { return typing; }
            set
            {
                if (typing != value)
                {
                    typing = value;
                    if (typing)
                    {
                        MessageOpponentTyping();
                    }
                    else
                    {
                        layoutPanelChatMessages.Controls.RemoveAt(layoutPanelChatMessages.Controls.Count - 1);
                        layoutPanelChatMessages.RowCount--;
                    }
                }
            }
        }

        bool safeLeave, rematch;
        public bool forcedExit = false;
        public bool exitFromMain = false;

        SoundPlayer rollDiceSound, movePieceSound, chatMessageSound, yourTurn, endGameWIN, endGameLOST;
        DateTime startTime;
        Random random;
        private readonly Panel[] movesSuggestion;

        #region GAME FORM
        public GameForm(int zone, int piecesType)
        {
            InitializeComponent();
            
            DEF.NET.GameForm = this;

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
            SetMyInfo();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            random = new Random();
            ChatSound = GameSound = Settings.Default.GameSound;
            rollDiceSound = new SoundPlayer(Resources.rolldice);
            movePieceSound = new SoundPlayer(Resources.movepiece);
            chatMessageSound = new SoundPlayer(Resources.chatMessage);
            yourTurn = new SoundPlayer(Resources.yourTurn);
            endGameWIN = new SoundPlayer(Resources.endgameWin);
            endGameLOST = new SoundPlayer(Resources.endgameLost);

            toolTip.SetToolTip(buttonShowChat, "Show chat window");
            toolTip.SetToolTip(labelMessageCounter, "Show chat window");
            toolTip.SetToolTip(buttonCloseChat, "Close chat window");
            toolTip.SetToolTip(buttonSendMessage, "Send");
        }

        private void GameForm_Shown(object sender, EventArgs e)
        {
            startTime = DateTime.Now;
            timerGame.Start();
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!forcedExit)
            {
                if (ME.CountPieces > 0 && !safeLeave)
                {
                    DialogResult dr = MessageBox.Show("Are you sure you want to leave the game?", "Leaving the game", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        SendToServer(DEF.OPPONENT_LEFT);
                        if (ME.GetPlayerType() == DEF.TYPE_PLAYER)
                            UpdateMyScore(ABANDON);
                    }
                    else if (dr == DialogResult.No)
                    {
                        e.Cancel = true;
                        return;
                    }
                }

                if (panelGameOver.Visible && !labelOpponentLeftMatch.Visible)
                    SendToServer(DEF.GAME_END);

                if (!exitFromMain)
                    DEF.NET.BackOnline();
            }
            else
            {
                DEF.NET.BackOnline();
            }
            DEF.NET.GameForm = null;
            Settings.Default.Save();
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
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
            else if (e.KeyCode == Keys.F2)
            {
                chatMessage.Focus();
            }
            else if (e.Control && e.KeyCode == Keys.Oem3)
            {
                ChangeTurn();
            }
            else if(e.Control && e.KeyCode == Keys.S)
            {
                if (ME.GetPlayerType() == DEF.TYPE_PLAYER)
                    StatusFormShow();
            }
        }

        private void GameForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                buttonCloseChat.Visible = false;
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                buttonCloseChat.Visible = true;
            }
        }

        private void GameForm_ResizeEnd(object sender, EventArgs e)
        {
            if (layoutPanelChatMessages.Controls.Count > 0)
                panelChatMessages.ScrollControlIntoView(layoutPanelChatMessages.Controls[layoutPanelChatMessages.Controls.Count - 1]);
            this.Size = new Size(this.Width - GameTable.Width % 14 + 1, this.Height - GameTable.Height % 13 + 1);
            SetLocationPanelNoMovesPossible();
        }

        private void GameForm_Activated(object sender, EventArgs e)
        {
            //if (Application.OpenForms.OfType<StatusForm>().SingleOrDefault() != null)
            //    Application.OpenForms.OfType<StatusForm>().Single().Activate();
        }
        #endregion

        #region CONNECTION
        public void ConnectionError()
        {
            PlaySound(SOUND_ENDGAME_LOST);
            formLayoutPanel.Enabled = false;
            timerGame.Stop();
            labelMessageExitMatch.Text = "CONNECTION ERROR\nThe game cannot continue";
            panelExitMatch.Visible = true;
            forcedExit = true;
        }

        private void ButtonExitWithError_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region SET TABLE
        private void SetLocationPanelNoMovesPossible()
        {
            int GFw;
            if(!buttonShowChat.Visible)
                GFw = this.ClientSize.Width - 250 - 85;
            else
                GFw = this.ClientSize.Width - 85;

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
            ME = new PLAYER(piecesType, zone)
            {
                Id = Settings.Default.Id,
                Name = Settings.Default.Name
            };
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
            if (piecesType == 1)
                piecesType += 1;
            else
                piecesType -= 1;

            OPPONENT = new PLAYER(piecesType, zone);
            if (zone <= 2)
                OPPONENT.Panel = panelPlayerInfo_1;
            else
                OPPONENT.Panel = panelPlayerInfo_2;
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

        private void StartGame()
        {
            MyTurn = true;
            SendToServer($"{DEF.PLAYER_INFO_2}.{SendMyInfo(ME.PermissionToSeeInfo)}");
        }

        private void GameStarting(string opponentInfo)
        {
            SetOpponentInfo(opponentInfo);
            SetPieces(ME);
            SetPieces(OPPONENT);
            formLayoutPanel.Enabled = true;
            loadingGIF.Dispose();
        }

        private void SetOpponentInfo(string info)
        {
            string[] i = info.Split('*');

            switch (OPPONENT.PiecesType)
            {
                case 1: ((PictureBox)OPPONENT.Panel.Controls[1]).Image = Resources.piece1; break;
                case 2: ((PictureBox)OPPONENT.Panel.Controls[1]).Image = Resources.piece2; break;
            }

            OPPONENT.Id = Convert.ToInt32(i[0]);
            OPPONENT.Name = i[1];
            OPPONENT.Panel.Controls[0].Text = i[1];
            labelWaitForOpponent.Text = "Waiting for " + i[1] + "...";

            if (OPPONENT.GetPlayerType() == DEF.TYPE_PLAYER)
            {
                if (OPPONENT.PermissionToSeeInfo)
                {
                    GetOpponentStatus();
                }
                else
                {
                    OPPONENT.Panel.Controls[0].MouseDoubleClick += new MouseEventHandler(OpponentName_MouseDoubleClick);
                    toolTip.SetToolTip(OPPONENT.Panel.Controls[0], "Opponent blocked permission to see his status.\nDouble click for asking permission.");
                }
            }
        }

        private string SendMyInfo(bool Permission)
        {
            return ME.Id + "*" + ME.Name + "*" + Permission;
        }

        private void SetMyInfo()
        {
            switch (ME.PiecesType)
            {
                case 1: ((PictureBox)ME.Panel.Controls[1]).Image = Resources.piece1; break;
                case 2: ((PictureBox)ME.Panel.Controls[1]).Image = Resources.piece2; break;
            }
            
            ME.Panel.Controls[0].Text = "Me";
            if (ME.GetPlayerType() == DEF.TYPE_PLAYER)
            {
                ME.Panel.Controls[0].MouseDoubleClick += new MouseEventHandler(MyName_MouseDoubleClick);
                toolTip.SetToolTip(ME.Panel.Controls[0], "Double click to see status");
            }
        }

        private async void GetOpponentStatus()
        {
            await DEF.connection.OpenAsync();
            MySqlCommand command = new MySqlCommand("select * from usersscore where IdUser = '" + OPPONENT.Id + "'", DEF.connection);
            DbDataReader DR = await command.ExecuteReaderAsync();
            if (DR.Read())
            {
                toolTip.SetToolTip(OPPONENT.Panel.Controls[0],
                                                "Wins: " + DR[1] + "\n" +
                                                "Winning Streak: " + DR[2] + "\n" +
                                                "Longest Winning Streak: " + DR[3] + "\n" +
                                                "Loses: " + DR[4] + "\n" +
                                                "Abandons: " + DR[5]);
            }
            DEF.connection.Close();
        }

        private void StatusFormShow()
        {
            new StatusForm().ShowDialog();
        }

        private void MyName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            StatusFormShow();
        }

        private void OpponentName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OPPONENT.Panel.Controls[0].MouseDoubleClick -= new MouseEventHandler(OpponentName_MouseDoubleClick);
            SendToServer(DEF.ASK_FOR_INFO);
            toolTip.SetToolTip(OPPONENT.Panel.Controls[0],"Waiting for " + OPPONENT.Name +" to give permission...");
        }

        private void SetRematch(bool turn)
        {
            rematch = false;
            safeLeave = false;
            panelGameOver.Visible = false;
            ME.Panel.Controls[2].BackColor = OPPONENT.Panel.Controls[2].BackColor = Color.FromArgb(176, 142, 106);

            labelTime.Text = "00:00";
            startTime = DateTime.Now;
            timerGame.Start();

            GameTable.Controls.OfType<PIECE>().ToList().ForEach(t => 
            {
                GameTable.Remove(t);
                t.Dispose();
            });

            MyTurn = turn;
            SetPieces(ME);
            SetPieces(OPPONENT);
        }
        #endregion

        #region SERVER <-> CLIENT COMMUNICATION
        private void SendToServer(string send)
        {
            Console.WriteLine(send);
            DEF.NET.STW.WriteLine(Crypto.Encrypt(send));
        }

        public void DoWhatReceived(string received)
        {
            string[] r = received.Split('.');
            
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                switch (r[0])
                {
                    case DEF.START_GAME:
                        StartGame();
                        break;
                    case DEF.PLAYER_INFO_1:
                        GameStarting(r[1]);
                        break;
                    case DEF.PLAYER_INFO_2:
                        SendToServer($"{DEF.PLAYER_INFO_1}.{SendMyInfo(ME.PermissionToSeeInfo)}");
                        MyTurn = false;
                        GameStarting(r[1]);
                        break;
                    case DEF.CHAT_MESSAGE:
                        MessageReceived(string.Join(".", r.Skip(1)));
                        break;
                    case DEF.CHAT_MESSAGE_TYPING:
                        OpponentTyping = true;
                        break;
                    case DEF.CHAT_MESSAGE_STOPPED_TYPING:
                        OpponentTyping = false;
                        break;
                    case DEF.ASK_FOR_INFO:
                        MessageAskForPermissionToSeeStatus();
                        break;
                    case DEF.PERMISSION_GRANTED:
                        OPPONENT.PermissionToSeeInfo = true;
                        GetOpponentStatus();
                        MessagePermissionGranted();
                        break;
                    case DEF.MOVE:
                        GameTable.SaveTable();
                        int mColumn = Convert.ToInt32(r[1]);
                        int mRow = Convert.ToInt32(r[2]);
                        int column = Convert.ToInt32(r[3]);
                        int row = Convert.ToInt32(r[4]);
                        PlaySound(SOUND_MOVEPIECE);
                        ((PIECE)GameTable.GetControlFromPosition(mColumn, mRow)).MovePiece(column, row);
                        moves = GameTable.Controls.OfType<Dice>().Where(x => x.DiceValue != 0).Count();
                        if (moves > 0)
                        {
                            OPPONENT.CountPossibleMoves = 0;
                            GameTable.SetPossibleMoves(OPPONENT);
                            if (OPPONENT.CountPossibleMoves == 0 && OPPONENT.CountPieces > 0)
                                NoMovesPossible(true);
                        }
                        else
                            MyTurn = true;
                        break;
                    case DEF.UNDO:
                        GameTable.UndoTable();
                        GameTable.SetPossibleMoves(OPPONENT);
                        break;
                    case DEF.DICE:
                        backgroundWorker_Dice.RunWorkerAsync(r[1] + "." + r[2] + ".OPPONENT");
                        break;
                    case DEF.GAME_OVER:
                        GAMEOVER(LOSE);
                        break;
                    case DEF.REMATCH:
                        rematch = true;
                        if (labelWaitForOpponent.Visible == true)
                        {
                            if (labelGameResult.Text == "YOU WON!")
                                SetRematch(true);
                            else
                                SetRematch(false);
                        }
                        break;
                    case DEF.GAME_END:
                        buttonRematch.Visible = false;
                        buttonExitMatch.Visible = true;
                        labelWaitForOpponent.Visible = false;
                        panelGameOver.UseWaitCursor = false;
                        labelOpponentLeftMatch.Text = OPPONENT.Name + " left the match";
                        labelOpponentLeftMatch.Visible = true;
                        break;
                    case DEF.OPPONENT_LEFT:
                        timerGame.Stop();
                        if(ME.GetPlayerType() == DEF.TYPE_PLAYER)
                            UpdateMyScore(WIN);
                        safeLeave = true;
                        formLayoutPanel.Enabled = false;
                        labelGameResult.Text = "YOU WON!";
                        buttonRematch.Visible = false;
                        labelOpponentLeftMatch.Text = OPPONENT.Name + " left the match";
                        labelOpponentLeftMatch.Visible = true;
                        labelWaitForOpponent.Visible = false;
                        buttonExitMatch.Visible = true;
                        panelGameOver.Visible = true;
                        ME.Panel.Controls[2].Text = (int.Parse(ME.Panel.Controls[2].Text) + 1).ToString();
                        break;
                    case DEF.CHANGE_TURN:
                        moves = 0;
                        MyTurn = true;
                        break;
                    default:
                        //MessageBox.Show("Something went wrong. I have no idea what happened.");
                        Console.WriteLine($"ILLEGAL STRING RECEIVED IN GAME FORM ->{received}");
                        break;
                }
            }));
        }
        #endregion

        #region CHAT
        private void ChatMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                chatMessage.AppendText(Environment.NewLine);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendMessage();
            }
        }

        private void ChatMessage_TextChanged(object sender, EventArgs e)
        {
            if (chatMessage.Text.Length == 1)
            {
                //if (char.IsLetter(Convert.ToChar(chatMessage.Text)))
                //{
                //    chatMessage.Text = chatMessage.Text.ToUpper();
                //    chatMessage.SelectionStart = 2;
                //}
                SendToServer(DEF.CHAT_MESSAGE_TYPING);
            }
            else if(chatMessage.Text.Length == 0)
            {
                SendToServer(DEF.CHAT_MESSAGE_STOPPED_TYPING);
            }
        }

        private void ButtonSendMessage_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void ButtonSendMessage_MouseEnter(object sender, EventArgs e)
        {
            buttonSendMessage.BackColor = Color.WhiteSmoke;
        }

        private void ButtonSendMessage_MouseLeave(object sender, EventArgs e)
        {
            buttonSendMessage.BackColor = Color.Transparent;
        }

        private void SendMessage()
        {
            if (!string.IsNullOrWhiteSpace(chatMessage.Text))
            {
                this.BeginInvoke(new MethodInvoker(delegate ()
                {
                    MessageSent(chatMessage.Text.Trim());
                    string message = chatMessage.Text.Trim();
                    chatMessage.Text = ""; //here sends STOP_TYPING
                    SendToServer($"{DEF.CHAT_MESSAGE}.{message}");
                }));
            }
        }
    
        private void MessageSent(string message)
        {
            layoutPanelChatMessages.RowCount++;
            Console.WriteLine("rct: " + layoutPanelChatMessages.RowCount);
            RowStyle rs = new RowStyle(SizeType.AutoSize);
            if (OpponentTyping)
            {
                OpponentTyping = false;
                layoutPanelChatMessages.RowStyles.Add(rs);
                OpponentTyping = true;
            }
            else
                layoutPanelChatMessages.RowStyles.Add(rs);
            Label lb = MessageLabel(message, Color.FromArgb(216, 227, 231), DockStyle.Right); //LightBlue
            layoutPanelChatMessages.Controls.Add(lb, 0, layoutPanelChatMessages.RowCount - 1);
            panelChatMessages.ScrollControlIntoView(lb);
        }

        private void MessageReceived(string message)
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                PlaySound(SOUND_CHATMESSAGE);
                
                layoutPanelChatMessages.RowCount++;
                RowStyle rs = new RowStyle(SizeType.AutoSize);
                layoutPanelChatMessages.RowStyles.Add(rs);
                Label lb = MessageLabel(message, Color.FromArgb(255, 226, 200), DockStyle.Left); //Tan
                layoutPanelChatMessages.Controls.Add(lb, 0, layoutPanelChatMessages.RowCount - 1);
                panelChatMessages.ScrollControlIntoView(lb);

                if (formLayoutPanel.ColumnStyles[2].Width == 0)
                {
                    if (labelMessageCounter.Text == "")
                        labelMessageCounter.Text = "1";
                    else
                        labelMessageCounter.Text = (Convert.ToInt32(labelMessageCounter.Text) + 1).ToString();
                }
            }));
        }

        private Label MessageLabel(string message, Color color, DockStyle dockStyle)
        {
            Label l = new Label
            {
                Font = new Font("Segoe UI", 10.5F),
                Text = message,
                BackColor = color,
                Padding = new Padding(5, 5, 5, 5),
                Margin = new Padding(2, 2, 2, 2),
                AutoSize = true,
                MaximumSize = new Size(200, int.MaxValue),
                Dock = dockStyle
            };
            return l;
        }

        private void MessageOpponentTyping()
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                layoutPanelChatMessages.RowCount++;
                RowStyle rs = new RowStyle(SizeType.AutoSize);
                layoutPanelChatMessages.RowStyles.Add(rs);
                Label lb = new Label
                {
                    Font = new Font("Segoe UI", 9.5F),
                    Text = OPPONENT.Name + " is typing...",
                    BackColor = Color.White,
                    Padding = new Padding(5, 2, 5, 2),
                    Margin = new Padding(2, 2, 2, 2),
                    AutoSize = true,
                    MaximumSize = new Size(200, int.MaxValue),
                    Dock = DockStyle.Left
                };
                layoutPanelChatMessages.Controls.Add(lb, 0, layoutPanelChatMessages.RowCount - 1);
                panelChatMessages.ScrollControlIntoView(lb);

                if (formLayoutPanel.ColumnStyles[2].Width == 0)
                {
                    if (labelMessageCounter.Text == "")
                        labelMessageCounter.Text = "1";
                    else
                        labelMessageCounter.Text = (Convert.ToInt32(labelMessageCounter.Text) + 1).ToString();
                }
            }));
        }

        private void MessageAskForPermissionToSeeStatus()
        {
            PlaySound(SOUND_CHATMESSAGE);

            layoutPanelChatMessages.RowCount++;
            RowStyle rs = new RowStyle(SizeType.AutoSize);
            //layoutPanelChatMessages.RowStyles.Insert(layoutPanelChatMessages.RowCount - 1, rs);
            layoutPanelChatMessages.RowStyles.Add(rs);
            Label lb = MessageLabel("Double click on this message to give permission to you opponent to see your status", Color.RosyBrown, DockStyle.Left);
            lb.Cursor = Cursors.Hand;
            lb.MouseDoubleClick += new MouseEventHandler(PermissionMessage_MouseDoubleClick);
            layoutPanelChatMessages.Controls.Add(lb, 0, layoutPanelChatMessages.RowCount - 1);
            panelChatMessages.ScrollControlIntoView(lb);

            if (formLayoutPanel.ColumnStyles[2].Width == 0)
            {
                if (labelMessageCounter.Text == "")
                    labelMessageCounter.Text = "1";
                else
                    labelMessageCounter.Text = (Convert.ToInt32(labelMessageCounter.Text) + 1).ToString();
            }
        }

        private void MessagePermissionGranted()
        {
            PlaySound(SOUND_CHATMESSAGE);

            layoutPanelChatMessages.RowCount++;
            RowStyle rs = new RowStyle(SizeType.AutoSize);
            //layoutPanelChatMessages.RowStyles.Insert(layoutPanelChatMessages.RowCount - 1, rs);
            layoutPanelChatMessages.RowStyles.Add(rs);
            Label lb = MessageLabel("Opponent has given you permission to see his status.", Color.RosyBrown, DockStyle.Left);
            layoutPanelChatMessages.Controls.Add(lb, 0, layoutPanelChatMessages.RowCount - 1);
            panelChatMessages.ScrollControlIntoView(lb);

            if (formLayoutPanel.ColumnStyles[2].Width == 0)
            {
                if (labelMessageCounter.Text == "")
                    labelMessageCounter.Text = "1";
                else
                    labelMessageCounter.Text = (Convert.ToInt32(labelMessageCounter.Text) + 1).ToString();
            }
        }

        private void PermissionMessage_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SendToServer(DEF.PERMISSION_GRANTED);
            ME.PermissionToSeeInfo = true;
            ((Label)sender).MouseDoubleClick -= new MouseEventHandler(PermissionMessage_MouseDoubleClick);
            ((Label)sender).Cursor = Cursors.Default;
            ((Label)sender).Text = "Permission granted for opponent to see your status.";
        }

        private void ShowChat()
        {
            formLayoutPanel.ColumnStyles[2].Width = 250;
            this.MinimumSize = new Size(1138, 685);

            buttonShowChat.Visible = false;
            labelMessageCounter.Text = "";

            if (layoutPanelChatMessages.Controls.Count > 0)
                panelChatMessages.ScrollControlIntoView(layoutPanelChatMessages.Controls[layoutPanelChatMessages.Controls.Count - 1]);

            SetLocationPanelNoMovesPossible();
        }

        private void ButtonShowChat_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                GameTable.MaximumSize = new Size(GameTable.Size.Width, GameTable.Size.Height);
                this.Width += 250;
                ShowChat();
                GameTable.MaximumSize = new Size(0, 0);
            }
            else
            {
                ShowChat();
            }
        }

        private void ButtonShowChat_MouseEnter(object sender, EventArgs e)
        {
            buttonShowChat.BackColor = SystemColors.Window;
        }

        private void ButtonShowChat_MouseLeave(object sender, EventArgs e)
        {
            buttonShowChat.BackColor = Color.Transparent;
        }

        private void ButtonChatSound_Click(object sender, EventArgs e)
        {
            ChatSound = !ChatSound;
        }

        private void ButtonCloseChat_Click(object sender, EventArgs e)
        {
            GameTable.MaximumSize = new Size(GameTable.Size.Width, GameTable.Size.Height);

            this.MinimumSize = new Size(1138 - 250, 685);
            formLayoutPanel.ColumnStyles[2].Width = 0;
            this.Width -= 250;

            buttonShowChat.Visible = true;
            SetLocationPanelNoMovesPossible();

            GameTable.MaximumSize = new Size(0, 0);
        }
        #endregion

        #region SET PIECES
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

            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_2";
            piece.Column = 1;
            piece.Row = 1;
            piece.PLAYER = PLAYER;
            GameTable.AddPiece(piece);

            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_3";
            piece.Column = 12;
            piece.Row = 1;
            piece.PLAYER = PLAYER;
            GameTable.AddPiece(piece);

            piece = new PIECE1(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "1_4";
            piece.Column = 13;
            piece.Row = 1;
            piece.PLAYER = PLAYER;
            GameTable.AddPiece(piece);
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
            piece.Column = 9;
            piece.Row = 1;
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
            piece.Column = 11;
            piece.Row = 1;
            piece.PLAYER = PLAYER;
            GameTable.AddPiece(piece);

            piece = new PIECE2(PLAYER, pieceType);
            piece.MouseClick += new MouseEventHandler(Piece_MouseClick);
            piece.Paint += new PaintEventHandler(Piece_Paint);
            piece.Name = "2_2";
            piece.Column = 1;
            piece.Row = 13;
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
            piece.Column = 9;
            piece.Row = 13;
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
                else if (sound == SOUND_CHATMESSAGE)
                    chatMessageSound.Play();
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
            //Console.WriteLine("Piece " + piece.Name + ", c=" + piece.Column + ", r=" + piece.Row);
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
                            //Console.WriteLine("selectedPiece = Piece " + SelectedPiece.Name + ", c=" + SelectedPiece.Column + ", r=" + SelectedPiece.Row);
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
                SendToServer($"{DEF.MOVE}.{SelectedPiece.Column}.{SelectedPiece.Row}.{column}.{row}");
                PlaySound(SOUND_MOVEPIECE);
                SelectedPiece.MovePiece(column, row);
                SelectedPiece = null;

                if(ME.CountPieces == 10)
                {
                    if (GameTable.IsWinLines_2(ME))
                    {
                        GAMEOVER(WIN);
                        return;
                    }
                }
                else if(ME.CountPieces == 5)
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

                moves = GameTable.Controls.OfType<Dice>().Where(x => x.DiceValue != 0).Count(); Console.WriteLine("MOVES: " + moves);
                if (moves > 0)
                {
                    ME.CountPossibleMoves = 0;
                    GameTable.SetPossibleMoves(ME);

                    if (ME.CountPossibleMoves == 0)
                        NoMovesPossible(false);
                    else
                    {
                        if(!undo)
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
            SendToServer(DEF.UNDO);

            buttonUndo.Visible = false;
            if(SelectedPiece != null)
                UnselectPiece();
            GameTable.SetPossibleMoves(ME);
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

        private void UpdateMyScore(string result)
        {
            string date = startTime.ToString("d MMM yyyy | hh:mm tt");
            switch (result)
            {
                case WIN:
                    MyStatus.Wins++;
                    MyStatus.WinningStreak++;
                    if (MyStatus.WinningStreak > MyStatus.LongestWinningStreak)
                        MyStatus.LongestWinningStreak = MyStatus.WinningStreak;
                    new MyStatus.Match(date, OPPONENT.Name, WIN);
                    UpdateStatusDB(WIN);
                    break;
                case LOSE:
                    MyStatus.Loses++;
                    MyStatus.WinningStreak = 0;
                    new MyStatus.Match(date, OPPONENT.Name, LOSE);
                    UpdateStatusDB(LOSE);
                    break;
                case ABANDON:
                    MyStatus.Abandons++;
                    MyStatus.WinningStreak = 0;
                    new MyStatus.Match(date, OPPONENT.Name, ABANDON);
                    UpdateStatusDB(ABANDON);
                    break;
            }
        }

        private void UpdateStatusDB(string result)
        {
            SendToServer($"{DEF.UPDATE_DB_SCORE}.{MyStatus.Wins}.{MyStatus.WinningStreak}.{MyStatus.LongestWinningStreak}.{MyStatus.Loses}.{MyStatus.Abandons}.{result}");
        }

        private void GAMEOVER(string WIN_OR_LOSE)
        {
            //Console.WriteLine("GAME_OVER");
            timerGame.Stop();
            MyTurn = null;
            safeLeave = true;
            buttonRematch.BackColor = Color.Transparent;
            buttonRematch.Enabled = true;
            buttonExitMatch.Visible = true;
            labelWaitForOpponent.Visible = false;
            panelGameOver.UseWaitCursor = false;
            panelGameOver.Visible = true;
            ME.Panel.Controls[2].BackColor = OPPONENT.Panel.Controls[2].BackColor = Color.FromArgb(117, 76, 36);
            if (WIN_OR_LOSE == WIN)
            {
                PlaySound(SOUND_ENDGAME_WIN);
                ME.Panel.Controls[2].Text = (int.Parse(ME.Panel.Controls[2].Text) + 1).ToString();
                labelGameResult.Text = "YOU WON!";
                SendToServer(DEF.GAME_OVER);

                if(ME.GetPlayerType() == DEF.TYPE_PLAYER)
                    UpdateMyScore(WIN);
            }
            else
            {
                PlaySound(SOUND_ENDGAME_LOST);
                OPPONENT.Panel.Controls[2].Text = (int.Parse(OPPONENT.Panel.Controls[2].Text) + 1).ToString();
                labelGameResult.Text = "YOU LOST!";

                if (ME.GetPlayerType() == DEF.TYPE_PLAYER)
                    UpdateMyScore(LOSE);
            }
        }

        public void GAMEOVER_OpponentCrashed()
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                PlaySound(SOUND_ENDGAME_LOST);
                formLayoutPanel.Enabled = false;
                timerGame.Stop();
                forcedExit = true;
                labelMessageExitMatch.Text = $"Connection error for {OPPONENT.Name}.\nThe match will not be counted.";
                panelExitMatch.Visible = true;
            }));
        }

        private void ButtonRematch_Click(object sender, EventArgs e)
        {
            buttonRematch.BackColor = Color.RosyBrown;
            buttonRematch.Enabled = false;
            buttonExitMatch.Visible = false;
            labelWaitForOpponent.Visible = true;
            panelGameOver.UseWaitCursor = true;
            SendToServer(DEF.REMATCH);

            if(rematch)
            {
                if (labelGameResult.Text == "YOU WON!")
                    SetRematch(true);
                else
                    SetRematch(false);
            }
        }

        private void ButtonExitGame_Click(object sender, EventArgs e)
        {
            if(!labelOpponentLeftMatch.Visible)
                SendToServer(DEF.GAME_END);
            this.Close();
        }
        #endregion

        private void ChangeTurn()
        {
            //Console.WriteLine("CHANGETURN");

            moves = 0;
            MyTurn = false;
            BlockDice();
            SendToServer(DEF.CHANGE_TURN);
        }

        #region DICE
        private void ButtonRollDice_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                RollDice();
            }
        }

        private void ButtonRollDice_MouseLeave(object sender, EventArgs e)
        {
            this.ActiveControl = GameTable;
        }

        private void RollDice()
        {
            if (MyTurn == true && moves == 0 && !backgroundWorker_Dice.IsBusy)
            {
                buttonRollDice.Enabled = false;
                int dice1 = random.Next(1, 7);
                int dice2 = random.Next(1, 7);

                //int dice1 = Convert.ToInt32(r1.Text);
                //int dice2 = Convert.ToInt32(r2.Text);

                SendToServer($"{DEF.DICE}.{dice1}.{dice2}");
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
            else //OPPONENT
            {
                OPPONENT.CountPossibleMoves = 0;
                GameTable.SetPossibleMoves(OPPONENT);
                if (OPPONENT.CountPossibleMoves == 0)
                    NoMovesPossible(true);
            }
        }
        #endregion

        private void Button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(moves);
            Console.WriteLine("D1: " + Dice1.DiceValue);
            Console.WriteLine("D2: " + Dice2.DiceValue);
        }
    }
}