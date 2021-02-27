namespace Backgammon
{
    partial class PreferencesForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxPermission = new System.Windows.Forms.ComboBox();
            this.checkBoxAppSounds = new System.Windows.Forms.CheckBox();
            this.checkBoxChatSound = new System.Windows.Forms.CheckBox();
            this.checkBoxGameSound = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(260, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Permission for opponent to see your status";
            // 
            // comboBoxPermission
            // 
            this.comboBoxPermission.FormattingEnabled = true;
            this.comboBoxPermission.Items.AddRange(new object[] {
            "True",
            "False"});
            this.comboBoxPermission.Location = new System.Drawing.Point(283, 16);
            this.comboBoxPermission.Name = "comboBoxPermission";
            this.comboBoxPermission.Size = new System.Drawing.Size(71, 21);
            this.comboBoxPermission.TabIndex = 1;
            this.comboBoxPermission.SelectedIndexChanged += new System.EventHandler(this.ComboBoxPermission_SelectedIndexChanged);
            // 
            // checkBoxAppSounds
            // 
            this.checkBoxAppSounds.AutoSize = true;
            this.checkBoxAppSounds.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxAppSounds.Checked = true;
            this.checkBoxAppSounds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAppSounds.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBoxAppSounds.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxAppSounds.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxAppSounds.Location = new System.Drawing.Point(20, 49);
            this.checkBoxAppSounds.Name = "checkBoxAppSounds";
            this.checkBoxAppSounds.Size = new System.Drawing.Size(124, 20);
            this.checkBoxAppSounds.TabIndex = 6;
            this.checkBoxAppSounds.Text = "App Sounds ON";
            this.checkBoxAppSounds.UseVisualStyleBackColor = false;
            this.checkBoxAppSounds.CheckedChanged += new System.EventHandler(this.CheckBoxAppSounds_CheckedChanged);
            // 
            // checkBoxChatSound
            // 
            this.checkBoxChatSound.AutoSize = true;
            this.checkBoxChatSound.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxChatSound.Checked = true;
            this.checkBoxChatSound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxChatSound.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBoxChatSound.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxChatSound.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxChatSound.Location = new System.Drawing.Point(20, 75);
            this.checkBoxChatSound.Name = "checkBoxChatSound";
            this.checkBoxChatSound.Size = new System.Drawing.Size(119, 20);
            this.checkBoxChatSound.TabIndex = 7;
            this.checkBoxChatSound.Text = "Chat Sound ON";
            this.checkBoxChatSound.UseVisualStyleBackColor = false;
            this.checkBoxChatSound.CheckedChanged += new System.EventHandler(this.CheckBoxChatSound_CheckedChanged);
            // 
            // checkBoxGameSound
            // 
            this.checkBoxGameSound.AutoSize = true;
            this.checkBoxGameSound.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxGameSound.Checked = true;
            this.checkBoxGameSound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxGameSound.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBoxGameSound.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxGameSound.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxGameSound.Location = new System.Drawing.Point(20, 101);
            this.checkBoxGameSound.Name = "checkBoxGameSound";
            this.checkBoxGameSound.Size = new System.Drawing.Size(129, 20);
            this.checkBoxGameSound.TabIndex = 8;
            this.checkBoxGameSound.Text = "Game Sound ON";
            this.checkBoxGameSound.UseVisualStyleBackColor = false;
            this.checkBoxGameSound.CheckedChanged += new System.EventHandler(this.CheckBoxGameSound_CheckedChanged);
            // 
            // PreferencesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(381, 146);
            this.Controls.Add(this.checkBoxGameSound);
            this.Controls.Add(this.checkBoxChatSound);
            this.Controls.Add(this.checkBoxAppSounds);
            this.Controls.Add(this.comboBoxPermission);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "PreferencesForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PreferencesForm_FormClosing);
            this.Load += new System.EventHandler(this.PreferencesForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PreferencesForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxPermission;
        private System.Windows.Forms.CheckBox checkBoxAppSounds;
        private System.Windows.Forms.CheckBox checkBoxChatSound;
        private System.Windows.Forms.CheckBox checkBoxGameSound;
    }
}