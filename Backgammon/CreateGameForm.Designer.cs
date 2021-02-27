namespace Backgammon
{
    partial class CreateGameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateGameForm));
            this.buttonPlay = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Zone2 = new System.Windows.Forms.PictureBox();
            this.Zone4 = new System.Windows.Forms.PictureBox();
            this.Zone3 = new System.Windows.Forms.PictureBox();
            this.Zone1 = new System.Windows.Forms.PictureBox();
            this.piece2 = new System.Windows.Forms.PictureBox();
            this.piece1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Zone2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Zone4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Zone3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Zone1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.piece2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.piece1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonPlay
            // 
            this.buttonPlay.BackColor = System.Drawing.SystemColors.Window;
            this.buttonPlay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonPlay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Linen;
            this.buttonPlay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.buttonPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.buttonPlay.Location = new System.Drawing.Point(654, 347);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(179, 90);
            this.buttonPlay.TabIndex = 4;
            this.buttonPlay.Text = "PLAY";
            this.buttonPlay.UseVisualStyleBackColor = false;
            this.buttonPlay.Click += new System.EventHandler(this.ButtonPlay_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(24, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "House:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.Location = new System.Drawing.Point(617, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Play as:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Linen;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Controls.Add(this.Zone2);
            this.panel1.Controls.Add(this.Zone4);
            this.panel1.Controls.Add(this.Zone3);
            this.panel1.Controls.Add(this.Zone1);
            this.panel1.Location = new System.Drawing.Point(22, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(547, 440);
            this.panel1.TabIndex = 3;
            // 
            // Zone2
            // 
            this.Zone2.BackColor = System.Drawing.Color.Transparent;
            this.Zone2.BackgroundImage = global::Backgammon.Properties.Resources.zoneNormal;
            this.Zone2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Zone2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Zone2.Location = new System.Drawing.Point(294, 5);
            this.Zone2.Name = "Zone2";
            this.Zone2.Size = new System.Drawing.Size(247, 185);
            this.Zone2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Zone2.TabIndex = 5;
            this.Zone2.TabStop = false;
            this.Zone2.Tag = "2";
            this.Zone2.Click += new System.EventHandler(this.Zone_Click);
            this.Zone2.MouseEnter += new System.EventHandler(this.Zone_MouseEnter);
            this.Zone2.MouseLeave += new System.EventHandler(this.Zone_MouseLeave);
            // 
            // Zone4
            // 
            this.Zone4.BackColor = System.Drawing.Color.Transparent;
            this.Zone4.BackgroundImage = global::Backgammon.Properties.Resources.zoneNormal;
            this.Zone4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Zone4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Zone4.Location = new System.Drawing.Point(294, 251);
            this.Zone4.Name = "Zone4";
            this.Zone4.Size = new System.Drawing.Size(247, 185);
            this.Zone4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Zone4.TabIndex = 4;
            this.Zone4.TabStop = false;
            this.Zone4.Tag = "4";
            this.Zone4.Click += new System.EventHandler(this.Zone_Click);
            this.Zone4.MouseEnter += new System.EventHandler(this.Zone_MouseEnter);
            this.Zone4.MouseLeave += new System.EventHandler(this.Zone_MouseLeave);
            // 
            // Zone3
            // 
            this.Zone3.BackColor = System.Drawing.Color.Transparent;
            this.Zone3.BackgroundImage = global::Backgammon.Properties.Resources.zoneNormal;
            this.Zone3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Zone3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Zone3.Location = new System.Drawing.Point(6, 250);
            this.Zone3.Name = "Zone3";
            this.Zone3.Size = new System.Drawing.Size(247, 185);
            this.Zone3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Zone3.TabIndex = 3;
            this.Zone3.TabStop = false;
            this.Zone3.Tag = "3";
            this.Zone3.Click += new System.EventHandler(this.Zone_Click);
            this.Zone3.MouseEnter += new System.EventHandler(this.Zone_MouseEnter);
            this.Zone3.MouseLeave += new System.EventHandler(this.Zone_MouseLeave);
            // 
            // Zone1
            // 
            this.Zone1.BackColor = System.Drawing.Color.Transparent;
            this.Zone1.BackgroundImage = global::Backgammon.Properties.Resources.zoneNormal;
            this.Zone1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Zone1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Zone1.Location = new System.Drawing.Point(6, 5);
            this.Zone1.Name = "Zone1";
            this.Zone1.Size = new System.Drawing.Size(247, 185);
            this.Zone1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Zone1.TabIndex = 2;
            this.Zone1.TabStop = false;
            this.Zone1.Tag = "1";
            this.Zone1.Click += new System.EventHandler(this.Zone_Click);
            this.Zone1.MouseEnter += new System.EventHandler(this.Zone_MouseEnter);
            this.Zone1.MouseLeave += new System.EventHandler(this.Zone_MouseLeave);
            // 
            // piece2
            // 
            this.piece2.BackgroundImage = global::Backgammon.Properties.Resources.piece2;
            this.piece2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.piece2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.piece2.Location = new System.Drawing.Point(689, 43);
            this.piece2.Name = "piece2";
            this.piece2.Size = new System.Drawing.Size(54, 54);
            this.piece2.TabIndex = 0;
            this.piece2.TabStop = false;
            this.piece2.Tag = "2";
            this.piece2.Click += new System.EventHandler(this.Piece_Click);
            // 
            // piece1
            // 
            this.piece1.BackgroundImage = global::Backgammon.Properties.Resources.piece1;
            this.piece1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.piece1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.piece1.Location = new System.Drawing.Point(621, 43);
            this.piece1.Name = "piece1";
            this.piece1.Size = new System.Drawing.Size(54, 54);
            this.piece1.TabIndex = 0;
            this.piece1.TabStop = false;
            this.piece1.Tag = "1";
            this.piece1.Click += new System.EventHandler(this.Piece_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(621, 454);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "*Who has the first move will be random";
            // 
            // CreateGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(897, 506);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.piece1);
            this.Controls.Add(this.piece2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonPlay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(913, 545);
            this.Name = "CreateGameForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Game Preferences";
            this.Load += new System.EventHandler(this.CreateGameForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Zone2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Zone4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Zone3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Zone1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.piece2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.piece1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox Zone1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox Zone2;
        private System.Windows.Forms.PictureBox Zone4;
        private System.Windows.Forms.PictureBox Zone3;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox piece1;
        private System.Windows.Forms.PictureBox piece2;
        private System.Windows.Forms.Label label1;
    }
}