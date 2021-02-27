namespace Backgammon
{
    partial class GameFormComputer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameFormComputer));
            this.formLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panelPlayers = new System.Windows.Forms.Panel();
            this.buttonUndo = new System.Windows.Forms.Button();
            this.panelPlayerInfo_2 = new System.Windows.Forms.Panel();
            this.playerName_2 = new System.Windows.Forms.Label();
            this.playerImage_2 = new System.Windows.Forms.PictureBox();
            this.panelPlayerInfo_1 = new System.Windows.Forms.Panel();
            this.playerName_1 = new System.Windows.Forms.Label();
            this.playerImage_1 = new System.Windows.Forms.PictureBox();
            this.panelTopMiddle = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.r1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.r2 = new System.Windows.Forms.TextBox();
            this.buttonGameSound = new System.Windows.Forms.Button();
            this.panelBackgroundGame = new System.Windows.Forms.Panel();
            this.GameTable = new Backgammon.GameTable();
            this.Dice3 = new Backgammon.Dice();
            this.Dice1 = new Backgammon.Dice();
            this.Dice2 = new Backgammon.Dice();
            this.Dice4 = new Backgammon.Dice();
            this.buttonRollDice = new System.Windows.Forms.Button();
            this.panelOut = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panelTopLeft = new System.Windows.Forms.Panel();
            this.labelTime = new System.Windows.Forms.Label();
            this.labelTm = new System.Windows.Forms.Label();
            this.backgroundWorker_Dice = new System.ComponentModel.BackgroundWorker();
            this.panelGameOver = new System.Windows.Forms.Panel();
            this.buttonRematch = new System.Windows.Forms.Button();
            this.labelGameResult = new System.Windows.Forms.Label();
            this.buttonExitMatch = new System.Windows.Forms.Button();
            this.panelNoPossibleMoves = new System.Windows.Forms.Panel();
            this.labelNoMovesPossible = new System.Windows.Forms.Label();
            this.timerGame = new System.Windows.Forms.Timer(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.formLayoutPanel.SuspendLayout();
            this.panelPlayers.SuspendLayout();
            this.panelPlayerInfo_2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playerImage_2)).BeginInit();
            this.panelPlayerInfo_1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playerImage_1)).BeginInit();
            this.panelTopMiddle.SuspendLayout();
            this.panelBackgroundGame.SuspendLayout();
            this.GameTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dice3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dice1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dice2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dice4)).BeginInit();
            this.panelTopLeft.SuspendLayout();
            this.panelGameOver.SuspendLayout();
            this.panelNoPossibleMoves.SuspendLayout();
            this.SuspendLayout();
            // 
            // formLayoutPanel
            // 
            this.formLayoutPanel.BackColor = System.Drawing.SystemColors.Window;
            this.formLayoutPanel.ColumnCount = 2;
            this.formLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.formLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.formLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.formLayoutPanel.Controls.Add(this.panelPlayers, 0, 1);
            this.formLayoutPanel.Controls.Add(this.panelTopMiddle, 1, 0);
            this.formLayoutPanel.Controls.Add(this.panelBackgroundGame, 1, 1);
            this.formLayoutPanel.Controls.Add(this.panelTopLeft, 0, 0);
            this.formLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formLayoutPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.formLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.formLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.formLayoutPanel.Name = "formLayoutPanel";
            this.formLayoutPanel.RowCount = 2;
            this.formLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.formLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.formLayoutPanel.Size = new System.Drawing.Size(872, 646);
            this.formLayoutPanel.TabIndex = 0;
            // 
            // panelPlayers
            // 
            this.panelPlayers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(142)))), ((int)(((byte)(106)))));
            this.panelPlayers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPlayers.Controls.Add(this.buttonUndo);
            this.panelPlayers.Controls.Add(this.panelPlayerInfo_2);
            this.panelPlayers.Controls.Add(this.panelPlayerInfo_1);
            this.panelPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPlayers.Location = new System.Drawing.Point(0, 45);
            this.panelPlayers.Margin = new System.Windows.Forms.Padding(0);
            this.panelPlayers.Name = "panelPlayers";
            this.panelPlayers.Size = new System.Drawing.Size(85, 601);
            this.panelPlayers.TabIndex = 2;
            // 
            // buttonUndo
            // 
            this.buttonUndo.BackColor = System.Drawing.Color.White;
            this.buttonUndo.Location = new System.Drawing.Point(4, 285);
            this.buttonUndo.Name = "buttonUndo";
            this.buttonUndo.Size = new System.Drawing.Size(75, 29);
            this.buttonUndo.TabIndex = 3;
            this.buttonUndo.Text = "Undo";
            this.buttonUndo.UseVisualStyleBackColor = false;
            this.buttonUndo.Visible = false;
            this.buttonUndo.Click += new System.EventHandler(this.ButtonUndo_Click);
            // 
            // panelPlayerInfo_2
            // 
            this.panelPlayerInfo_2.Controls.Add(this.playerName_2);
            this.panelPlayerInfo_2.Controls.Add(this.playerImage_2);
            this.panelPlayerInfo_2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPlayerInfo_2.Location = new System.Drawing.Point(0, 323);
            this.panelPlayerInfo_2.Name = "panelPlayerInfo_2";
            this.panelPlayerInfo_2.Size = new System.Drawing.Size(83, 276);
            this.panelPlayerInfo_2.TabIndex = 1;
            // 
            // playerName_2
            // 
            this.playerName_2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.playerName_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerName_2.ForeColor = System.Drawing.Color.White;
            this.playerName_2.Location = new System.Drawing.Point(0, 89);
            this.playerName_2.Name = "playerName_2";
            this.playerName_2.Size = new System.Drawing.Size(83, 23);
            this.playerName_2.TabIndex = 2;
            this.playerName_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // playerImage_2
            // 
            this.playerImage_2.Location = new System.Drawing.Point(13, 117);
            this.playerImage_2.Name = "playerImage_2";
            this.playerImage_2.Size = new System.Drawing.Size(57, 56);
            this.playerImage_2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.playerImage_2.TabIndex = 1;
            this.playerImage_2.TabStop = false;
            // 
            // panelPlayerInfo_1
            // 
            this.panelPlayerInfo_1.Controls.Add(this.playerName_1);
            this.panelPlayerInfo_1.Controls.Add(this.playerImage_1);
            this.panelPlayerInfo_1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPlayerInfo_1.Location = new System.Drawing.Point(0, 0);
            this.panelPlayerInfo_1.Name = "panelPlayerInfo_1";
            this.panelPlayerInfo_1.Size = new System.Drawing.Size(83, 276);
            this.panelPlayerInfo_1.TabIndex = 0;
            // 
            // playerName_1
            // 
            this.playerName_1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.playerName_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerName_1.ForeColor = System.Drawing.Color.White;
            this.playerName_1.Location = new System.Drawing.Point(0, 164);
            this.playerName_1.Name = "playerName_1";
            this.playerName_1.Size = new System.Drawing.Size(83, 23);
            this.playerName_1.TabIndex = 1;
            this.playerName_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // playerImage_1
            // 
            this.playerImage_1.Location = new System.Drawing.Point(13, 103);
            this.playerImage_1.Name = "playerImage_1";
            this.playerImage_1.Size = new System.Drawing.Size(57, 56);
            this.playerImage_1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.playerImage_1.TabIndex = 0;
            this.playerImage_1.TabStop = false;
            // 
            // panelTopMiddle
            // 
            this.panelTopMiddle.BackColor = System.Drawing.Color.Linen;
            this.panelTopMiddle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTopMiddle.Controls.Add(this.button2);
            this.panelTopMiddle.Controls.Add(this.r1);
            this.panelTopMiddle.Controls.Add(this.button1);
            this.panelTopMiddle.Controls.Add(this.r2);
            this.panelTopMiddle.Controls.Add(this.buttonGameSound);
            this.panelTopMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTopMiddle.Location = new System.Drawing.Point(85, 0);
            this.panelTopMiddle.Margin = new System.Windows.Forms.Padding(0);
            this.panelTopMiddle.Name = "panelTopMiddle";
            this.panelTopMiddle.Size = new System.Drawing.Size(787, 45);
            this.panelTopMiddle.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(83, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // r1
            // 
            this.r1.Location = new System.Drawing.Point(232, 11);
            this.r1.Name = "r1";
            this.r1.Size = new System.Drawing.Size(100, 20);
            this.r1.TabIndex = 8;
            this.r1.Text = "2";
            this.r1.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(475, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // r2
            // 
            this.r2.Location = new System.Drawing.Point(353, 11);
            this.r2.Name = "r2";
            this.r2.Size = new System.Drawing.Size(100, 20);
            this.r2.TabIndex = 6;
            this.r2.Text = "3";
            this.r2.Visible = false;
            // 
            // buttonGameSound
            // 
            this.buttonGameSound.BackgroundImage = global::Backgammon.Properties.Resources.SoundON;
            this.buttonGameSound.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonGameSound.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonGameSound.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonGameSound.FlatAppearance.BorderSize = 0;
            this.buttonGameSound.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.buttonGameSound.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.buttonGameSound.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGameSound.Location = new System.Drawing.Point(0, 0);
            this.buttonGameSound.Name = "buttonGameSound";
            this.buttonGameSound.Size = new System.Drawing.Size(43, 43);
            this.buttonGameSound.TabIndex = 5;
            this.buttonGameSound.Tag = "";
            this.buttonGameSound.UseVisualStyleBackColor = true;
            this.buttonGameSound.Click += new System.EventHandler(this.ButtonGameSound_Click);
            // 
            // panelBackgroundGame
            // 
            this.panelBackgroundGame.BackColor = System.Drawing.Color.Transparent;
            this.panelBackgroundGame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBackgroundGame.Controls.Add(this.GameTable);
            this.panelBackgroundGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBackgroundGame.Location = new System.Drawing.Point(85, 45);
            this.panelBackgroundGame.Margin = new System.Windows.Forms.Padding(0);
            this.panelBackgroundGame.Name = "panelBackgroundGame";
            this.panelBackgroundGame.Size = new System.Drawing.Size(787, 601);
            this.panelBackgroundGame.TabIndex = 4;
            // 
            // GameTable
            // 
            this.GameTable.BackColor = System.Drawing.Color.Linen;
            this.GameTable.BackgroundImage = global::Backgammon.Properties.Resources.gameTableBackground_Right;
            this.GameTable.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.GameTable.ColumnCount = 16;
            this.GameTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.GameTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142148F));
            this.GameTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142148F));
            this.GameTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142148F));
            this.GameTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142148F));
            this.GameTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142148F));
            this.GameTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142148F));
            this.GameTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142148F));
            this.GameTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142148F));
            this.GameTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142148F));
            this.GameTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142148F));
            this.GameTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142148F));
            this.GameTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142148F));
            this.GameTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142148F));
            this.GameTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142148F));
            this.GameTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.009899998F));
            this.GameTable.Controls.Add(this.Dice3, 7, 5);
            this.GameTable.Controls.Add(this.Dice1, 7, 6);
            this.GameTable.Controls.Add(this.Dice2, 7, 8);
            this.GameTable.Controls.Add(this.Dice4, 7, 9);
            this.GameTable.Controls.Add(this.buttonRollDice, 7, 7);
            this.GameTable.Controls.Add(this.panelOut, 14, 7);
            this.GameTable.Controls.Add(this.panel12, 12, 7);
            this.GameTable.Controls.Add(this.panel1, 1, 7);
            this.GameTable.Controls.Add(this.panel3, 3, 7);
            this.GameTable.Controls.Add(this.panel2, 2, 7);
            this.GameTable.Controls.Add(this.panel4, 4, 7);
            this.GameTable.Controls.Add(this.panel11, 11, 7);
            this.GameTable.Controls.Add(this.panel13, 13, 7);
            this.GameTable.Controls.Add(this.panel5, 5, 7);
            this.GameTable.Controls.Add(this.panel6, 6, 7);
            this.GameTable.Controls.Add(this.panel8, 8, 7);
            this.GameTable.Controls.Add(this.panel9, 9, 7);
            this.GameTable.Controls.Add(this.panel10, 10, 7);
            this.GameTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GameTable.Location = new System.Drawing.Point(0, 0);
            this.GameTable.Margin = new System.Windows.Forms.Padding(0);
            this.GameTable.MinimumSize = new System.Drawing.Size(785, 599);
            this.GameTable.Name = "GameTable";
            this.GameTable.RowCount = 15;
            this.GameTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.GameTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.691999F));
            this.GameTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.691999F));
            this.GameTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.691999F));
            this.GameTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.691999F));
            this.GameTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.691999F));
            this.GameTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.691999F));
            this.GameTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.691999F));
            this.GameTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.691999F));
            this.GameTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.691999F));
            this.GameTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.691999F));
            this.GameTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.691999F));
            this.GameTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.691999F));
            this.GameTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.691999F));
            this.GameTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 0.004000002F));
            this.GameTable.Size = new System.Drawing.Size(785, 599);
            this.GameTable.TabIndex = 0;
            this.GameTable.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GameTable_MouseClick);
            // 
            // Dice3
            // 
            this.Dice3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Dice3.BackColor = System.Drawing.Color.White;
            this.Dice3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Dice3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Dice3.DiceBlocked = false;
            this.Dice3.DiceValue = 0;
            this.Dice3.Location = new System.Drawing.Point(343, 186);
            this.Dice3.Margin = new System.Windows.Forms.Padding(0);
            this.Dice3.Name = "Dice3";
            this.Dice3.Size = new System.Drawing.Size(42, 42);
            this.Dice3.TabIndex = 2;
            this.Dice3.TabStop = false;
            this.Dice3.Visible = false;
            // 
            // Dice1
            // 
            this.Dice1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Dice1.BackColor = System.Drawing.Color.White;
            this.Dice1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Dice1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Dice1.DiceBlocked = false;
            this.Dice1.DiceValue = 0;
            this.Dice1.Location = new System.Drawing.Point(343, 232);
            this.Dice1.Margin = new System.Windows.Forms.Padding(0);
            this.Dice1.Name = "Dice1";
            this.Dice1.Size = new System.Drawing.Size(42, 42);
            this.Dice1.TabIndex = 2;
            this.Dice1.TabStop = false;
            this.Dice1.Visible = false;
            // 
            // Dice2
            // 
            this.Dice2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Dice2.BackColor = System.Drawing.Color.White;
            this.Dice2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Dice2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Dice2.DiceBlocked = false;
            this.Dice2.DiceValue = 0;
            this.Dice2.Location = new System.Drawing.Point(343, 324);
            this.Dice2.Margin = new System.Windows.Forms.Padding(0);
            this.Dice2.Name = "Dice2";
            this.Dice2.Size = new System.Drawing.Size(42, 42);
            this.Dice2.TabIndex = 1;
            this.Dice2.TabStop = false;
            this.Dice2.Visible = false;
            // 
            // Dice4
            // 
            this.Dice4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Dice4.BackColor = System.Drawing.Color.White;
            this.Dice4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Dice4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Dice4.DiceBlocked = false;
            this.Dice4.DiceValue = 0;
            this.Dice4.Location = new System.Drawing.Point(343, 370);
            this.Dice4.Margin = new System.Windows.Forms.Padding(0);
            this.Dice4.Name = "Dice4";
            this.Dice4.Size = new System.Drawing.Size(42, 42);
            this.Dice4.TabIndex = 2;
            this.Dice4.TabStop = false;
            this.Dice4.Visible = false;
            // 
            // buttonRollDice
            // 
            this.buttonRollDice.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonRollDice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRollDice.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(222)))), ((int)(((byte)(216)))));
            this.buttonRollDice.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.buttonRollDice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRollDice.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRollDice.Location = new System.Drawing.Point(339, 279);
            this.buttonRollDice.Name = "buttonRollDice";
            this.buttonRollDice.Size = new System.Drawing.Size(50, 39);
            this.buttonRollDice.TabIndex = 0;
            this.buttonRollDice.TabStop = false;
            this.buttonRollDice.Text = "Roll Dice";
            this.buttonRollDice.UseVisualStyleBackColor = true;
            this.buttonRollDice.Visible = false;
            this.buttonRollDice.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ButtonRollDice_MouseClick);
            this.buttonRollDice.MouseLeave += new System.EventHandler(this.ButtonRollDice_MouseLeave);
            // 
            // panelOut
            // 
            this.panelOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panelOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(142)))), ((int)(((byte)(106)))));
            this.panelOut.Location = new System.Drawing.Point(729, 276);
            this.panelOut.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.panelOut.Name = "panelOut";
            this.panelOut.Size = new System.Drawing.Size(54, 46);
            this.panelOut.TabIndex = 7;
            // 
            // panel12
            // 
            this.panel12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panel12.BackColor = System.Drawing.Color.Linen;
            this.panel12.Location = new System.Drawing.Point(616, 276);
            this.panel12.Margin = new System.Windows.Forms.Padding(0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(56, 46);
            this.panel12.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panel1.BackColor = System.Drawing.Color.Linen;
            this.panel1.Location = new System.Drawing.Point(0, 276);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(56, 46);
            this.panel1.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panel3.BackColor = System.Drawing.Color.Linen;
            this.panel3.Location = new System.Drawing.Point(112, 276);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(56, 46);
            this.panel3.TabIndex = 11;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panel2.BackColor = System.Drawing.Color.Linen;
            this.panel2.Location = new System.Drawing.Point(56, 276);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(56, 46);
            this.panel2.TabIndex = 12;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panel4.BackColor = System.Drawing.Color.Linen;
            this.panel4.Location = new System.Drawing.Point(168, 276);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(56, 46);
            this.panel4.TabIndex = 13;
            // 
            // panel11
            // 
            this.panel11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panel11.BackColor = System.Drawing.Color.Linen;
            this.panel11.Location = new System.Drawing.Point(560, 276);
            this.panel11.Margin = new System.Windows.Forms.Padding(0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(56, 46);
            this.panel11.TabIndex = 14;
            // 
            // panel13
            // 
            this.panel13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panel13.BackColor = System.Drawing.Color.Linen;
            this.panel13.Location = new System.Drawing.Point(672, 276);
            this.panel13.Margin = new System.Windows.Forms.Padding(0);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(56, 46);
            this.panel13.TabIndex = 15;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panel5.BackColor = System.Drawing.Color.Linen;
            this.panel5.Location = new System.Drawing.Point(224, 276);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(56, 46);
            this.panel5.TabIndex = 16;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panel6.BackColor = System.Drawing.Color.Linen;
            this.panel6.Location = new System.Drawing.Point(280, 276);
            this.panel6.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(55, 46);
            this.panel6.TabIndex = 17;
            // 
            // panel8
            // 
            this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panel8.BackColor = System.Drawing.Color.Linen;
            this.panel8.Location = new System.Drawing.Point(393, 276);
            this.panel8.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(55, 46);
            this.panel8.TabIndex = 18;
            // 
            // panel9
            // 
            this.panel9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panel9.BackColor = System.Drawing.Color.Linen;
            this.panel9.Location = new System.Drawing.Point(448, 276);
            this.panel9.Margin = new System.Windows.Forms.Padding(0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(56, 46);
            this.panel9.TabIndex = 19;
            // 
            // panel10
            // 
            this.panel10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panel10.BackColor = System.Drawing.Color.Linen;
            this.panel10.Location = new System.Drawing.Point(504, 276);
            this.panel10.Margin = new System.Windows.Forms.Padding(0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(56, 46);
            this.panel10.TabIndex = 20;
            // 
            // panelTopLeft
            // 
            this.panelTopLeft.BackColor = System.Drawing.Color.Linen;
            this.panelTopLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTopLeft.Controls.Add(this.labelTime);
            this.panelTopLeft.Controls.Add(this.labelTm);
            this.panelTopLeft.Location = new System.Drawing.Point(0, 0);
            this.panelTopLeft.Margin = new System.Windows.Forms.Padding(0);
            this.panelTopLeft.Name = "panelTopLeft";
            this.panelTopLeft.Size = new System.Drawing.Size(85, 45);
            this.panelTopLeft.TabIndex = 5;
            // 
            // labelTime
            // 
            this.labelTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.Location = new System.Drawing.Point(0, 21);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(83, 16);
            this.labelTime.TabIndex = 1;
            this.labelTime.Text = "00:00";
            this.labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTm
            // 
            this.labelTm.AutoSize = true;
            this.labelTm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTm.Location = new System.Drawing.Point(23, 3);
            this.labelTm.Name = "labelTm";
            this.labelTm.Size = new System.Drawing.Size(35, 15);
            this.labelTm.TabIndex = 0;
            this.labelTm.Text = "Time";
            // 
            // backgroundWorker_Dice
            // 
            this.backgroundWorker_Dice.WorkerReportsProgress = true;
            this.backgroundWorker_Dice.WorkerSupportsCancellation = true;
            this.backgroundWorker_Dice.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_Dice_DoWork);
            this.backgroundWorker_Dice.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_Dice_ProgressChanged);
            this.backgroundWorker_Dice.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_Dice_RunWorkerCompleted);
            // 
            // panelGameOver
            // 
            this.panelGameOver.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelGameOver.BackColor = System.Drawing.Color.FloralWhite;
            this.panelGameOver.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelGameOver.Controls.Add(this.buttonRematch);
            this.panelGameOver.Controls.Add(this.labelGameResult);
            this.panelGameOver.Controls.Add(this.buttonExitMatch);
            this.panelGameOver.Location = new System.Drawing.Point(276, 296);
            this.panelGameOver.Name = "panelGameOver";
            this.panelGameOver.Size = new System.Drawing.Size(350, 100);
            this.panelGameOver.TabIndex = 14;
            this.panelGameOver.Visible = false;
            // 
            // buttonRematch
            // 
            this.buttonRematch.BackColor = System.Drawing.Color.Transparent;
            this.buttonRematch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRematch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RosyBrown;
            this.buttonRematch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.buttonRematch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRematch.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRematch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonRematch.Location = new System.Drawing.Point(70, 54);
            this.buttonRematch.Name = "buttonRematch";
            this.buttonRematch.Size = new System.Drawing.Size(100, 30);
            this.buttonRematch.TabIndex = 1;
            this.buttonRematch.Text = "Rematch";
            this.buttonRematch.UseVisualStyleBackColor = false;
            this.buttonRematch.Click += new System.EventHandler(this.ButtonRematch_Click);
            // 
            // labelGameResult
            // 
            this.labelGameResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGameResult.ForeColor = System.Drawing.Color.Maroon;
            this.labelGameResult.Location = new System.Drawing.Point(2, 6);
            this.labelGameResult.Name = "labelGameResult";
            this.labelGameResult.Size = new System.Drawing.Size(344, 36);
            this.labelGameResult.TabIndex = 0;
            this.labelGameResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonExitMatch
            // 
            this.buttonExitMatch.BackColor = System.Drawing.Color.Transparent;
            this.buttonExitMatch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonExitMatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExitMatch.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExitMatch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonExitMatch.Location = new System.Drawing.Point(180, 54);
            this.buttonExitMatch.Name = "buttonExitMatch";
            this.buttonExitMatch.Size = new System.Drawing.Size(100, 30);
            this.buttonExitMatch.TabIndex = 3;
            this.buttonExitMatch.Text = "Exit Match";
            this.buttonExitMatch.UseVisualStyleBackColor = false;
            this.buttonExitMatch.Click += new System.EventHandler(this.ButtonExitGame_Click);
            // 
            // panelNoPossibleMoves
            // 
            this.panelNoPossibleMoves.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelNoPossibleMoves.BackColor = System.Drawing.Color.Peru;
            this.panelNoPossibleMoves.Controls.Add(this.labelNoMovesPossible);
            this.panelNoPossibleMoves.Location = new System.Drawing.Point(482, 317);
            this.panelNoPossibleMoves.Name = "panelNoPossibleMoves";
            this.panelNoPossibleMoves.Size = new System.Drawing.Size(328, 58);
            this.panelNoPossibleMoves.TabIndex = 15;
            this.panelNoPossibleMoves.Visible = false;
            // 
            // labelNoMovesPossible
            // 
            this.labelNoMovesPossible.AutoSize = true;
            this.labelNoMovesPossible.BackColor = System.Drawing.Color.Transparent;
            this.labelNoMovesPossible.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNoMovesPossible.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.labelNoMovesPossible.Location = new System.Drawing.Point(12, 12);
            this.labelNoMovesPossible.Margin = new System.Windows.Forms.Padding(0);
            this.labelNoMovesPossible.Name = "labelNoMovesPossible";
            this.labelNoMovesPossible.Size = new System.Drawing.Size(263, 31);
            this.labelNoMovesPossible.TabIndex = 1;
            this.labelNoMovesPossible.Text = "No Moves Possible";
            // 
            // timerGame
            // 
            this.timerGame.Interval = 1000;
            this.timerGame.Tick += new System.EventHandler(this.TimerGame_Tick);
            // 
            // GameFormComputer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(872, 646);
            this.Controls.Add(this.panelNoPossibleMoves);
            this.Controls.Add(this.panelGameOver);
            this.Controls.Add(this.formLayoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 100);
            this.MinimumSize = new System.Drawing.Size(888, 685);
            this.Name = "GameFormComputer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Backgammon";
            this.Activated += new System.EventHandler(this.GameFormComputer_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameFormComputer_FormClosing);
            this.Load += new System.EventHandler(this.GameFormComputer_Load);
            this.Shown += new System.EventHandler(this.GameFormComputer_Shown);
            this.ResizeEnd += new System.EventHandler(this.GameFormComputer_ResizeEnd);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameFormComputer_KeyDown);
            this.Resize += new System.EventHandler(this.GameFormComputer_Resize);
            this.formLayoutPanel.ResumeLayout(false);
            this.panelPlayers.ResumeLayout(false);
            this.panelPlayerInfo_2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.playerImage_2)).EndInit();
            this.panelPlayerInfo_1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.playerImage_1)).EndInit();
            this.panelTopMiddle.ResumeLayout(false);
            this.panelTopMiddle.PerformLayout();
            this.panelBackgroundGame.ResumeLayout(false);
            this.GameTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dice3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dice1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dice2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dice4)).EndInit();
            this.panelTopLeft.ResumeLayout(false);
            this.panelTopLeft.PerformLayout();
            this.panelGameOver.ResumeLayout(false);
            this.panelNoPossibleMoves.ResumeLayout(false);
            this.panelNoPossibleMoves.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelPlayers;
        private System.Windows.Forms.Panel panelTopMiddle;
        private System.Windows.Forms.Panel panelBackgroundGame;
        private System.Windows.Forms.Panel panelTopLeft;
        protected System.Windows.Forms.TableLayoutPanel formLayoutPanel;
        private GameTable GameTable;
        private Dice Dice3;
        private Dice Dice1;
        private Dice Dice2;
        private Dice Dice4;
        private System.Windows.Forms.Button buttonRollDice;
        private System.ComponentModel.BackgroundWorker backgroundWorker_Dice;
        private System.Windows.Forms.Label labelTm;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Timer timerGame;
        private System.Windows.Forms.Panel panelPlayerInfo_2;
        private System.Windows.Forms.Label playerName_2;
        private System.Windows.Forms.PictureBox playerImage_2;
        private System.Windows.Forms.Panel panelPlayerInfo_1;
        private System.Windows.Forms.Label playerName_1;
        private System.Windows.Forms.PictureBox playerImage_1;
        private System.Windows.Forms.Button buttonGameSound;
        private System.Windows.Forms.Panel panelGameOver;
        private System.Windows.Forms.Panel panelNoPossibleMoves;
        private System.Windows.Forms.Button buttonRematch;
        private System.Windows.Forms.Label labelGameResult;
        private System.Windows.Forms.Button buttonExitMatch;
        private System.Windows.Forms.Panel panelOut;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.TextBox r2;
        private System.Windows.Forms.Label labelNoMovesPossible;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TextBox r1;
        private System.Windows.Forms.Button buttonUndo;
        private System.Windows.Forms.Button button2;
    }
}

