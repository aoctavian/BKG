using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backgammon
{
    public class PLAYER : PLAYERINFO
    {
        public int CountPieces { get; set; }
        public int PiecesType { get; set; }
        public int Zone { get; set; }
        public bool HasPiecesOnBar { get; set; }
        public PIECE PieceFromBar { get; set; }
        public int CountPossibleMoves { get; set; }

        public int BarRow { get; }
        public int BarOut { get; }

        public PLAYER(int piecesType, int zone)
        {
            CountPieces = 0;
            PiecesType = piecesType;
            Zone = zone;
            if (Zone == 1 || Zone == 2)
            {
                BarRow = 11;
                BarOut = 3;
            }
            else
            {
                BarRow = 3;
                BarOut = 11;
            }
            HasPiecesOnBar = false;
        }
    }
}