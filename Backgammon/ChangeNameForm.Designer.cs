namespace Backgammon
{
    partial class ChangeNameForm
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
            this.textBoxNewName = new System.Windows.Forms.TextBox();
            this.newName = new System.Windows.Forms.Label();
            this.labelCurrentName = new System.Windows.Forms.Label();
            this.buttonChangeName = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxNewName
            // 
            this.textBoxNewName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNewName.Location = new System.Drawing.Point(136, 72);
            this.textBoxNewName.MaxLength = 50;
            this.textBoxNewName.Name = "textBoxNewName";
            this.textBoxNewName.Size = new System.Drawing.Size(116, 22);
            this.textBoxNewName.TabIndex = 0;
            this.textBoxNewName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxNewName_KeyDown);
            // 
            // newName
            // 
            this.newName.AutoSize = true;
            this.newName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newName.Location = new System.Drawing.Point(38, 76);
            this.newName.Name = "newName";
            this.newName.Size = new System.Drawing.Size(78, 16);
            this.newName.TabIndex = 3;
            this.newName.Text = "New Name:";
            // 
            // labelCurrentName
            // 
            this.labelCurrentName.AutoSize = true;
            this.labelCurrentName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCurrentName.Location = new System.Drawing.Point(38, 40);
            this.labelCurrentName.Name = "labelCurrentName";
            this.labelCurrentName.Size = new System.Drawing.Size(96, 16);
            this.labelCurrentName.TabIndex = 2;
            this.labelCurrentName.Text = "Current Name: ";
            // 
            // buttonChangeName
            // 
            this.buttonChangeName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonChangeName.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.buttonChangeName.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.buttonChangeName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonChangeName.Location = new System.Drawing.Point(167, 125);
            this.buttonChangeName.Name = "buttonChangeName";
            this.buttonChangeName.Size = new System.Drawing.Size(85, 27);
            this.buttonChangeName.TabIndex = 1;
            this.buttonChangeName.Text = "Change";
            this.buttonChangeName.UseVisualStyleBackColor = true;
            this.buttonChangeName.Click += new System.EventHandler(this.ButtonChangeName_Click);
            // 
            // ChangeNameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(302, 194);
            this.Controls.Add(this.textBoxNewName);
            this.Controls.Add(this.newName);
            this.Controls.Add(this.labelCurrentName);
            this.Controls.Add(this.buttonChangeName);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ChangeNameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change Name";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxNewName;
        private System.Windows.Forms.Label newName;
        private System.Windows.Forms.Label labelCurrentName;
        private System.Windows.Forms.Button buttonChangeName;
    }
}