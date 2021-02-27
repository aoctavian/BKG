using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    class PIECE1 : PIECE
    {
        private PLAYER _PLAYER;
        private int pieceType;

        public PIECE1(PLAYER PLAYER, int pieceType)
        {
            _PLAYER = PLAYER;
            this.pieceType = pieceType;
            PLAYER.CountPieces++;
        }

        public override PLAYER PLAYER
        {
            get { return _PLAYER; }
            set { _PLAYER = value; }
        }

        public override int PieceType
        {
            get { return pieceType; }
            set { pieceType = value; }
        }

        public override int BarRow()
        {
            return PLAYER.BarRow;
        }

        public override int BarOut()
        {
            return PLAYER.BarOut;
        }
    }
}