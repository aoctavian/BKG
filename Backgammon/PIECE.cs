using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backgammon
{
    public class PossibleLocation
    {
        public int Column { get; set; }
        public int Row { get; set; }
        public Dice Dice { get; set; }
        public int NrDice { get; set; }

        public PossibleLocation(int column, int row, Dice dice, int nrDice)
        {
            Column = column;
            Row = row;
            Dice = dice;
            NrDice = nrDice;
        }
    }

    public class SimplePIECE
    {
        public string Name { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public int StackCount { get; set; }

        public SimplePIECE(PIECE piece)
        {
            Name = piece.Name;
            StackCount = piece.PieceStack.Count;
            Column = piece.Column;
            Row = piece.Row;
        }
    }

    public partial class PIECE : PictureBox
    {
        //virtual access
        private PLAYER _PLAYER;
        private int pieceType;

        private bool painted;

        public PIECE()
        {
            InitializeComponent();
            PieceStack = new Stack<PIECE>();
            OnBar = false;
            painted = false;
            PossibleMovesList = new List<PossibleLocation>();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            //pe.Graphics.DrawString(Name, Font, Brushes.Red, 0, 0);
        }

        public List<PossibleLocation> PossibleMovesList { get; set; }
        //--------------------------------------------------------------------------------------------------------------------------------
        [Browsable(false)]
        public virtual PLAYER PLAYER
        {
            get { return _PLAYER; }
            set { _PLAYER = value; }
        }

        [Category("Added by me")]
        [Description("Indicates the Piece Type")]
        public virtual int PieceType
        {
            get { return pieceType; }
            set { pieceType = value; }
        }

        public virtual int BarRow()
        {
            return 0;
        }

        public virtual int BarOut()
        {
            return 0;
        }
        //--------------------------------------------------------------------------------------------------------------------------------

        //is set when piece is added to GameTable
        [Browsable(false)]
        public GameTable Table { get; set; }

        [Browsable(false)]
        public Stack<PIECE> PieceStack { get; }

        public void AddToStack(PIECE Piece)
        {
            PieceStack.Push(Piece);
            Painted = true;
        }

        public PIECE RemoveFromStack()
        {
            if (PieceStack.Count == 1)
                Painted = false;
            return PieceStack.Pop();
        }

        [Category("Added by me")]
        [Description("Indicates if the Piece is on bar")]
        public bool OnBar { get; set; }

        [Browsable(false)]
        public bool Painted
        {
            get { return painted; }
            set { painted = value; Invalidate(); }//causes the control to redraw 
        }

        [Browsable(false)]
        public int Column { get; set; }

        [Browsable(false)]
        public int Row { get; set; }

        public bool CanMove(int toColumn, int toRow)
        {
            int index;
            if (toRow <= 6)
                index = PossibleMovesList.FindIndex(x => x.Column == toColumn && x.Row <= 6);
            else
                index = PossibleMovesList.FindIndex(x => x.Column == toColumn && x.Row >= 8);

            if (index != -1)
            {
                Table.SaveTable();
                return true;
            }
            return false;
        }

        public void MovePiece(int toColumn, int toRow)
        {
            int index;
            if (toRow <= 6)
                index = PossibleMovesList.FindIndex(x => x.Column == toColumn && x.Row <= 6);
            else
                index = PossibleMovesList.FindIndex(x => x.Column == toColumn && x.Row >= 8);

            PossibleMovesList[index].Dice.DiceBlocked = true;
            if (Table.CountPiecesFromColumn(PossibleMovesList[index].Column, PossibleMovesList[index].Row) == 1 && Table.ColumnPiecesType(PossibleMovesList[index].Column, PossibleMovesList[index].Row) != PieceType)
            {
                PutOnBar(Table.GetFirstPieceFromColumn(PossibleMovesList[index].Column, PossibleMovesList[index].Row));
            }
            if (PossibleMovesList[index].NrDice > 1)
            {
                for (int i = index; i > index - PossibleMovesList[index].NrDice; i--)
                {
                    PossibleMovesList[i].Dice.DiceBlocked = true;
                    if (Table.CountPiecesFromColumn(PossibleMovesList[i].Column, PossibleMovesList[i].Row) == 1 && Table.ColumnPiecesType(PossibleMovesList[i].Column, PossibleMovesList[i].Row) != PieceType)
                    {
                        PutOnBar(Table.GetFirstPieceFromColumn(PossibleMovesList[i].Column, PossibleMovesList[i].Row));
                    }
                }
            }

            if (this.Column == 7)
            {
                if (this.PieceStack.Count == 0)
                {
                    OnBar = false;
                    PLAYER.HasPiecesOnBar = false;
                    PLAYER.PieceFromBar = null;
                }
                else
                {
                    this.PieceStack.ElementAt(0).OnBar = false;
                }
            }

            int freeRow = Table.FreeRow(toColumn, toRow); //salveaza freeRow cu o piesa de la oponent si dupa o scoate si trebuie refacut freeRow

            this.BorderStyle = BorderStyle.None;
            switch (freeRow)
            {
                case 0:
                    PIECE c6 = (PIECE)Table.GetControlFromPosition(toColumn, 6);
                    if (this.PieceStack.Count > 0)
                        c6.AddToStack(this.RemoveFromStack());
                    else
                    {
                        Table.Remove(this);
                        c6.AddToStack(this);
                    }
                    break;
                case 14:
                    PIECE c8 = (PIECE)Table.GetControlFromPosition(toColumn, 8);
                    if (this.PieceStack.Count > 0)
                        c8.AddToStack(this.RemoveFromStack());
                    else
                    {
                        Table.Remove(this);
                        c8.AddToStack(this);
                    }
                    break;
                default:
                    PIECE piece;
                    if (PieceStack.Count > 0)
                        piece = this.RemoveFromStack();
                    else
                        piece = this;

                    piece.Column = toColumn;

                    if (piece.Column != 14 && piece.Column != 0)
                    {
                        piece.Row = freeRow;
                        Table.MovePiece(piece);
                    }
                    else
                    {
                        piece.Row = BarOut();
                        PIECE p = (PIECE)Table.GetControlFromPosition(piece.Column, BarOut());
                        if (p == null)
                        {
                            Table.MovePiece(piece);
                        }
                        else
                        {
                            Table.Remove(piece);
                            p.AddToStack(piece);
                        }
                    }
                    break;
            }

            if (toColumn == 14 || toColumn == 0)
            {
                PLAYER.CountPieces--;
            }
        }

        public void MovePieceBack(int toColumn, int toRow)
        {
            if(toColumn == 7)
            {
                PutOnBar(this);
                return;
            }
            else if (this.Column == 7)
            {
                if (this.PieceStack.Count == 0)
                {
                    OnBar = false;
                    PLAYER.HasPiecesOnBar = false;
                    PLAYER.PieceFromBar = null;
                }
                else
                {
                    this.PieceStack.ElementAt(0).OnBar = false;
                }
            }
            else if(this.Column == 0 || this.Column== 14)
            {
                PLAYER.CountPieces++;
            }

            int freeRow = Table.FreeRow(toColumn, toRow); //salveaza freeRow cu o piesa de la oponent si dupa o scoate si trebuie refacut freeRow

            switch (freeRow)
            {
                case 0:
                    PIECE c6 = (PIECE)Table.GetControlFromPosition(toColumn, 6);
                    if (this.PieceStack.Count > 0)
                        c6.AddToStack(this.RemoveFromStack());
                    else
                    {
                        Table.Remove(this);
                        c6.AddToStack(this);
                    }
                    break;
                case 14:
                    PIECE c8 = (PIECE)Table.GetControlFromPosition(toColumn, 8);
                    if (this.PieceStack.Count > 0)
                        c8.AddToStack(this.RemoveFromStack());
                    else
                    {
                        Table.Remove(this);
                        c8.AddToStack(this);
                    }
                    break;
                default:
                    PIECE piece;
                    if (PieceStack.Count > 0)
                        piece = this.RemoveFromStack();
                    else
                        piece = this;

                    piece.Column = toColumn;

                    if (piece.Column != 14 && piece.Column != 0)
                    {
                        piece.Row = freeRow;
                        Table.MovePiece(piece);
                    }
                    else
                    {
                        piece.Row = BarOut();
                        PIECE p = (PIECE)Table.GetControlFromPosition(piece.Column, BarOut());
                        if (p == null)
                        {
                            Table.MovePiece(piece);
                        }
                        else
                        {
                            Table.Remove(piece);
                            p.AddToStack(piece);
                        }
                    }
                    break;
            }
        }

        private void PutOnBar(PIECE piece)
        {
            Table.Remove(piece);

            piece.Column = 7;
            piece.Row = piece.BarRow();
            piece.OnBar = true;

            if (!piece.PLAYER.HasPiecesOnBar)
            {
                Table.MovePiece(piece);
                piece.PLAYER.PieceFromBar = piece;
            }
            else
                ((PIECE)Table.GetControlFromPosition(piece.Column, piece.Row)).AddToStack(piece);

            piece.PLAYER.HasPiecesOnBar = true;
        }

        public bool AddPossibleMove(PossibleLocation location)
        {
            int nrPieces = Table.CountPiecesFromColumn(location.Column, location.Row);
            if (nrPieces == 0 || Table.ColumnPiecesType(location.Column, location.Row) == PieceType || nrPieces == 1)
            {
                PossibleMovesList.Add(location);
                PLAYER.CountPossibleMoves++;
                return true;
            }
            else
                return false;
        }
    }
}