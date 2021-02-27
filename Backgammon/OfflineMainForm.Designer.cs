namespace Backgammon
{
    partial class OfflineMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OfflineMainForm));
            this.formMenu = new System.Windows.Forms.MenuStrip();
            this.formMenuGame = new System.Windows.Forms.ToolStripMenuItem();
            this.formMenuGame_playOnline = new System.Windows.Forms.ToolStripMenuItem();
            this.formMenuGame_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.formMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.ormMenuHelp_aboutBackgammon = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.labelAuthorQuote = new System.Windows.Forms.Label();
            this.labelQuote = new System.Windows.Forms.Label();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.checkBoxGameSound = new System.Windows.Forms.CheckBox();
            this.formMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // formMenu
            // 
            this.formMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formMenuGame,
            this.formMenuHelp});
            this.formMenu.Location = new System.Drawing.Point(0, 0);
            this.formMenu.Name = "formMenu";
            this.formMenu.Size = new System.Drawing.Size(1184, 24);
            this.formMenu.TabIndex = 0;
            // 
            // formMenuGame
            // 
            this.formMenuGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formMenuGame_playOnline,
            this.formMenuGame_exit});
            this.formMenuGame.Name = "formMenuGame";
            this.formMenuGame.Size = new System.Drawing.Size(50, 20);
            this.formMenuGame.Text = "Game";
            // 
            // formMenuGame_playOnline
            // 
            this.formMenuGame_playOnline.Name = "formMenuGame_playOnline";
            this.formMenuGame_playOnline.Size = new System.Drawing.Size(132, 22);
            this.formMenuGame_playOnline.Text = "Play online";
            this.formMenuGame_playOnline.Click += new System.EventHandler(this.FormMenuGame_playOnline_Click);
            // 
            // formMenuGame_exit
            // 
            this.formMenuGame_exit.Name = "formMenuGame_exit";
            this.formMenuGame_exit.Size = new System.Drawing.Size(132, 22);
            this.formMenuGame_exit.Text = "Exit";
            this.formMenuGame_exit.Click += new System.EventHandler(this.FormMenuGame_exit_Click);
            // 
            // formMenuHelp
            // 
            this.formMenuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ormMenuHelp_aboutBackgammon});
            this.formMenuHelp.Name = "formMenuHelp";
            this.formMenuHelp.Size = new System.Drawing.Size(44, 20);
            this.formMenuHelp.Text = "Help";
            // 
            // ormMenuHelp_aboutBackgammon
            // 
            this.ormMenuHelp_aboutBackgammon.Image = global::Backgammon.Properties.Resources.ic_info;
            this.ormMenuHelp_aboutBackgammon.Name = "ormMenuHelp_aboutBackgammon";
            this.ormMenuHelp_aboutBackgammon.Size = new System.Drawing.Size(184, 22);
            this.ormMenuHelp_aboutBackgammon.Text = "About Backgammon";
            this.ormMenuHelp_aboutBackgammon.Click += new System.EventHandler(this.FormMenuHelp_aboutBackgammon_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Gadugi", 60F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label2.Location = new System.Drawing.Point(65, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(640, 95);
            this.label2.TabIndex = 3;
            this.label2.Text = "BACKGAMMON";
            // 
            // labelAuthorQuote
            // 
            this.labelAuthorQuote.BackColor = System.Drawing.Color.Transparent;
            this.labelAuthorQuote.Font = new System.Drawing.Font("Georgia", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAuthorQuote.ForeColor = System.Drawing.Color.White;
            this.labelAuthorQuote.Location = new System.Drawing.Point(240, 277);
            this.labelAuthorQuote.Name = "labelAuthorQuote";
            this.labelAuthorQuote.Size = new System.Drawing.Size(278, 23);
            this.labelAuthorQuote.TabIndex = 14;
            this.labelAuthorQuote.Text = "- Morgan Wootten -";
            this.labelAuthorQuote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelQuote
            // 
            this.labelQuote.AutoSize = true;
            this.labelQuote.BackColor = System.Drawing.Color.Transparent;
            this.labelQuote.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelQuote.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.labelQuote.Location = new System.Drawing.Point(239, 217);
            this.labelQuote.MaximumSize = new System.Drawing.Size(278, 0);
            this.labelQuote.Name = "labelQuote";
            this.labelQuote.Size = new System.Drawing.Size(274, 42);
            this.labelQuote.TabIndex = 13;
            this.labelQuote.Text = "You learn more from losing than winning. You learn how to keep going.";
            this.labelQuote.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonPlay
            // 
            this.buttonPlay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonPlay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.buttonPlay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.buttonPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPlay.Font = new System.Drawing.Font("Castellar", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPlay.ForeColor = System.Drawing.SystemColors.Desktop;
            this.buttonPlay.Location = new System.Drawing.Point(81, 505);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(290, 72);
            this.buttonPlay.TabIndex = 16;
            this.buttonPlay.Text = "Play";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.ButtonPlay_Click);
            // 
            // checkBoxGameSound
            // 
            this.checkBoxGameSound.AutoSize = true;
            this.checkBoxGameSound.Checked = true;
            this.checkBoxGameSound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxGameSound.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxGameSound.Location = new System.Drawing.Point(461, 535);
            this.checkBoxGameSound.Name = "checkBoxGameSound";
            this.checkBoxGameSound.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.checkBoxGameSound.Size = new System.Drawing.Size(126, 24);
            this.checkBoxGameSound.TabIndex = 17;
            this.checkBoxGameSound.Text = "Game Sound";
            this.checkBoxGameSound.UseVisualStyleBackColor = true;
            this.checkBoxGameSound.CheckedChanged += new System.EventHandler(this.CheckBoxGameSound_CheckedChanged);
            // 
            // OfflineMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Backgammon.Properties.Resources.Wallpaper;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.checkBoxGameSound);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.labelAuthorQuote);
            this.Controls.Add(this.labelQuote);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.formMenu);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.formMenu;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1200, 700);
            this.MinimumSize = new System.Drawing.Size(1200, 700);
            this.Name = "OfflineMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Backgammon";
            this.Load += new System.EventHandler(this.OfflineMainForm_Load);
            this.Shown += new System.EventHandler(this.OfflineMainForm_Shown);
            this.formMenu.ResumeLayout(false);
            this.formMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip formMenu;
        private System.Windows.Forms.ToolStripMenuItem formMenuHelp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelAuthorQuote;
        private System.Windows.Forms.Label labelQuote;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.ToolStripMenuItem ormMenuHelp_aboutBackgammon;
        private System.Windows.Forms.CheckBox checkBoxGameSound;
        private System.Windows.Forms.ToolStripMenuItem formMenuGame;
        private System.Windows.Forms.ToolStripMenuItem formMenuGame_playOnline;
        private System.Windows.Forms.ToolStripMenuItem formMenuGame_exit;
    }
}