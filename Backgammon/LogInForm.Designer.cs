namespace Backgammon
{
    partial class LogInForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogInForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonLogIn = new System.Windows.Forms.Button();
            this.linkLabelCreateAccount = new System.Windows.Forms.LinkLabel();
            this.linkLabelForgotPassword = new System.Windows.Forms.LinkLabel();
            this.loading = new System.Windows.Forms.PictureBox();
            this.panelErrorConnection = new System.Windows.Forms.Panel();
            this.reconnect = new Backgammon.TransparentPanel();
            this.pictureBoxSignal = new System.Windows.Forms.PictureBox();
            this.pictureBoxServer = new System.Windows.Forms.PictureBox();
            this.iconRetry = new System.Windows.Forms.PictureBox();
            this.labelRetry = new System.Windows.Forms.Label();
            this.labelErrorConnection = new System.Windows.Forms.Label();
            this.line = new System.Windows.Forms.PictureBox();
            this.buttonOffline = new System.Windows.Forms.Button();
            this.buttonGuest = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.timerCount = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.loading)).BeginInit();
            this.panelErrorConnection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSignal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxServer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconRetry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.line)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 195);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "User";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 226);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(126, 192);
            this.textBoxUser.MaxLength = 50;
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(150, 21);
            this.textBoxUser.TabIndex = 0;
            this.textBoxUser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LogIn_KeyDown);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(126, 223);
            this.textBoxPassword.MaxLength = 50;
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(150, 21);
            this.textBoxPassword.TabIndex = 1;
            this.textBoxPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LogIn_KeyDown);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Stencil", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label4.Location = new System.Drawing.Point(12, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(500, 57);
            this.label4.TabIndex = 7;
            this.label4.Text = "BACKGAMMON";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(500, 26);
            this.label5.TabIndex = 8;
            this.label5.Text = "Welcome to";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonLogIn
            // 
            this.buttonLogIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonLogIn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.buttonLogIn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.buttonLogIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLogIn.Location = new System.Drawing.Point(163, 263);
            this.buttonLogIn.Name = "buttonLogIn";
            this.buttonLogIn.Size = new System.Drawing.Size(75, 26);
            this.buttonLogIn.TabIndex = 2;
            this.buttonLogIn.Text = "Log In";
            this.buttonLogIn.UseVisualStyleBackColor = true;
            this.buttonLogIn.Click += new System.EventHandler(this.ButtonLogIn_Click);
            // 
            // linkLabelCreateAccount
            // 
            this.linkLabelCreateAccount.AutoSize = true;
            this.linkLabelCreateAccount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabelCreateAccount.Location = new System.Drawing.Point(86, 304);
            this.linkLabelCreateAccount.Name = "linkLabelCreateAccount";
            this.linkLabelCreateAccount.Size = new System.Drawing.Size(115, 15);
            this.linkLabelCreateAccount.TabIndex = 10;
            this.linkLabelCreateAccount.TabStop = true;
            this.linkLabelCreateAccount.Text = "Create new account";
            this.linkLabelCreateAccount.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelCreateAccount_LinkClicked);
            // 
            // linkLabelForgotPassword
            // 
            this.linkLabelForgotPassword.AutoSize = true;
            this.linkLabelForgotPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabelForgotPassword.Location = new System.Drawing.Point(211, 304);
            this.linkLabelForgotPassword.Name = "linkLabelForgotPassword";
            this.linkLabelForgotPassword.Size = new System.Drawing.Size(98, 15);
            this.linkLabelForgotPassword.TabIndex = 11;
            this.linkLabelForgotPassword.TabStop = true;
            this.linkLabelForgotPassword.Text = "Forgot password";
            // 
            // loading
            // 
            this.loading.Image = ((System.Drawing.Image)(resources.GetObject("loading.Image")));
            this.loading.Location = new System.Drawing.Point(1, 162);
            this.loading.Name = "loading";
            this.loading.Size = new System.Drawing.Size(522, 179);
            this.loading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.loading.TabIndex = 12;
            this.loading.TabStop = false;
            this.loading.UseWaitCursor = true;
            // 
            // panelErrorConnection
            // 
            this.panelErrorConnection.Controls.Add(this.reconnect);
            this.panelErrorConnection.Controls.Add(this.pictureBoxSignal);
            this.panelErrorConnection.Controls.Add(this.pictureBoxServer);
            this.panelErrorConnection.Controls.Add(this.iconRetry);
            this.panelErrorConnection.Controls.Add(this.labelRetry);
            this.panelErrorConnection.Controls.Add(this.labelErrorConnection);
            this.panelErrorConnection.Location = new System.Drawing.Point(1, 162);
            this.panelErrorConnection.Name = "panelErrorConnection";
            this.panelErrorConnection.Size = new System.Drawing.Size(332, 179);
            this.panelErrorConnection.TabIndex = 13;
            // 
            // reconnect
            // 
            this.reconnect.BackColor = System.Drawing.Color.White;
            this.reconnect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.reconnect.Location = new System.Drawing.Point(64, 29);
            this.reconnect.Name = "reconnect";
            this.reconnect.Opacity = 0;
            this.reconnect.Size = new System.Drawing.Size(204, 150);
            this.reconnect.TabIndex = 18;
            this.reconnect.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Reconnect_MouseClick);
            // 
            // pictureBoxSignal
            // 
            this.pictureBoxSignal.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxSignal.Image = global::Backgammon.Properties.Resources.signal;
            this.pictureBoxSignal.Location = new System.Drawing.Point(3, 9);
            this.pictureBoxSignal.Name = "pictureBoxSignal";
            this.pictureBoxSignal.Size = new System.Drawing.Size(326, 45);
            this.pictureBoxSignal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxSignal.TabIndex = 19;
            this.pictureBoxSignal.TabStop = false;
            // 
            // pictureBoxServer
            // 
            this.pictureBoxServer.Image = global::Backgammon.Properties.Resources.server;
            this.pictureBoxServer.Location = new System.Drawing.Point(3, 9);
            this.pictureBoxServer.Name = "pictureBoxServer";
            this.pictureBoxServer.Size = new System.Drawing.Size(326, 45);
            this.pictureBoxServer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxServer.TabIndex = 17;
            this.pictureBoxServer.TabStop = false;
            // 
            // iconRetry
            // 
            this.iconRetry.Image = global::Backgammon.Properties.Resources.ic_retry;
            this.iconRetry.Location = new System.Drawing.Point(3, 122);
            this.iconRetry.Name = "iconRetry";
            this.iconRetry.Size = new System.Drawing.Size(326, 16);
            this.iconRetry.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.iconRetry.TabIndex = 16;
            this.iconRetry.TabStop = false;
            // 
            // labelRetry
            // 
            this.labelRetry.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.labelRetry.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(53)))), ((int)(((byte)(90)))));
            this.labelRetry.Location = new System.Drawing.Point(3, 142);
            this.labelRetry.Name = "labelRetry";
            this.labelRetry.Size = new System.Drawing.Size(326, 18);
            this.labelRetry.TabIndex = 15;
            this.labelRetry.Text = "Retry to connect";
            this.labelRetry.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelErrorConnection
            // 
            this.labelErrorConnection.Font = new System.Drawing.Font("Candara", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelErrorConnection.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelErrorConnection.Location = new System.Drawing.Point(3, 61);
            this.labelErrorConnection.Margin = new System.Windows.Forms.Padding(0);
            this.labelErrorConnection.Name = "labelErrorConnection";
            this.labelErrorConnection.Size = new System.Drawing.Size(326, 49);
            this.labelErrorConnection.TabIndex = 14;
            this.labelErrorConnection.Text = "Disconnected from server\r\nPlease check your internet connection";
            this.labelErrorConnection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // line
            // 
            this.line.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.line.Location = new System.Drawing.Point(333, 183);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(1, 145);
            this.line.TabIndex = 14;
            this.line.TabStop = false;
            // 
            // buttonOffline
            // 
            this.buttonOffline.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonOffline.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.buttonOffline.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.buttonOffline.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOffline.Location = new System.Drawing.Point(363, 212);
            this.buttonOffline.Name = "buttonOffline";
            this.buttonOffline.Size = new System.Drawing.Size(121, 35);
            this.buttonOffline.TabIndex = 16;
            this.buttonOffline.Text = "Play OFFLINE";
            this.buttonOffline.UseVisualStyleBackColor = true;
            this.buttonOffline.Click += new System.EventHandler(this.ButtonOffline_Click);
            // 
            // buttonGuest
            // 
            this.buttonGuest.BackColor = System.Drawing.SystemColors.Window;
            this.buttonGuest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonGuest.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.buttonGuest.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.buttonGuest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGuest.Location = new System.Drawing.Point(363, 268);
            this.buttonGuest.Name = "buttonGuest";
            this.buttonGuest.Size = new System.Drawing.Size(121, 35);
            this.buttonGuest.TabIndex = 17;
            this.buttonGuest.Text = "Play as GUEST";
            this.buttonGuest.UseVisualStyleBackColor = false;
            this.buttonGuest.Visible = false;
            this.buttonGuest.Click += new System.EventHandler(this.ButtonGuest_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(363, 366);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "0";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(445, 366);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 19;
            this.button2.Text = "1";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // timerCount
            // 
            this.timerCount.Interval = 1000;
            this.timerCount.Tick += new System.EventHandler(this.TimerCount_Tick);
            // 
            // LogInForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(524, 401);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.loading);
            this.Controls.Add(this.panelErrorConnection);
            this.Controls.Add(this.linkLabelForgotPassword);
            this.Controls.Add(this.linkLabelCreateAccount);
            this.Controls.Add(this.buttonLogIn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.line);
            this.Controls.Add(this.buttonOffline);
            this.Controls.Add(this.buttonGuest);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(540, 440);
            this.MinimumSize = new System.Drawing.Size(540, 440);
            this.Name = "LogInForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Backgammon - Log In";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LogInForm_FormClosing);
            this.Load += new System.EventHandler(this.LogInForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.loading)).EndInit();
            this.panelErrorConnection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSignal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxServer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconRetry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.line)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonLogIn;
        private System.Windows.Forms.LinkLabel linkLabelCreateAccount;
        private System.Windows.Forms.LinkLabel linkLabelForgotPassword;
        public System.Windows.Forms.PictureBox loading;
        private System.Windows.Forms.PictureBox line;
        private System.Windows.Forms.Button buttonOffline;
        private System.Windows.Forms.Panel panelErrorConnection;
        private System.Windows.Forms.Label labelErrorConnection;
        private System.Windows.Forms.PictureBox iconRetry;
        private System.Windows.Forms.Label labelRetry;
        private System.Windows.Forms.PictureBox pictureBoxServer;
        private TransparentPanel reconnect;
        private System.Windows.Forms.Button buttonGuest;
        private System.Windows.Forms.PictureBox pictureBoxSignal;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timerCount;
    }
}