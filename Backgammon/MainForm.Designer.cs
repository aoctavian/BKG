namespace Backgammon
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.formMenu = new System.Windows.Forms.MenuStrip();
            this.formMenuGame = new System.Windows.Forms.ToolStripMenuItem();
            this.formMenuGame_preferences = new System.Windows.Forms.ToolStripMenuItem();
            this.formMenuGame_signOut = new System.Windows.Forms.ToolStripMenuItem();
            this.formMenuGame_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.formMenuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.formMenuOptions_changeName = new System.Windows.Forms.ToolStripMenuItem();
            this.formMenuOptions_changePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.formMenuStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.formMenuStatus_showStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.formMenuStatus_resetYourStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.formMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.formMenuHelp_aboutBackgammon = new System.Windows.Forms.ToolStripMenuItem();
            this.formMenuUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.loading = new System.Windows.Forms.PictureBox();
            this.labelQuote = new System.Windows.Forms.Label();
            this.labelAuthorQuote = new System.Windows.Forms.Label();
            this.contextMenuStripOnlinePlayers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panelLoading = new System.Windows.Forms.Panel();
            this.labelConnecting = new System.Windows.Forms.Label();
            this.panelErrorConnection = new System.Windows.Forms.Panel();
            this.iconRetry = new System.Windows.Forms.PictureBox();
            this.labelErrorConnection = new System.Windows.Forms.Label();
            this.labelRetry = new System.Windows.Forms.Label();
            this.pictureBoxSignal = new System.Windows.Forms.PictureBox();
            this.pictureBoxServer = new System.Windows.Forms.PictureBox();
            this.timerMinimized = new System.Windows.Forms.Timer(this.components);
            this.panelGlobalChat = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chatMessageBox = new System.Windows.Forms.RichTextBox();
            this.panelGlobalChatMessages = new System.Windows.Forms.Panel();
            this.layoutPanelGlobalChatMessages = new System.Windows.Forms.TableLayoutPanel();
            this.buttonGlobalChatSound = new System.Windows.Forms.Button();
            this.buttonHideGlobalChat = new System.Windows.Forms.Button();
            this.panelChatOptions = new System.Windows.Forms.Panel();
            this.buttonShowGlobalChat = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panelPlayAgainstComputer = new System.Windows.Forms.Panel();
            this.line = new System.Windows.Forms.PictureBox();
            this.buttonPlayAgainstComputer = new System.Windows.Forms.PictureBox();
            this.labelPlayAgainstComputer = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridViewOnlinePlayers = new Backgammon.DataGridViewOnlinePlayers();
            this.formMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loading)).BeginInit();
            this.panelLoading.SuspendLayout();
            this.panelErrorConnection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconRetry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSignal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxServer)).BeginInit();
            this.panelGlobalChat.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelGlobalChatMessages.SuspendLayout();
            this.panelChatOptions.SuspendLayout();
            this.panelPlayAgainstComputer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.line)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonPlayAgainstComputer)).BeginInit();
            this.SuspendLayout();
            // 
            // formMenu
            // 
            this.formMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formMenuGame,
            this.formMenuOptions,
            this.formMenuStatus,
            this.formMenuHelp,
            this.formMenuUpdate});
            this.formMenu.Location = new System.Drawing.Point(0, 0);
            this.formMenu.Name = "formMenu";
            this.formMenu.Padding = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.formMenu.Size = new System.Drawing.Size(1184, 24);
            this.formMenu.TabIndex = 0;
            // 
            // formMenuGame
            // 
            this.formMenuGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formMenuGame_preferences,
            this.formMenuGame_signOut,
            this.formMenuGame_exit});
            this.formMenuGame.ForeColor = System.Drawing.SystemColors.ControlText;
            this.formMenuGame.Name = "formMenuGame";
            this.formMenuGame.Size = new System.Drawing.Size(50, 20);
            this.formMenuGame.Text = "Game";
            // 
            // formMenuGame_preferences
            // 
            this.formMenuGame_preferences.Image = global::Backgammon.Properties.Resources.ic_preferences;
            this.formMenuGame_preferences.Name = "formMenuGame_preferences";
            this.formMenuGame_preferences.Size = new System.Drawing.Size(135, 22);
            this.formMenuGame_preferences.Text = "Preferences";
            this.formMenuGame_preferences.Click += new System.EventHandler(this.FormMenuGame_preferences_Click);
            // 
            // formMenuGame_signOut
            // 
            this.formMenuGame_signOut.Name = "formMenuGame_signOut";
            this.formMenuGame_signOut.Size = new System.Drawing.Size(135, 22);
            this.formMenuGame_signOut.Text = "Sign out";
            this.formMenuGame_signOut.Click += new System.EventHandler(this.FormMenuGame_signOut_Click);
            // 
            // formMenuGame_exit
            // 
            this.formMenuGame_exit.Name = "formMenuGame_exit";
            this.formMenuGame_exit.Size = new System.Drawing.Size(135, 22);
            this.formMenuGame_exit.Text = "Exit";
            this.formMenuGame_exit.Click += new System.EventHandler(this.FormMenuGame_exit_Click);
            // 
            // formMenuOptions
            // 
            this.formMenuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formMenuOptions_changeName,
            this.formMenuOptions_changePassword});
            this.formMenuOptions.Name = "formMenuOptions";
            this.formMenuOptions.Size = new System.Drawing.Size(61, 20);
            this.formMenuOptions.Text = "Options";
            // 
            // formMenuOptions_changeName
            // 
            this.formMenuOptions_changeName.Name = "formMenuOptions_changeName";
            this.formMenuOptions_changeName.Size = new System.Drawing.Size(168, 22);
            this.formMenuOptions_changeName.Text = "Change name";
            this.formMenuOptions_changeName.Click += new System.EventHandler(this.FormMenuOptions_changeName_Click);
            // 
            // formMenuOptions_changePassword
            // 
            this.formMenuOptions_changePassword.Name = "formMenuOptions_changePassword";
            this.formMenuOptions_changePassword.Size = new System.Drawing.Size(168, 22);
            this.formMenuOptions_changePassword.Text = "Change password";
            this.formMenuOptions_changePassword.Click += new System.EventHandler(this.FormMenuOptions_changePassword_Click);
            // 
            // formMenuStatus
            // 
            this.formMenuStatus.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formMenuStatus_showStatus,
            this.formMenuStatus_resetYourStatus});
            this.formMenuStatus.Name = "formMenuStatus";
            this.formMenuStatus.Size = new System.Drawing.Size(51, 20);
            this.formMenuStatus.Text = "Status";
            // 
            // formMenuStatus_showStatus
            // 
            this.formMenuStatus_showStatus.Name = "formMenuStatus_showStatus";
            this.formMenuStatus_showStatus.Size = new System.Drawing.Size(163, 22);
            this.formMenuStatus_showStatus.Text = "Show status";
            this.formMenuStatus_showStatus.Click += new System.EventHandler(this.FormMenuShowStatus_Click);
            // 
            // formMenuStatus_resetYourStatus
            // 
            this.formMenuStatus_resetYourStatus.Name = "formMenuStatus_resetYourStatus";
            this.formMenuStatus_resetYourStatus.Size = new System.Drawing.Size(163, 22);
            this.formMenuStatus_resetYourStatus.Text = "Reset your status";
            this.formMenuStatus_resetYourStatus.Click += new System.EventHandler(this.FormMenuStatus_resetYourStatus_Click);
            // 
            // formMenuHelp
            // 
            this.formMenuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formMenuHelp_aboutBackgammon});
            this.formMenuHelp.Name = "formMenuHelp";
            this.formMenuHelp.Size = new System.Drawing.Size(44, 20);
            this.formMenuHelp.Text = "Help";
            // 
            // formMenuHelp_aboutBackgammon
            // 
            this.formMenuHelp_aboutBackgammon.Image = global::Backgammon.Properties.Resources.ic_info;
            this.formMenuHelp_aboutBackgammon.Name = "formMenuHelp_aboutBackgammon";
            this.formMenuHelp_aboutBackgammon.Size = new System.Drawing.Size(184, 22);
            this.formMenuHelp_aboutBackgammon.Text = "About Backgammon";
            this.formMenuHelp_aboutBackgammon.Click += new System.EventHandler(this.FormMenuHelp_aboutBackgammon_Click);
            // 
            // formMenuUpdate
            // 
            this.formMenuUpdate.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.formMenuUpdate.BackColor = System.Drawing.Color.LimeGreen;
            this.formMenuUpdate.Name = "formMenuUpdate";
            this.formMenuUpdate.Size = new System.Drawing.Size(57, 20);
            this.formMenuUpdate.Text = "Update";
            this.formMenuUpdate.Visible = false;
            this.formMenuUpdate.Click += new System.EventHandler(this.FormMenuUpdate_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Stencil Std", 57.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label2.Location = new System.Drawing.Point(65, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(626, 101);
            this.label2.TabIndex = 2;
            this.label2.Text = "BACKGAMMON";
            // 
            // loading
            // 
            this.loading.Image = ((System.Drawing.Image)(resources.GetObject("loading.Image")));
            this.loading.Location = new System.Drawing.Point(3, 148);
            this.loading.Name = "loading";
            this.loading.Size = new System.Drawing.Size(301, 90);
            this.loading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.loading.TabIndex = 7;
            this.loading.TabStop = false;
            this.loading.UseWaitCursor = true;
            // 
            // labelQuote
            // 
            this.labelQuote.AutoSize = true;
            this.labelQuote.BackColor = System.Drawing.Color.Transparent;
            this.labelQuote.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelQuote.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.labelQuote.Location = new System.Drawing.Point(853, 84);
            this.labelQuote.MaximumSize = new System.Drawing.Size(278, 0);
            this.labelQuote.Name = "labelQuote";
            this.labelQuote.Size = new System.Drawing.Size(274, 42);
            this.labelQuote.TabIndex = 10;
            this.labelQuote.Text = "You learn more from losing than winning. You learn how to keep going.";
            this.labelQuote.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelAuthorQuote
            // 
            this.labelAuthorQuote.BackColor = System.Drawing.Color.Transparent;
            this.labelAuthorQuote.Font = new System.Drawing.Font("Georgia", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAuthorQuote.ForeColor = System.Drawing.Color.White;
            this.labelAuthorQuote.Location = new System.Drawing.Point(854, 144);
            this.labelAuthorQuote.Name = "labelAuthorQuote";
            this.labelAuthorQuote.Size = new System.Drawing.Size(278, 23);
            this.labelAuthorQuote.TabIndex = 12;
            this.labelAuthorQuote.Text = "- Morgan Wootten -";
            this.labelAuthorQuote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // contextMenuStripOnlinePlayers
            // 
            this.contextMenuStripOnlinePlayers.Name = "contextMenuStripOnlinePlayers";
            this.contextMenuStripOnlinePlayers.Size = new System.Drawing.Size(61, 4);
            // 
            // panelLoading
            // 
            this.panelLoading.BackColor = System.Drawing.SystemColors.Window;
            this.panelLoading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLoading.Controls.Add(this.labelConnecting);
            this.panelLoading.Controls.Add(this.loading);
            this.panelLoading.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.panelLoading.Location = new System.Drawing.Point(64, 235);
            this.panelLoading.Name = "panelLoading";
            this.panelLoading.Size = new System.Drawing.Size(309, 386);
            this.panelLoading.TabIndex = 22;
            // 
            // labelConnecting
            // 
            this.labelConnecting.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.labelConnecting.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConnecting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(53)))), ((int)(((byte)(90)))));
            this.labelConnecting.Location = new System.Drawing.Point(3, 252);
            this.labelConnecting.Name = "labelConnecting";
            this.labelConnecting.Size = new System.Drawing.Size(301, 30);
            this.labelConnecting.TabIndex = 8;
            this.labelConnecting.Text = "Connecting to server...";
            this.labelConnecting.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelConnecting.Visible = false;
            // 
            // panelErrorConnection
            // 
            this.panelErrorConnection.BackColor = System.Drawing.SystemColors.Window;
            this.panelErrorConnection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelErrorConnection.Controls.Add(this.iconRetry);
            this.panelErrorConnection.Controls.Add(this.labelErrorConnection);
            this.panelErrorConnection.Controls.Add(this.labelRetry);
            this.panelErrorConnection.Controls.Add(this.pictureBoxSignal);
            this.panelErrorConnection.Controls.Add(this.pictureBoxServer);
            this.panelErrorConnection.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.panelErrorConnection.Location = new System.Drawing.Point(64, 236);
            this.panelErrorConnection.Name = "panelErrorConnection";
            this.panelErrorConnection.Size = new System.Drawing.Size(309, 385);
            this.panelErrorConnection.TabIndex = 23;
            this.panelErrorConnection.Visible = false;
            // 
            // iconRetry
            // 
            this.iconRetry.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconRetry.Image = global::Backgammon.Properties.Resources.ic_retry;
            this.iconRetry.Location = new System.Drawing.Point(48, 251);
            this.iconRetry.Name = "iconRetry";
            this.iconRetry.Size = new System.Drawing.Size(213, 33);
            this.iconRetry.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.iconRetry.TabIndex = 2;
            this.iconRetry.TabStop = false;
            this.iconRetry.Visible = false;
            this.iconRetry.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RetryToConnect_MouseClick);
            this.iconRetry.MouseEnter += new System.EventHandler(this.RetryToConnect_MouseEnter);
            this.iconRetry.MouseLeave += new System.EventHandler(this.RetryToConnect_MouseLeave);
            // 
            // labelErrorConnection
            // 
            this.labelErrorConnection.Font = new System.Drawing.Font("Candara", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelErrorConnection.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelErrorConnection.Location = new System.Drawing.Point(3, 184);
            this.labelErrorConnection.Name = "labelErrorConnection";
            this.labelErrorConnection.Size = new System.Drawing.Size(301, 46);
            this.labelErrorConnection.TabIndex = 0;
            this.labelErrorConnection.Text = "No internet connection\r\nConnection lost";
            this.labelErrorConnection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelRetry
            // 
            this.labelRetry.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelRetry.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.labelRetry.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(53)))), ((int)(((byte)(90)))));
            this.labelRetry.Location = new System.Drawing.Point(48, 259);
            this.labelRetry.Name = "labelRetry";
            this.labelRetry.Size = new System.Drawing.Size(213, 60);
            this.labelRetry.TabIndex = 1;
            this.labelRetry.Text = "Retry to connect";
            this.labelRetry.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelRetry.Visible = false;
            this.labelRetry.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RetryToConnect_MouseClick);
            this.labelRetry.MouseEnter += new System.EventHandler(this.RetryToConnect_MouseEnter);
            this.labelRetry.MouseLeave += new System.EventHandler(this.RetryToConnect_MouseLeave);
            // 
            // pictureBoxSignal
            // 
            this.pictureBoxSignal.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxSignal.Image = global::Backgammon.Properties.Resources.signal;
            this.pictureBoxSignal.Location = new System.Drawing.Point(3, 104);
            this.pictureBoxSignal.Name = "pictureBoxSignal";
            this.pictureBoxSignal.Size = new System.Drawing.Size(301, 72);
            this.pictureBoxSignal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxSignal.TabIndex = 3;
            this.pictureBoxSignal.TabStop = false;
            // 
            // pictureBoxServer
            // 
            this.pictureBoxServer.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxServer.Image = global::Backgammon.Properties.Resources.server;
            this.pictureBoxServer.Location = new System.Drawing.Point(3, 104);
            this.pictureBoxServer.Name = "pictureBoxServer";
            this.pictureBoxServer.Size = new System.Drawing.Size(301, 72);
            this.pictureBoxServer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxServer.TabIndex = 3;
            this.pictureBoxServer.TabStop = false;
            this.pictureBoxServer.Visible = false;
            // 
            // timerMinimized
            // 
            this.timerMinimized.Interval = 300000;
            this.timerMinimized.Tick += new System.EventHandler(this.TimerMinimized_Tick);
            // 
            // panelGlobalChat
            // 
            this.panelGlobalChat.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelGlobalChat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelGlobalChat.Controls.Add(this.panel2);
            this.panelGlobalChat.Controls.Add(this.panelGlobalChatMessages);
            this.panelGlobalChat.Location = new System.Drawing.Point(414, 208);
            this.panelGlobalChat.Name = "panelGlobalChat";
            this.panelGlobalChat.Size = new System.Drawing.Size(340, 413);
            this.panelGlobalChat.TabIndex = 25;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.chatMessageBox);
            this.panel2.Location = new System.Drawing.Point(-1, 377);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(340, 35);
            this.panel2.TabIndex = 1;
            // 
            // chatMessageBox
            // 
            this.chatMessageBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.chatMessageBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chatMessageBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chatMessageBox.ForeColor = System.Drawing.Color.DarkGray;
            this.chatMessageBox.Location = new System.Drawing.Point(7, 8);
            this.chatMessageBox.Multiline = false;
            this.chatMessageBox.Name = "chatMessageBox";
            this.chatMessageBox.Size = new System.Drawing.Size(324, 17);
            this.chatMessageBox.TabIndex = 100;
            this.chatMessageBox.Text = "-> Message";
            this.chatMessageBox.TextChanged += new System.EventHandler(this.ChatMessageBox_TextChanged);
            this.chatMessageBox.Enter += new System.EventHandler(this.ChatMessageBox_Enter);
            this.chatMessageBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChatMessageBox_KeyDown);
            this.chatMessageBox.Leave += new System.EventHandler(this.ChatMessageBox_Leave);
            // 
            // panelGlobalChatMessages
            // 
            this.panelGlobalChatMessages.AutoScroll = true;
            this.panelGlobalChatMessages.BackColor = System.Drawing.Color.White;
            this.panelGlobalChatMessages.Controls.Add(this.layoutPanelGlobalChatMessages);
            this.panelGlobalChatMessages.Location = new System.Drawing.Point(0, 0);
            this.panelGlobalChatMessages.Name = "panelGlobalChatMessages";
            this.panelGlobalChatMessages.Size = new System.Drawing.Size(338, 377);
            this.panelGlobalChatMessages.TabIndex = 2;
            // 
            // layoutPanelGlobalChatMessages
            // 
            this.layoutPanelGlobalChatMessages.AutoSize = true;
            this.layoutPanelGlobalChatMessages.BackColor = System.Drawing.Color.White;
            this.layoutPanelGlobalChatMessages.ColumnCount = 1;
            this.layoutPanelGlobalChatMessages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanelGlobalChatMessages.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutPanelGlobalChatMessages.Location = new System.Drawing.Point(0, 0);
            this.layoutPanelGlobalChatMessages.Margin = new System.Windows.Forms.Padding(0);
            this.layoutPanelGlobalChatMessages.Name = "layoutPanelGlobalChatMessages";
            this.layoutPanelGlobalChatMessages.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.layoutPanelGlobalChatMessages.RowCount = 1;
            this.layoutPanelGlobalChatMessages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.layoutPanelGlobalChatMessages.Size = new System.Drawing.Size(338, 3);
            this.layoutPanelGlobalChatMessages.TabIndex = 1;
            // 
            // buttonGlobalChatSound
            // 
            this.buttonGlobalChatSound.BackColor = System.Drawing.SystemColors.Window;
            this.buttonGlobalChatSound.BackgroundImage = global::Backgammon.Properties.Resources.SoundON;
            this.buttonGlobalChatSound.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonGlobalChatSound.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonGlobalChatSound.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.buttonGlobalChatSound.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.buttonGlobalChatSound.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.buttonGlobalChatSound.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGlobalChatSound.Location = new System.Drawing.Point(0, 36);
            this.buttonGlobalChatSound.Name = "buttonGlobalChatSound";
            this.buttonGlobalChatSound.Size = new System.Drawing.Size(45, 45);
            this.buttonGlobalChatSound.TabIndex = 31;
            this.buttonGlobalChatSound.UseVisualStyleBackColor = false;
            this.buttonGlobalChatSound.Click += new System.EventHandler(this.ButtonGlobalChatSound_Click);
            // 
            // buttonHideGlobalChat
            // 
            this.buttonHideGlobalChat.BackColor = System.Drawing.SystemColors.Window;
            this.buttonHideGlobalChat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonHideGlobalChat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonHideGlobalChat.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.buttonHideGlobalChat.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.buttonHideGlobalChat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.buttonHideGlobalChat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonHideGlobalChat.Location = new System.Drawing.Point(0, 0);
            this.buttonHideGlobalChat.Name = "buttonHideGlobalChat";
            this.buttonHideGlobalChat.Size = new System.Drawing.Size(25, 35);
            this.buttonHideGlobalChat.TabIndex = 32;
            this.buttonHideGlobalChat.Text = "<";
            this.toolTip.SetToolTip(this.buttonHideGlobalChat, "Hide chat");
            this.buttonHideGlobalChat.UseVisualStyleBackColor = false;
            this.buttonHideGlobalChat.Click += new System.EventHandler(this.ButtonHideGlobalChat_Click);
            // 
            // panelChatOptions
            // 
            this.panelChatOptions.BackColor = System.Drawing.Color.Transparent;
            this.panelChatOptions.Controls.Add(this.buttonGlobalChatSound);
            this.panelChatOptions.Controls.Add(this.buttonHideGlobalChat);
            this.panelChatOptions.Controls.Add(this.buttonShowGlobalChat);
            this.panelChatOptions.Location = new System.Drawing.Point(756, 208);
            this.panelChatOptions.Name = "panelChatOptions";
            this.panelChatOptions.Size = new System.Drawing.Size(50, 82);
            this.panelChatOptions.TabIndex = 33;
            this.panelChatOptions.Visible = false;
            // 
            // buttonShowGlobalChat
            // 
            this.buttonShowGlobalChat.BackColor = System.Drawing.SystemColors.Window;
            this.buttonShowGlobalChat.BackgroundImage = global::Backgammon.Properties.Resources.ic_showChat;
            this.buttonShowGlobalChat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonShowGlobalChat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonShowGlobalChat.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.buttonShowGlobalChat.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.buttonShowGlobalChat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.buttonShowGlobalChat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonShowGlobalChat.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonShowGlobalChat.Location = new System.Drawing.Point(0, 0);
            this.buttonShowGlobalChat.Name = "buttonShowGlobalChat";
            this.buttonShowGlobalChat.Size = new System.Drawing.Size(40, 35);
            this.buttonShowGlobalChat.TabIndex = 34;
            this.toolTip.SetToolTip(this.buttonShowGlobalChat, "Show chat");
            this.buttonShowGlobalChat.UseVisualStyleBackColor = false;
            this.buttonShowGlobalChat.Visible = false;
            this.buttonShowGlobalChat.Click += new System.EventHandler(this.ButtonShowGlobalChat_Click);
            // 
            // panelPlayAgainstComputer
            // 
            this.panelPlayAgainstComputer.Controls.Add(this.line);
            this.panelPlayAgainstComputer.Controls.Add(this.buttonPlayAgainstComputer);
            this.panelPlayAgainstComputer.Controls.Add(this.labelPlayAgainstComputer);
            this.panelPlayAgainstComputer.Location = new System.Drawing.Point(65, 208);
            this.panelPlayAgainstComputer.Name = "panelPlayAgainstComputer";
            this.panelPlayAgainstComputer.Size = new System.Drawing.Size(307, 29);
            this.panelPlayAgainstComputer.TabIndex = 34;
            // 
            // line
            // 
            this.line.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.line.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.line.Location = new System.Drawing.Point(0, 28);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(307, 1);
            this.line.TabIndex = 3;
            this.line.TabStop = false;
            // 
            // buttonPlayAgainstComputer
            // 
            this.buttonPlayAgainstComputer.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonPlayAgainstComputer.Image = global::Backgammon.Properties.Resources.button_play;
            this.buttonPlayAgainstComputer.Location = new System.Drawing.Point(226, 4);
            this.buttonPlayAgainstComputer.Name = "buttonPlayAgainstComputer";
            this.buttonPlayAgainstComputer.Size = new System.Drawing.Size(78, 22);
            this.buttonPlayAgainstComputer.TabIndex = 2;
            this.buttonPlayAgainstComputer.TabStop = false;
            this.buttonPlayAgainstComputer.Click += new System.EventHandler(this.ButtonPlayAgainstComputer_Click);
            // 
            // labelPlayAgainstComputer
            // 
            this.labelPlayAgainstComputer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlayAgainstComputer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(47)))), ((int)(((byte)(89)))));
            this.labelPlayAgainstComputer.Location = new System.Drawing.Point(5, 4);
            this.labelPlayAgainstComputer.Name = "labelPlayAgainstComputer";
            this.labelPlayAgainstComputer.Size = new System.Drawing.Size(104, 23);
            this.labelPlayAgainstComputer.TabIndex = 0;
            this.labelPlayAgainstComputer.Text = "Play against AI";
            this.labelPlayAgainstComputer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1101, 520);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 35;
            this.button1.Text = "0";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1101, 550);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 36;
            this.button2.Text = "1";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // dataGridViewOnlinePlayers
            // 
            this.dataGridViewOnlinePlayers.Location = new System.Drawing.Point(64, 235);
            this.dataGridViewOnlinePlayers.Name = "dataGridViewOnlinePlayers";
            this.dataGridViewOnlinePlayers.Size = new System.Drawing.Size(309, 386);
            this.dataGridViewOnlinePlayers.TabIndex = 24;
            this.dataGridViewOnlinePlayers.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImage = global::Backgammon.Properties.Resources.Wallpaper;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panelPlayAgainstComputer);
            this.Controls.Add(this.panelLoading);
            this.Controls.Add(this.dataGridViewOnlinePlayers);
            this.Controls.Add(this.panelErrorConnection);
            this.Controls.Add(this.panelChatOptions);
            this.Controls.Add(this.panelGlobalChat);
            this.Controls.Add(this.labelAuthorQuote);
            this.Controls.Add(this.labelQuote);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.formMenu);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 100);
            this.MainMenuStrip = this.formMenu;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1200, 700);
            this.MinimumSize = new System.Drawing.Size(1200, 700);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Backgammon";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.formMenu.ResumeLayout(false);
            this.formMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loading)).EndInit();
            this.panelLoading.ResumeLayout(false);
            this.panelErrorConnection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iconRetry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSignal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxServer)).EndInit();
            this.panelGlobalChat.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panelGlobalChatMessages.ResumeLayout(false);
            this.panelGlobalChatMessages.PerformLayout();
            this.panelChatOptions.ResumeLayout(false);
            this.panelPlayAgainstComputer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.line)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonPlayAgainstComputer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip formMenu;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.PictureBox loading;
        private System.Windows.Forms.Label labelQuote;
        private System.Windows.Forms.Label labelAuthorQuote;
        private System.Windows.Forms.ToolStripMenuItem formMenuStatus;
        private System.Windows.Forms.ToolStripMenuItem formMenuOptions;
        private System.Windows.Forms.ToolStripMenuItem formMenuOptions_changeName;
        private System.Windows.Forms.ToolStripMenuItem formMenuHelp;
        private System.Windows.Forms.ToolStripMenuItem formMenuHelp_aboutBackgammon;
        private System.Windows.Forms.ToolStripMenuItem formMenuStatus_resetYourStatus;
        private System.Windows.Forms.ToolStripMenuItem formMenuGame;
        private System.Windows.Forms.ToolStripMenuItem formMenuGame_preferences;
        private System.Windows.Forms.ToolStripMenuItem formMenuGame_exit;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripOnlinePlayers;
        private System.Windows.Forms.ToolStripMenuItem formMenuGame_signOut;
        private System.Windows.Forms.Panel panelLoading;
        private System.Windows.Forms.Label labelConnecting;
        private System.Windows.Forms.Panel panelErrorConnection;
        private System.Windows.Forms.Label labelRetry;
        private System.Windows.Forms.Label labelErrorConnection;
        private System.Windows.Forms.PictureBox iconRetry;
        private System.Windows.Forms.PictureBox pictureBoxServer;
        private DataGridViewOnlinePlayers dataGridViewOnlinePlayers;
        private System.Windows.Forms.Timer timerMinimized;
        private System.Windows.Forms.Panel panelGlobalChat;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox chatMessageBox;
        private System.Windows.Forms.Panel panelGlobalChatMessages;
        private System.Windows.Forms.TableLayoutPanel layoutPanelGlobalChatMessages;
        private System.Windows.Forms.Button buttonGlobalChatSound;
        private System.Windows.Forms.Button buttonHideGlobalChat;
        private System.Windows.Forms.Panel panelChatOptions;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button buttonShowGlobalChat;
        private System.Windows.Forms.Panel panelPlayAgainstComputer;
        private System.Windows.Forms.Label labelPlayAgainstComputer;
        private System.Windows.Forms.PictureBox buttonPlayAgainstComputer;
        private System.Windows.Forms.PictureBox line;
        private System.Windows.Forms.ToolStripMenuItem formMenuStatus_showStatus;
        private System.Windows.Forms.ToolStripMenuItem formMenuOptions_changePassword;
        private System.Windows.Forms.PictureBox pictureBoxSignal;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem formMenuUpdate;
    }
}