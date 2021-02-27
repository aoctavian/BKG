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
    public partial class PreferencesForm : Form
    {
        public PreferencesForm()
        {
            InitializeComponent();
        }

        private void PreferencesForm_Load(object sender, EventArgs e)
        {
            checkBoxAppSounds.Checked = Settings.Default.AppSounds;
            checkBoxChatSound.Checked = Settings.Default.GlobalChatSound;
            checkBoxGameSound.Checked = Settings.Default.GameSound;

            if (Settings.Default.PermissionToSeeStatus)
                comboBoxPermission.SelectedIndex = 0;
            else
                comboBoxPermission.SelectedIndex = 1;

            this.ActiveControl = label1;
        }

        private void PreferencesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Save();
            DialogResult = DialogResult.OK;
        }

        private void PreferencesForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void ComboBoxPermission_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.PermissionToSeeStatus = Convert.ToBoolean(comboBoxPermission.SelectedItem);
        }

        private void CheckBoxAppSounds_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.GameSound = checkBoxAppSounds.Checked;
        }

        private void CheckBoxChatSound_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.GlobalChatSound = checkBoxChatSound.Checked;
        }

        private void CheckBoxGameSound_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.GameSound = checkBoxGameSound.Checked;
        }
    }
}
