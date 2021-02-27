using Backgammon.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backgammon
{
    public partial class ChangeNameForm : Form
    {
        public new string Name { get; set; }

        public ChangeNameForm()
        {
            InitializeComponent();
            labelCurrentName.Text = "Your in game name: "+ Settings.Default.Name;
        }

        private void ChangeName()
        {
            if (!String.IsNullOrEmpty(textBoxNewName.Text))
            {
                Name = textBoxNewName.Text;
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void ButtonChangeName_Click(object sender, EventArgs e)
        {
            ChangeName();
        }

        private void TextBoxNewName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                ChangeName();
            }
        }
    }
}
