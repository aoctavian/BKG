using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Backgammon.Properties;

namespace Backgammon
{
    public partial class Dice : PictureBox
    {
        public int diceValue;
        private bool diceBlocked;

        public Dice()
        {
            InitializeComponent();
            diceValue = 0;
            diceBlocked = false;
        }

        public Dice(Dice dice)
        {
            diceValue = dice.diceValue;
            diceBlocked = dice.diceBlocked;
            Name = dice.Name;
        }
        
        [Category("Added by me")]
        public int DiceValue
        {
            get { return diceValue; }
            set
            {
                diceValue = value;
                DiceBlocked = false;
                switch (diceValue)
                {
                    case 1: BackgroundImage = Resources.dice1; break;
                    case 2: BackgroundImage = Resources.dice2; break;
                    case 3: BackgroundImage = Resources.dice3; break;
                    case 4: BackgroundImage = Resources.dice4; break;
                    case 5: BackgroundImage = Resources.dice5; break;
                    case 6: BackgroundImage = Resources.dice6; break;
                    default: BackgroundImage = null; break;
                }
                Visible = true;
            }
        }
        
        [Category("Added by me")]
        public bool DiceBlocked
        {
            get { return diceBlocked; }
            set
            {
                diceBlocked = value;
                if (diceBlocked)
                {
                    BackColor = Color.Gainsboro;
                    //DiceValue = 0; // not beacuse its calling 'set DiceValue' and it changes properties
                    diceValue = 0;
                }
                else
                {
                    BackColor = Color.White;
                }
            }
        }
    }
}