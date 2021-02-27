namespace Backgammon
{
    partial class StatusForm
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
            this.labelWins = new System.Windows.Forms.Label();
            this.labelLongestWinningStreak = new System.Windows.Forms.Label();
            this.labelWinningStreak = new System.Windows.Forms.Label();
            this.labelLoses = new System.Windows.Forms.Label();
            this.labelAbandons = new System.Windows.Forms.Label();
            this.labelAllGames = new System.Windows.Forms.Label();
            this.dataGridViewAllGames = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonFilters = new System.Windows.Forms.Button();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.radioButtonAbandons = new System.Windows.Forms.RadioButton();
            this.radioButtonAll = new System.Windows.Forms.RadioButton();
            this.radioButtonLoses = new System.Windows.Forms.RadioButton();
            this.radioButtonWins = new System.Windows.Forms.RadioButton();
            this.textBoxFilterOpponent = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAllGames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelWins
            // 
            this.labelWins.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWins.Location = new System.Drawing.Point(12, 12);
            this.labelWins.Name = "labelWins";
            this.labelWins.Size = new System.Drawing.Size(510, 20);
            this.labelWins.TabIndex = 0;
            this.labelWins.Text = "Wins: ";
            this.labelWins.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelLongestWinningStreak
            // 
            this.labelLongestWinningStreak.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLongestWinningStreak.Location = new System.Drawing.Point(12, 62);
            this.labelLongestWinningStreak.Name = "labelLongestWinningStreak";
            this.labelLongestWinningStreak.Size = new System.Drawing.Size(510, 20);
            this.labelLongestWinningStreak.TabIndex = 1;
            this.labelLongestWinningStreak.Text = "Longest winning streak: ";
            this.labelLongestWinningStreak.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelWinningStreak
            // 
            this.labelWinningStreak.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWinningStreak.Location = new System.Drawing.Point(12, 37);
            this.labelWinningStreak.Name = "labelWinningStreak";
            this.labelWinningStreak.Size = new System.Drawing.Size(510, 20);
            this.labelWinningStreak.TabIndex = 2;
            this.labelWinningStreak.Text = "Winning streak: ";
            this.labelWinningStreak.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelLoses
            // 
            this.labelLoses.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLoses.Location = new System.Drawing.Point(12, 87);
            this.labelLoses.Name = "labelLoses";
            this.labelLoses.Size = new System.Drawing.Size(510, 20);
            this.labelLoses.TabIndex = 3;
            this.labelLoses.Text = "Loses: ";
            this.labelLoses.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelAbandons
            // 
            this.labelAbandons.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAbandons.Location = new System.Drawing.Point(12, 112);
            this.labelAbandons.Name = "labelAbandons";
            this.labelAbandons.Size = new System.Drawing.Size(510, 20);
            this.labelAbandons.TabIndex = 4;
            this.labelAbandons.Text = "Abandons: ";
            this.labelAbandons.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelAllGames
            // 
            this.labelAllGames.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAllGames.Location = new System.Drawing.Point(12, 165);
            this.labelAllGames.Name = "labelAllGames";
            this.labelAllGames.Size = new System.Drawing.Size(510, 20);
            this.labelAllGames.TabIndex = 6;
            this.labelAllGames.Text = "All games ";
            this.labelAllGames.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridViewAllGames
            // 
            this.dataGridViewAllGames.AllowUserToAddRows = false;
            this.dataGridViewAllGames.AllowUserToDeleteRows = false;
            this.dataGridViewAllGames.AllowUserToResizeRows = false;
            this.dataGridViewAllGames.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewAllGames.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridViewAllGames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAllGames.Location = new System.Drawing.Point(16, 189);
            this.dataGridViewAllGames.MultiSelect = false;
            this.dataGridViewAllGames.Name = "dataGridViewAllGames";
            this.dataGridViewAllGames.ReadOnly = true;
            this.dataGridViewAllGames.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridViewAllGames.RowHeadersVisible = false;
            this.dataGridViewAllGames.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAllGames.Size = new System.Drawing.Size(506, 158);
            this.dataGridViewAllGames.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Gainsboro;
            this.pictureBox1.Location = new System.Drawing.Point(12, 146);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(510, 1);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // buttonFilters
            // 
            this.buttonFilters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFilters.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFilters.Location = new System.Drawing.Point(16, 160);
            this.buttonFilters.Name = "buttonFilters";
            this.buttonFilters.Size = new System.Drawing.Size(54, 25);
            this.buttonFilters.TabIndex = 9;
            this.buttonFilters.Text = "Filters";
            this.buttonFilters.UseVisualStyleBackColor = true;
            this.buttonFilters.Click += new System.EventHandler(this.ButtonFilters_Click);
            // 
            // panelFilters
            // 
            this.panelFilters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFilters.Controls.Add(this.dateTimePicker);
            this.panelFilters.Controls.Add(this.radioButtonAbandons);
            this.panelFilters.Controls.Add(this.radioButtonAll);
            this.panelFilters.Controls.Add(this.radioButtonLoses);
            this.panelFilters.Controls.Add(this.radioButtonWins);
            this.panelFilters.Controls.Add(this.textBoxFilterOpponent);
            this.panelFilters.Location = new System.Drawing.Point(16, 189);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new System.Drawing.Size(506, 45);
            this.panelFilters.TabIndex = 10;
            this.panelFilters.Visible = false;
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Checked = false;
            this.dateTimePicker.Location = new System.Drawing.Point(9, 12);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.ShowCheckBox = true;
            this.dateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker.TabIndex = 6;
            this.dateTimePicker.ValueChanged += new System.EventHandler(this.DateTimePicker_ValueChanged);
            // 
            // radioButtonAbandons
            // 
            this.radioButtonAbandons.AutoSize = true;
            this.radioButtonAbandons.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonAbandons.Location = new System.Drawing.Point(422, 22);
            this.radioButtonAbandons.Name = "radioButtonAbandons";
            this.radioButtonAbandons.Size = new System.Drawing.Size(80, 19);
            this.radioButtonAbandons.TabIndex = 5;
            this.radioButtonAbandons.TabStop = true;
            this.radioButtonAbandons.Text = "Abandons";
            this.radioButtonAbandons.UseVisualStyleBackColor = true;
            this.radioButtonAbandons.Click += new System.EventHandler(this.RadioButtonResult_Click);
            // 
            // radioButtonAll
            // 
            this.radioButtonAll.AutoSize = true;
            this.radioButtonAll.Checked = true;
            this.radioButtonAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonAll.Location = new System.Drawing.Point(353, 2);
            this.radioButtonAll.Name = "radioButtonAll";
            this.radioButtonAll.Size = new System.Drawing.Size(38, 19);
            this.radioButtonAll.TabIndex = 4;
            this.radioButtonAll.TabStop = true;
            this.radioButtonAll.Text = "All";
            this.radioButtonAll.UseVisualStyleBackColor = true;
            this.radioButtonAll.Click += new System.EventHandler(this.RadioButtonResult_Click);
            // 
            // radioButtonLoses
            // 
            this.radioButtonLoses.AutoSize = true;
            this.radioButtonLoses.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonLoses.Location = new System.Drawing.Point(422, 2);
            this.radioButtonLoses.Name = "radioButtonLoses";
            this.radioButtonLoses.Size = new System.Drawing.Size(58, 19);
            this.radioButtonLoses.TabIndex = 3;
            this.radioButtonLoses.Text = "Loses";
            this.radioButtonLoses.UseVisualStyleBackColor = true;
            this.radioButtonLoses.Click += new System.EventHandler(this.RadioButtonResult_Click);
            // 
            // radioButtonWins
            // 
            this.radioButtonWins.AutoSize = true;
            this.radioButtonWins.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonWins.Location = new System.Drawing.Point(353, 22);
            this.radioButtonWins.Name = "radioButtonWins";
            this.radioButtonWins.Size = new System.Drawing.Size(52, 19);
            this.radioButtonWins.TabIndex = 2;
            this.radioButtonWins.Text = "Wins";
            this.radioButtonWins.UseVisualStyleBackColor = true;
            this.radioButtonWins.Click += new System.EventHandler(this.RadioButtonResult_Click);
            // 
            // textBoxFilterOpponent
            // 
            this.textBoxFilterOpponent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFilterOpponent.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.textBoxFilterOpponent.Location = new System.Drawing.Point(222, 11);
            this.textBoxFilterOpponent.Name = "textBoxFilterOpponent";
            this.textBoxFilterOpponent.Size = new System.Drawing.Size(110, 22);
            this.textBoxFilterOpponent.TabIndex = 1;
            this.textBoxFilterOpponent.Text = "Find Opponent";
            this.textBoxFilterOpponent.Click += new System.EventHandler(this.TextBoxFilterOpponent_Click);
            this.textBoxFilterOpponent.TextChanged += new System.EventHandler(this.TextBoxFilterOpponent_TextChanged);
            this.textBoxFilterOpponent.Enter += new System.EventHandler(this.TextBoxFilterOpponent_Enter);
            this.textBoxFilterOpponent.Leave += new System.EventHandler(this.TextBoxFilterOpponent_Leave);
            // 
            // StatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(534, 361);
            this.Controls.Add(this.panelFilters);
            this.Controls.Add(this.buttonFilters);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelAllGames);
            this.Controls.Add(this.labelAbandons);
            this.Controls.Add(this.labelLoses);
            this.Controls.Add(this.labelWinningStreak);
            this.Controls.Add(this.labelLongestWinningStreak);
            this.Controls.Add(this.labelWins);
            this.Controls.Add(this.dataGridViewAllGames);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(550, 2000);
            this.MinimumSize = new System.Drawing.Size(550, 400);
            this.Name = "StatusForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "My status";
            this.Load += new System.EventHandler(this.StatusForm_Load);
            this.SizeChanged += new System.EventHandler(this.StatusForm_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StatusForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAllGames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelWins;
        private System.Windows.Forms.Label labelLongestWinningStreak;
        private System.Windows.Forms.Label labelWinningStreak;
        private System.Windows.Forms.Label labelLoses;
        private System.Windows.Forms.Label labelAbandons;
        private System.Windows.Forms.Label labelAllGames;
        private System.Windows.Forms.DataGridView dataGridViewAllGames;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonFilters;
        private System.Windows.Forms.Panel panelFilters;
        private System.Windows.Forms.RadioButton radioButtonAll;
        private System.Windows.Forms.RadioButton radioButtonLoses;
        private System.Windows.Forms.RadioButton radioButtonWins;
        private System.Windows.Forms.TextBox textBoxFilterOpponent;
        private System.Windows.Forms.RadioButton radioButtonAbandons;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
    }
}