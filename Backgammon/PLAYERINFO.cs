using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backgammon
{
    public class PLAYERINFO
    {
        public Panel Panel { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public bool PermissionToSeeInfo { get; set; }

        public int GetPlayerType()
        {
            if (Id < 0)
                return DEF.TYPE_GUEST;
            else
                return DEF.TYPE_PLAYER;
        }

        public PLAYERINFO() { }
    }
}