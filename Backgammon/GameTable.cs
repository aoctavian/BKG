using Backgammon.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Backgammon
{
    public partial class GameTable : TableLayoutPanel
    {
        public GameTable()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        public void AddPiece(PIECE Piece)
        {
            Piece.Table = this;
            switch (Piece.PieceType)
            {
                case 1: Piece.BackgroundImage = Resources.piece1; break;
                case 2: Piece.BackgroundImage = Resources.piece2; break;
            }
            this.Controls.Add(Piece, Piece.Column, Piece.Row);
        }

        public void MovePiece(PIECE Piece)
        {
            this.Controls.Add(Piece, Piece.Column, Piece.Row);
        }

        public void Remove(PIECE Piece)
        {
            this.Controls.Remove(Piece);
        }

        private List<Dice> dicesList;
        private List<SimplePIECE> piecesList;
        public void SaveTable()
        {
            //save dice
            //save pieces
            //save pieces in table/out pieces
            dicesList = new List<Dice>();
            Controls.OfType<Dice>().Where(d => d.Visible == true).ToList().ForEach(dice => dicesList.Add(new Dice(dice)));

            var piecesTop = this.Controls.OfType<PIECE>().Where(x => x.Column != 7 && x.Column > 0 && x.Column < 14 && x.Row <= 6);
            var piecesBottom = this.Controls.OfType<PIECE>().Where(x => x.Column != 7 && x.Column > 0 && x.Column < 14 && x.Row >= 8);

            var pTop = from e in piecesTop
                       orderby e.Row descending
                       group e by e.Column into gr
                       select gr.First();

            var pBottom = from e in piecesBottom
                          orderby e.Row ascending
                          group e by e.Column into gr
                          select gr.First();

           
            var pieces = pTop.Concat(pBottom);
            
            piecesList = new List<SimplePIECE>();

            foreach (var p in pieces)
                piecesList.Add(new SimplePIECE(p));
            
            PIECE pBar1 = this.Controls.OfType<PIECE>().Where(x => x.Column == 7 && x.Row == 3).SingleOrDefault();
            if (pBar1 != null)
            {
                if (pBar1.PieceStack.Count > 0)
                    pBar1 = pBar1.PieceStack.Peek();
                piecesList.Add(new SimplePIECE(pBar1));
            }

            PIECE pBar2 = this.Controls.OfType<PIECE>().Where(x => x.Column == 7 && x.Row == 11).SingleOrDefault();
            if (pBar2 != null)
            {
                if (pBar2.PieceStack.Count > 0)
                    pBar2 = pBar2.PieceStack.Peek();
                piecesList.Add(new SimplePIECE(pBar2));
            }

            //foreach(SimplePIECE p in piecesList)
            //    Console.WriteLine($"Name= {p.Name}, c={p.Column}, r={p.Row}");
            Console.WriteLine("Table Saved");
        }

        public void UndoTable()
        {
            //restore last save
            dicesList.ForEach(dice => {
                Dice d = Controls.OfType<Dice>().Where(di => di.Name == dice.Name).Single();
                if(d.DiceValue != dice.DiceValue)
                    d.DiceValue = dice.DiceValue;
            });

            List<PIECE> tablePieces = new List<PIECE>();
            
            foreach (var piece in piecesList.Reverse<SimplePIECE>())
            {
                PIECE pi = Controls.OfType<PIECE>().Where(p => p.Name == piece.Name || (p.PieceStack.Count > 0 && p.PieceStack.Peek().Name == piece.Name)).Single();
                if (pi.Row != piece.Row || pi.Column != piece.Column || pi.PieceStack.Count != piece.StackCount)
                {
                    if (pi.PieceStack.Count == 0)
                    {
                        this.Remove(pi);
                        tablePieces.Add(pi);
                    }
                    else
                    {
                        tablePieces.Add(pi.RemoveFromStack());
                    }
                }
                else
                {
                    piecesList.Remove(piece);
                }
            }

            foreach (var piece in piecesList)
            {
                PIECE pi = tablePieces.Where(p => p.Name == piece.Name).Single();
                pi.MovePieceBack(piece.Column, piece.Row);
            }
        }

        public int FreeRow(int fromColumn, int row)
        {
            int i;
            if (row <= 6)
            {
                for (i = 1; i <= 6; i++)
                    if (this.GetControlFromPosition(fromColumn, i) == null)
                        return i;

                return 0; //means put on stack
            }
            else
            {
                for (i = 13; i >= 8; i--)
                    if (this.GetControlFromPosition(fromColumn, i) == null)
                        return i;

                return 14; //means put on stack
            }
        }

        public bool IsFirstPieceFromColumn(PIECE Piece)
        {
            if (Piece.Row <= 6)
            {
                if (this.GetControlFromPosition(Piece.Column, Piece.Row + 1) != null && Piece.Row + 1 <= 6)
                    return false;
            }
            else
            {
                if (this.GetControlFromPosition(Piece.Column, Piece.Row - 1) != null && Piece.Row - 1 >= 8)
                    return false;
            }
            return true;
        }

        public int CountPiecesFromColumn(int column, int row)
        {
            int i;
            if (column != 14 && column != 0) //in AddPossibleMoves
            {
                if (row <= 6)
                {
                    for (i = 1; i <= 7; i++)
                        if (this.GetControlFromPosition(column, i) == null || i == 7)
                            return i - 1;
                }
                else
                {
                    for (i = 13; i >= 7; i--)
                        if (this.GetControlFromPosition(column, i) == null || i == 7)
                            return 13 - i;
                }
            }
            //pentru AddPossibleMoves returneaza 0 ca sa poata adauga mutarea
            return 0;
        }

        public int ColumnPiecesType(int column, int row)
        {
            if (row <= 6)
            {
                if (GetControlFromPosition(column, 1) != null)
                    return ((PIECE)(this.GetControlFromPosition(column, 1))).PieceType;
            }
            else
            {
                if (GetControlFromPosition(column, 13) != null)
                    return ((PIECE)(this.GetControlFromPosition(column, 13))).PieceType;
            }
            return 0;
        }

        public PIECE GetFirstPieceFromColumn(int toColumn, int toRow)
        {
            if (toRow <= 6)
            {
                Control c = GetControlFromPosition(toColumn, 1);
                if(c != null)
                    return (PIECE)c;
            }
            else
            {
                Control c = GetControlFromPosition(toColumn, 13);
                if (c != null)
                    return (PIECE)c;
            }
            return null;
        }

        public bool PieceInZone(PIECE Piece)
        {
            switch (Piece.PLAYER.Zone)
            {
                case 2:
                    if (Piece.Column >= 8 && Piece.Column <= 13 && Piece.Row >= 1 && Piece.Row <= 6)
                        return true;
                    break;
                case 4:
                    if (Piece.Column >= 8 && Piece.Column <= 13 && Piece.Row >= 8 && Piece.Row <= 13)
                        return true;
                    break;
                case 1:
                    if (Piece.Column >= 1 && Piece.Column <= 6 && Piece.Row >= 1 && Piece.Row <= 6)
                        return true;
                    break;
                case 3:
                    if (Piece.Column >= 1 && Piece.Column <= 6 && Piece.Row >= 8 && Piece.Row <= 13)
                        return true;
                    break;
            }
            return false;
        }

        public int PiecesInZone(int zone, int pieceType)
        {
            int count = 0;
            int rowStart = 0, rowEnd = 0;
            int columnStart = 0, columnEnd = 0;
            switch (zone)
            {
                case 2:
                    columnStart = 8; columnEnd = 13;
                    rowStart = 1; rowEnd = 6;
                    break;
                case 4:
                    columnStart = 8; columnEnd = 13;
                    rowStart = 8; rowEnd = 13;
                    break;
                case 1:
                    columnStart = 1; columnEnd = 6;
                    rowStart = 1; rowEnd = 6;
                    break;
                case 3:
                    columnStart = 1; columnEnd = 6;
                    rowStart = 8; rowEnd = 13;
                    break;
            }
            for (int i = columnStart; i <= columnEnd; i++)
                for (int j = rowStart; j <= rowEnd; j++)
                    if (GetControlFromPosition(i, j) != null && ((PIECE)GetControlFromPosition(i, j)).PieceType == pieceType)
                    {
                        count += 1 + ((PIECE)GetControlFromPosition(i, j)).PieceStack.Count;
                    }
            return count;
        }

        public bool AllFreeToColum(int column, int zone, int pieceType, int c, int r)
        {
            switch (zone)
            {
                case 2:
                    for (int i = 8; i < column; i++)
                    {
                        if (GetControlFromPosition(i, 1) != null && ((PIECE)GetControlFromPosition(i, 1)).PieceType == pieceType)
                            return false;
                    }
                    break;
                case 4:
                    for (int i = 8; i < column; i++)
                    {
                        if (!(c == i && r == 13))
                            if (GetControlFromPosition(i, 13) != null && ((PIECE)GetControlFromPosition(i, 13)).PieceType == pieceType)
                                return false;
                    }
                    break;
                case 1:
                    for (int i = 6; i > column; i--)
                    {
                        if (GetControlFromPosition(i, 1) != null && ((PIECE)GetControlFromPosition(i, 1)).PieceType == pieceType)
                            return false;
                    }
                    break;
                case 3:
                    for (int i = 6; i > column; i--)
                    {
                        if (GetControlFromPosition(i, 13) != null && ((PIECE)GetControlFromPosition(i, 13)).PieceType == pieceType)
                            return false;
                    }
                    break;
            }
            return true;
        }

        public bool IsWin(int zone, int pieceType, int lines)
        {
            int rowStart = 0, rowEnd = 0;
            int columnStart = 0, columnEnd = 0;
            switch (zone)
            {
                case 2:
                    columnStart = 8; columnEnd = 13;
                    rowStart = 1; rowEnd = rowStart + lines - 1;
                    break;
                case 4:
                    columnStart = 8; columnEnd = 13;
                    rowEnd = 13; rowStart = rowEnd - lines - 1;
                    break;
                case 1:
                    columnStart = 1; columnEnd = 6;
                    rowStart = 1; rowEnd = rowStart + lines - 1;
                    break;
                case 3:
                    columnStart = 1; columnEnd = 6;
                    rowEnd = 13; rowStart = rowEnd - lines - 1;
                    break;
            }
            for (int i = columnStart; i <= columnEnd; i++)
                for (int j = rowStart; j <= rowEnd; j++)
                    if (!(GetControlFromPosition(i, j) != null && ((PIECE)GetControlFromPosition(i, j)).PieceType == pieceType))
                    {
                        return false;
                    }
            return true;
        }

        public bool IsWinLines_1(PLAYER PLAYER) //Linie
        {
            if (PiecesInZone(PLAYER.Zone, PLAYER.PiecesType) == PLAYER.CountPieces)
                if (IsWin(PLAYER.Zone, PLAYER.PiecesType, 1))
                    return true;
            return false;
        }

        public bool IsWinLines_2(PLAYER PLAYER) //Marti
        {
            if (PiecesInZone(PLAYER.Zone, PLAYER.PiecesType) == PLAYER.CountPieces)
                if (IsWin(PLAYER.Zone, PLAYER.PiecesType, 2))
                    return true;
            return false;
        }

        public void SetPossibleMoves(PLAYER PLAYER)
        {
            List<Dice> diceList = this.Controls.OfType<Dice>().Where(x => x.DiceValue != 0).ToList();
            List<Dice> dices;

            if (!PLAYER.HasPiecesOnBar)
            {
                var piecesTop = this.Controls.OfType<PIECE>().Where(x => x.PieceType == PLAYER.PiecesType && x.Column > 0 && x.Column < 14 && x.Row <= 6);
                var piecesBottom = this.Controls.OfType<PIECE>().Where(x => x.PieceType == PLAYER.PiecesType && x.Column > 0 && x.Column < 14 && x.Row >= 8);

                var pTop = from e in piecesTop
                           orderby e.Row descending
                           group e by e.Column into gr
                           select gr.First();

                var pBottom = from e in piecesBottom
                              orderby e.Row ascending
                              group e by e.Column into gr
                              select gr.First();

                var pieces = pTop.Concat(pBottom);

                switch (PLAYER.Zone)
                {
                    case 4:
                        if (diceList.Count == 2 && diceList[0].DiceValue != diceList[1].DiceValue) //intra cand mai sunt 2 zaruri din 4, si face de 2 ori
                        {
                            foreach (PIECE piece in pieces)
                            {
                                piece.PossibleMovesList.Clear();
                                dices = diceList.OrderBy(x => x.DiceValue).ToList();
                                SetPiecePossibleMoves_24(0, piece, dices.ToList(), (x) => x <= 6, 8);
                                dices = diceList.OrderByDescending(x => x.DiceValue).ToList();
                                SetPiecePossibleMoves_24(0, piece, dices, (x) => x <= 6, 8);
                            }
                        }
                        else
                        {
                            foreach (PIECE piece in pieces)
                            {
                                piece.PossibleMovesList.Clear();
                                SetPiecePossibleMoves_24(0, piece, diceList, (x) => x <= 6, 8);
                            }
                        }
                        break;
                    case 2:
                        if (diceList.Count == 2 && diceList[0].DiceValue != diceList[1].DiceValue)
                        {
                            foreach (PIECE piece in pieces)
                            {
                                piece.PossibleMovesList.Clear();
                                dices = diceList.OrderBy(x => x.DiceValue).ToList();
                                SetPiecePossibleMoves_24(0, piece, dices, (x) => x >= 8, 6);
                                dices = diceList.OrderByDescending(x => x.DiceValue).ToList();
                                SetPiecePossibleMoves_24(0, piece, dices, (x) => x >= 8, 6);
                            }
                        }
                        else
                        {
                            foreach (PIECE piece in pieces)
                            {
                                piece.PossibleMovesList.Clear();
                                SetPiecePossibleMoves_24(0, piece, diceList, (x) => x >= 8, 6);
                            }
                        }
                        break;
                    case 1:
                        if (diceList.Count == 2 && diceList[0].DiceValue != diceList[1].DiceValue)
                        {
                            foreach (PIECE piece in pieces)
                            {
                                piece.PossibleMovesList.Clear();
                                dices = diceList.OrderBy(x => x.DiceValue).ToList();
                                SetPiecePossibleMoves_13(0, piece, dices, (x) => x >= 8, 6);
                                dices = diceList.OrderByDescending(x => x.DiceValue).ToList();
                                SetPiecePossibleMoves_13(0, piece, dices, (x) => x >= 8, 6);
                            }
                        }
                        else
                        {
                            foreach (PIECE piece in pieces)
                            {
                                piece.PossibleMovesList.Clear();
                                SetPiecePossibleMoves_13(0, piece, diceList, (x) => x >= 8, 6);
                            }
                        }
                        break;
                    case 3:
                        if (diceList.Count == 2 && diceList[0].DiceValue != diceList[1].DiceValue)
                        {
                            foreach (PIECE piece in pieces)
                            {
                                piece.PossibleMovesList.Clear();
                                dices = diceList.OrderBy(x => x.DiceValue).ToList();
                                SetPiecePossibleMoves_13(0, piece, dices, (x) => x <= 6, 8);
                                dices = diceList.OrderByDescending(x => x.DiceValue).ToList();
                                SetPiecePossibleMoves_13(0, piece, dices, (x) => x <= 6, 8);
                            }
                        }
                        else
                        {
                            foreach (PIECE piece in pieces)
                            {
                                piece.PossibleMovesList.Clear();
                                SetPiecePossibleMoves_13(0, piece, diceList, (x) => x <= 6, 8);
                            }
                        }
                        break;

                }
            }
            else
            {
                int c = 0;
                PIECE Piece = PLAYER.PieceFromBar;
                int pColumn = Piece.Column;

                int pRow = Piece.Row;
                Piece.PossibleMovesList.Clear();

                switch (PLAYER.Zone)
                {
                    case 3:
                        foreach (Dice dice in diceList)
                        {
                            c = dice.DiceValue;
                            if (Piece.AddPossibleMove(new PossibleLocation(c, 6, dice, 1)))
                            {
                                if (Piece.PieceStack.Count == 0)
                                {
                                    Piece.Column = c;
                                    Piece.Row = 6;
                                    SetPiecePossibleMoves_13(1, Piece, diceList.Where(x => x != dice).ToList(), (x) => x <= 6, 8);
                                    Piece.Column = pColumn;
                                    Piece.Row = pRow;
                                }
                            }
                        }
                        break;
                    case 4:
                        foreach (Dice dice in diceList)
                        {
                            c = 14 - dice.DiceValue;
                            if (Piece.AddPossibleMove(new PossibleLocation(c, 6, dice, 1)))
                            {
                                if (Piece.PieceStack.Count == 0)
                                {
                                    Piece.Column = c;
                                    Piece.Row = 6;
                                    SetPiecePossibleMoves_24(1, Piece, diceList.Where(x => x != dice).ToList(), (x) => x <= 6, 8);
                                    Piece.Column = pColumn;
                                    Piece.Row = pRow;
                                }
                            }
                        }
                        break;
                    case 1:
                        foreach (Dice dice in diceList)
                        {
                            c = dice.DiceValue;
                            if (Piece.AddPossibleMove(new PossibleLocation(c, 8, dice, 1)))
                            {
                                if (Piece.PieceStack.Count == 0)
                                {
                                    Piece.Column = c;
                                    Piece.Row = 8;
                                    SetPiecePossibleMoves_13(1, Piece, diceList.Where(x => x != dice).ToList(), (x) => x >= 8, 6);
                                    Piece.Column = pColumn;
                                    Piece.Row = pRow;
                                }
                            }
                        }
                        break;
                    case 2:
                        foreach (Dice dice in diceList)
                        {
                            c = 14 - dice.DiceValue;
                            if (Piece.AddPossibleMove(new PossibleLocation(c, 8, dice, 1)))
                            {
                                if (Piece.PieceStack.Count == 0)
                                {
                                    Piece.Column = c;
                                    Piece.Row = 8;
                                    SetPiecePossibleMoves_24(1, Piece, diceList.Where(x => x != dice).ToList(), (x) => x >= 8, 6);
                                    Piece.Column = pColumn;
                                    Piece.Row = pRow;
                                }
                            }
                        }
                        break;
                }
            }
        }

        private void SetPossibleMovesBarPiece(PLAYER PLAYER)
        {
            List<Dice> diceList = this.Controls.OfType<Dice>().Where(x => x.DiceValue != 0).ToList();

            int c = 0;
            PIECE Piece = PLAYER.PieceFromBar;
            int pColumn = Piece.Column;

            int pRow = Piece.Row;
            Piece.PossibleMovesList.Clear();

            switch (PLAYER.Zone)
            {
                case 3:
                    foreach (Dice dice in diceList)
                    {
                        c = dice.DiceValue;
                        if (Piece.AddPossibleMove(new PossibleLocation(c, 6, dice, 1)))
                        {
                            if (Piece.PieceStack.Count == 0)
                            {
                                Piece.Column = c;
                                Piece.Row = 6;
                                SetPiecePossibleMoves_13(1, Piece, diceList.Where(x => x != dice).ToList(), (x) => x <= 6, 8);
                                Piece.Column = pColumn;
                                Piece.Row = pRow;
                            }
                        }
                    }
                    break;
                case 4:
                    foreach (Dice dice in diceList)
                    {
                        c = 14 - dice.DiceValue;
                        if (Piece.AddPossibleMove(new PossibleLocation(c, 6, dice, 1)))
                        {
                            if (Piece.PieceStack.Count == 0)
                            {
                                Piece.Column = c;
                                Piece.Row = 6;
                                SetPiecePossibleMoves_24(1, Piece, diceList.Where(x => x != dice).ToList(), (x) => x <= 6, 8);
                                Piece.Column = pColumn;
                                Piece.Row = pRow;
                            }
                        }
                    }
                    break;
                case 1:
                    foreach (Dice dice in diceList)
                    {
                        c = dice.DiceValue;
                        if (Piece.AddPossibleMove(new PossibleLocation(c, 8, dice, 1)))
                        {
                            if (Piece.PieceStack.Count == 0)
                            {
                                Piece.Column = c;
                                Piece.Row = 8;
                                SetPiecePossibleMoves_13(1, Piece, diceList.Where(x => x != dice).ToList(), (x) => x >= 8, 6);
                                Piece.Column = pColumn;
                                Piece.Row = pRow;
                            }
                        }
                    }
                    break;
                case 2:
                    foreach (Dice dice in diceList)
                    {
                        c = 14 - dice.DiceValue;
                        if (Piece.AddPossibleMove(new PossibleLocation(c, 8, dice, 1)))
                        {
                            if (Piece.PieceStack.Count == 0)
                            {
                                Piece.Column = c;
                                Piece.Row = 8;
                                SetPiecePossibleMoves_24(1, Piece, diceList.Where(x => x != dice).ToList(), (x) => x >= 8, 6);
                                Piece.Column = pColumn;
                                Piece.Row = pRow;
                            }
                        }
                    }
                    break;
            }
        }

        private void SetPiecePossibleMoves_24(int nrDice, PIECE Piece, List<Dice> diceList, Func<int, bool> condition, int changedRow)
        {
            int column = Piece.Column;
            int row = Piece.Row;
            int c = 0;

            foreach (Dice dice in diceList)
            {
                if (condition(row))
                {
                    c = column - dice.DiceValue;
                    if (c >= 8)
                    {
                        nrDice++;
                        if (!Piece.AddPossibleMove(new PossibleLocation(c, row, dice, nrDice)))
                            break;
                    }
                    else if (c <= 7 && c >= 1)
                    {
                        if (column >= 8)
                        {
                            c--;
                        }
                        nrDice++;
                        if (!Piece.AddPossibleMove(new PossibleLocation(c, row, dice, nrDice)))
                            break;
                    }
                    else if (c <= 0)
                    {
                        c = c * (-1) + 1;
                        row = changedRow;
                        nrDice++;
                        if (!Piece.AddPossibleMove(new PossibleLocation(c, row, dice, nrDice)))
                            break;
                    }
                }
                else
                {
                    c = column + dice.DiceValue;
                    if (c <= 6)
                    {
                        nrDice++;
                        if (!Piece.AddPossibleMove(new PossibleLocation(c, row, dice, nrDice)))
                            break;
                    }
                    else if (c >= 7 && c <= 13)
                    {
                        if (column <= 6)
                        {
                            c++;
                        }
                        nrDice++;
                        if (!Piece.AddPossibleMove(new PossibleLocation(c, row, dice, nrDice)))
                            break;
                    }
                    else if (this.PiecesInZone(Piece.PLAYER.Zone, Piece.PLAYER.PiecesType) == Piece.PLAYER.CountPieces - 1 && !PieceInZone(Piece))
                    {
                        if (c == 14 || this.AllFreeToColum(column, Piece.PLAYER.Zone, Piece.PLAYER.PiecesType, Piece.Column, Piece.Row))
                        {
                            nrDice++;
                            if (!Piece.AddPossibleMove(new PossibleLocation(14, row, dice, nrDice)))
                                break;
                        }
                    }
                    else if (this.PiecesInZone(Piece.PLAYER.Zone, Piece.PLAYER.PiecesType) == Piece.PLAYER.CountPieces)
                    {
                        if (c == 14 || this.AllFreeToColum(column, Piece.PLAYER.Zone, Piece.PLAYER.PiecesType, Piece.Column, Piece.Row))// || this.AllFreeToColumLess1(c - dice.DiceValue, Piece.PLAYER.Zone, Piece.PLAYER.PiecesType) == 1) //1 pana la column - dice.DiceValue
                        {
                            nrDice++;
                            if (!Piece.AddPossibleMove(new PossibleLocation(14, row, dice, nrDice)))
                                break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                column = c;
                if (column > 14)
                    break;
            }
        }

        private void SetPiecePossibleMoves_13(int nrDice, PIECE Piece, List<Dice> diceList, Func<int, bool> condition, int changedRow)
        {
            int column = Piece.Column;
            int row = Piece.Row;
            int c = 0;

            foreach (Dice dice in diceList)
            {
                if (condition(row))
                {
                    c = column + dice.DiceValue;
                    if (c <= 6)
                    {
                        nrDice++;
                        if (!Piece.AddPossibleMove(new PossibleLocation(c, row, dice, nrDice)))
                            break;
                    }
                    else if (c >= 7 && c <= 13)
                    {
                        if (column <= 6)
                        {
                            c++;
                        }
                        nrDice++;
                        if (!Piece.AddPossibleMove(new PossibleLocation(c, row, dice, nrDice)))
                            break;
                    }
                    else if (c >= 14)
                    {
                        c = 14 - (c - 13);
                        row = changedRow;
                        nrDice++;
                        if (!Piece.AddPossibleMove(new PossibleLocation(c, row, dice, nrDice)))
                            break;
                    }
                }
                else
                {
                    c = column - dice.DiceValue;
                    if (c >= 8)
                    {
                        nrDice++;
                        if (!Piece.AddPossibleMove(new PossibleLocation(c, row, dice, nrDice)))
                            break;
                    }
                    else if (c <= 7 && c >= 1)
                    {
                        if (column >= 8)
                        {
                            c--;
                        }
                        nrDice++;
                        if (!Piece.AddPossibleMove(new PossibleLocation(c, row, dice, nrDice)))
                            break;
                    }
                    else if (this.PiecesInZone(Piece.PLAYER.Zone, Piece.PLAYER.PiecesType) == Piece.PLAYER.CountPieces - 1 && !PieceInZone(Piece))
                    {
                        if (c == 0 || this.AllFreeToColum(column, Piece.PLAYER.Zone, Piece.PLAYER.PiecesType, Piece.Column, Piece.Row))
                        {
                            nrDice++;
                            if (!Piece.AddPossibleMove(new PossibleLocation(0, row, dice, nrDice)))
                                break;
                        }
                    }
                    else if (this.PiecesInZone(Piece.PLAYER.Zone, Piece.PLAYER.PiecesType) == Piece.PLAYER.CountPieces)
                    {
                        if (c == 0 || this.AllFreeToColum(column, Piece.PLAYER.Zone, Piece.PLAYER.PiecesType, Piece.Column, Piece.Row))
                        {
                            nrDice++;
                            if (!Piece.AddPossibleMove(new PossibleLocation(0, row, dice, nrDice)))
                                break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                column = c;
                if (column < 0)
                    break;
            }
        }

        private List<PIECE> GetTablePieces(PLAYER PLAYER)
        {
            var piecesTop = this.Controls.OfType<PIECE>().Where(x => x.PieceType == PLAYER.PiecesType && x.Column > 0 && x.Column < 14 && x.Row <= 6);
            var piecesBottom = this.Controls.OfType<PIECE>().Where(x => x.PieceType == PLAYER.PiecesType && x.Column > 0 && x.Column < 14 && x.Row >= 8);

            var pTop = from e in piecesTop
                       orderby e.Row descending
                       group e by e.Column into gr
                       select gr.First();

            var pBottom = from e in piecesBottom
                          orderby e.Row ascending
                          group e by e.Column into gr
                          select gr.First();

            return pTop.Concat(pBottom).ToList();
        }

        private bool SafeHouse(PLAYER PLAYER)
        {
            int count = 0;
            int rowStart = 0, rowEnd = 0;
            int columnStart = 0, columnEnd = 0;
            switch (PLAYER.Zone)
            {
                case 2:
                    columnStart = 8; columnEnd = 13;
                    rowStart = 1; rowEnd = 6;
                    break;
                case 4:
                    columnStart = 8; columnEnd = 13;
                    rowStart = 8; rowEnd = 13;
                    break;
                case 1:
                    columnStart = 1; columnEnd = 6;
                    rowStart = 1; rowEnd = 6;
                    break;
                case 3:
                    columnStart = 1; columnEnd = 6;
                    rowStart = 8; rowEnd = 13;
                    break;
            }
            for (int i = columnStart; i <= columnEnd; i++)
            {
                count = 0;
                for (int j = rowStart; j <= rowEnd; j++)
                    if (GetControlFromPosition(i, j) != null && ((PIECE)GetControlFromPosition(i, j)).PieceType == PLAYER.PiecesType)
                    {
                        count++;
                        if (count == 2)
                            continue;
                    }
                if (count == 1)
                    return false;
            }
            return true;
        }

        private List<PIECE> GetUnsafeHousePieces(PLAYER PLAYER)
        {
            List<PIECE> unsafePieces = new List<PIECE>();
            int count;
            int rowStart = 0, rowEnd = 0;
            int columnStart = 0, columnEnd = 0;
            switch (PLAYER.Zone)
            {
                case 2:
                    columnStart = 8; columnEnd = 13;
                    rowStart = 1; rowEnd = 6;
                    break;
                case 4:
                    columnStart = 8; columnEnd = 13;
                    rowStart = 8; rowEnd = 13;
                    break;
                case 1:
                    columnStart = 1; columnEnd = 6;
                    rowStart = 1; rowEnd = 6;
                    break;
                case 3:
                    columnStart = 1; columnEnd = 6;
                    rowStart = 8; rowEnd = 13;
                    break;
            }
            for (int i = columnStart; i <= columnEnd; i++)
            {
                count = 0;
                int j;
                for (j = rowStart; j <= rowEnd; j++)
                    if (GetControlFromPosition(i, j) != null && ((PIECE)GetControlFromPosition(i, j)).PieceType == PLAYER.PiecesType)
                    {
                        count++;
                        if (count == 2)
                            break;
                    }
                    else break;
                if (count == 1)
                    unsafePieces.Add((PIECE)GetControlFromPosition(i, j - 1));
            }
            return unsafePieces;
        }

        private List<PIECE> GetHousePieces(PLAYER PLAYER)
        {
            List<PIECE> housePieces = new List<PIECE>();
            List<PIECE> pieces = GetTablePieces(PLAYER);

            switch (PLAYER.Zone)
            {
                case 1:
                    housePieces = pieces.FindAll(p => p.Column < 7 && p.Row < 7).ToList();
                    break;
                case 2:
                    housePieces = pieces.FindAll(p => p.Column > 7 && p.Row < 7).ToList();
                    break;
                case 3:
                    housePieces = pieces.FindAll(p => p.Column < 7 && p.Row > 7).ToList();
                    break;
                case 4:
                    housePieces = pieces.FindAll(p => p.Column > 7 && p.Row > 7).ToList();
                    break;
            }
            return housePieces;
        }

        private List<PIECE> GetUnsafeInOpponentHousePieces(PLAYER PLAYER, PLAYER COMPUTER)
        {
            List<PIECE> unsafePieces = new List<PIECE>();
            int count;
            int rowStart = 0, rowEnd = 0;
            int columnStart = 0, columnEnd = 0;
            switch (PLAYER.Zone)
            {
                case 2:
                    columnStart = 8; columnEnd = 13;
                    rowStart = 1; rowEnd = 6;
                    break;
                case 4:
                    columnStart = 8; columnEnd = 13;
                    rowStart = 8; rowEnd = 13;
                    break;
                case 1:
                    columnStart = 1; columnEnd = 6;
                    rowStart = 1; rowEnd = 6;
                    break;
                case 3:
                    columnStart = 1; columnEnd = 6;
                    rowStart = 8; rowEnd = 13;
                    break;
            }
            for (int i = columnStart; i <= columnEnd; i++)
            {
                count = 0;
                int j;
                for (j = rowStart; j <= rowEnd; j++)
                    if (GetControlFromPosition(i, j) != null && ((PIECE)GetControlFromPosition(i, j)).PieceType == COMPUTER.PiecesType)
                    {
                        count++;
                        if (count == 2)
                            break;
                    }
                    else break;
                if (count == 1)
                    unsafePieces.Add((PIECE)GetControlFromPosition(i, j - 1));
            }
            return unsafePieces;
        }

        private List<PIECE> GetInOpponentHousePieces(PLAYER PLAYER)
        {
            List<PIECE> inOpponentHousePieces = new List<PIECE>();
            List<PIECE> pieces = GetTablePieces(PLAYER);

            switch (PLAYER.Zone)
            {
                case 3:
                    inOpponentHousePieces = pieces.FindAll(p => p.Column < 7 && p.Row < 7).ToList();
                    break;
                case 4:
                    inOpponentHousePieces = pieces.FindAll(p => p.Column > 7 && p.Row < 7).ToList();
                    break;
                case 1:
                    inOpponentHousePieces = pieces.FindAll(p => p.Column < 7 && p.Row > 7).ToList();
                    break;
                case 2:
                    inOpponentHousePieces = pieces.FindAll(p => p.Column > 7 && p.Row > 7).ToList();
                    break;
            }
            return inOpponentHousePieces;
        }

        private List<PIECE> GetUnsafe2ZonePieces(PLAYER PLAYER)
        {
            List<PIECE> unsafePieces = new List<PIECE>();
            int count;
            int rowStart = 0, rowEnd = 0;
            int columnStart = 0, columnEnd = 0;
            switch (PLAYER.Zone)
            {
                case 2:
                case 4:
                    columnStart = 1; columnEnd = 6;
                    rowStart = 1; rowEnd = 6;
                    break;
                case 1:
                case 3:
                    columnStart = 8; columnEnd = 13;
                    rowStart = 1; rowEnd = 6;
                    break;
            }
            //up zone
            for (int i = columnStart; i <= columnEnd; i++)
            {
                count = 0;
                int j;
                for (j = rowStart; j <= rowEnd; j++)
                    if (GetControlFromPosition(i, j) != null && ((PIECE)GetControlFromPosition(i, j)).PieceType == PLAYER.PiecesType)
                    {
                        count++;
                        if (count == 2)
                            break;
                    }
                    else break;
                if (count == 1)
                    unsafePieces.Add((PIECE)GetControlFromPosition(i, j - 1));
            }

            //bottom zone
            for (int i = columnStart; i <= columnEnd; i++)
            {
                count = 0;
                int j;
                for (j = rowStart; j <= rowEnd; j++)
                    if (GetControlFromPosition(i, 14 - j) != null && ((PIECE)GetControlFromPosition(i, 14 - j)).PieceType == PLAYER.PiecesType)
                    {
                        count++;
                        if (count == 2)
                            break;
                    }
                    else break;
                if (count == 1)
                    unsafePieces.Add((PIECE)GetControlFromPosition(i, 14 - j + 1));
            }

            return unsafePieces;
        }

        private List<PIECE> GetNearZonePieces(PLAYER PLAYER)
        {
            List<PIECE> nearZoneHousePieces = new List<PIECE>();
            List<PIECE> pieces = GetTablePieces(PLAYER);

            switch (PLAYER.Zone)
            {
                case 2:
                    nearZoneHousePieces = pieces.FindAll(p => p.Column < 7 && p.Row < 7).ToList();
                    break;
                case 1:
                    nearZoneHousePieces = pieces.FindAll(p => p.Column > 7 && p.Row < 7).ToList();
                    break;
                case 4:
                    nearZoneHousePieces = pieces.FindAll(p => p.Column < 7 && p.Row > 7).ToList();
                    break;
                case 3:
                    nearZoneHousePieces = pieces.FindAll(p => p.Column > 7 && p.Row > 7).ToList();
                    break;
            }
            return nearZoneHousePieces;
        }

        private List<PIECE> GetNearNextZonePieces(PLAYER PLAYER)
        {
            List<PIECE> nearNextZoneHousePieces = new List<PIECE>();
            List<PIECE> pieces = GetTablePieces(PLAYER);

            switch (PLAYER.Zone)
            {
                case 4:
                    nearNextZoneHousePieces = pieces.FindAll(p => p.Column < 7 && p.Row < 7).ToList();
                    break;
                case 3:
                    nearNextZoneHousePieces = pieces.FindAll(p => p.Column > 7 && p.Row < 7).ToList();
                    break;
                case 2:
                    nearNextZoneHousePieces = pieces.FindAll(p => p.Column < 7 && p.Row > 7).ToList();
                    break;
                case 1:
                    nearNextZoneHousePieces = pieces.FindAll(p => p.Column > 7 && p.Row > 7).ToList();
                    break;
            }
            return nearNextZoneHousePieces;
        }

        private bool CoverPieceHouse(PLAYER COMPUTER, List<PIECE> UnsafeHousePieces)
        {
            List<PIECE> pieces = GetTablePieces(COMPUTER);
            //cauta possible moves peste piesa

            foreach (PIECE p in UnsafeHousePieces)
            {
                PIECE piece = pieces.Find(pi => pi.PossibleMovesList.Find(loc => loc.Column == p.Column && ((loc.Row  < 7 && p.Row < 7)||(loc.Row > 7 && p.Row > 7))) != null);
                if (piece != null)
                {
                    piece.MovePiece(p.Column, p.Row);
                    return true;
                }
            }
            return false;
        }

        private bool CoverPiece2Zone(PLAYER PLAYER, List<PIECE> Unsafe2ZonePieces)
        {
            List<PIECE> pieces = GetTablePieces(PLAYER);
            PIECE piece;

            foreach (PIECE p in Unsafe2ZonePieces)
            {
                piece = pieces.Find(pi => pi.PossibleMovesList.Find(loc => loc.Column == p.Column && ((loc.Row < 7 && p.Row < 7) || (loc.Row > 7 && p.Row > 7))) != null);
                if (piece != null)
                {
                    piece.MovePiece(p.Column, p.Row);
                    return true;
                }
                if (p.PossibleMovesList.Count > 0)
                {
                    foreach (PossibleLocation loc in p.PossibleMovesList)
                    {
                        piece = pieces.Find(pi => pi.Column == loc.Column && ((loc.Row < 7 && pi.Row < 7) || (loc.Row > 7 && pi.Row > 7)));
                        if (piece != null)
                        {
                            p.MovePiece(piece.Column, piece.Row);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool MoveFromOpponentHouse(PLAYER PLAYER, List<PIECE> UnsafeInOpponentHousePieces)
        {
            List<PIECE> pieces = GetTablePieces(PLAYER);
            PIECE piece;

            foreach (PIECE p in UnsafeInOpponentHousePieces)
            {
                if (p.PossibleMovesList.Count > 0)
                {
                    //incearca sa muti piesa safe
                    foreach (PossibleLocation loc in p.PossibleMovesList)
                    {
                        piece = pieces.Find(pi => pi.Column == loc.Column && ((loc.Row < 7 && pi.Row < 7) || (loc.Row > 7 && pi.Row > 7)));
                        if (piece != null)
                        {
                            p.MovePiece(piece.Column, piece.Row);
                            return true;
                        }
                    }

                    //cauta sa muti o piesa peste
                    piece = pieces.Find(pi => pi.PossibleMovesList.Find(loc => loc.Column == p.Column && ((loc.Row < 7 && p.Row < 7) || (loc.Row > 7 && p.Row > 7))) != null);
                    if (piece != null)
                    {
                        piece.MovePiece(p.Column, p.Row);
                        return true;
                    }
                }
            }

            return false;
        }

        private void MovePieceOutComputer(PLAYER COMPUTER)
        {
            List<PIECE> HousePieces = null;
            switch(COMPUTER.Zone)
            {
                case 1:
                    var pieces1 = this.Controls.OfType<PIECE>().Where(x => x.PieceType == COMPUTER.PiecesType && x.Column > 0 && x.Column < 7 && x.Row <= 6).ToList();
                    var housePieces1 = from e in pieces1
                                       orderby e.Row descending
                                       group e by e.Column into gr
                                       select gr.First();
                    HousePieces = housePieces1.ToList();
                    break;
                case 2:
                    var pieces2 = this.Controls.OfType<PIECE>().Where(x => x.PieceType == COMPUTER.PiecesType && x.Column > 7 && x.Column < 14 && x.Row <= 6).ToList();
                    var housePieces2 = from e in pieces2
                                       orderby e.Row descending
                                       group e by e.Column into gr
                                       select gr.First();
                    HousePieces = housePieces2.ToList();
                    break;
                case 3:
                    var pieces3 = this.Controls.OfType<PIECE>().Where(x => x.PieceType == COMPUTER.PiecesType && x.Column > 0 && x.Column < 7 && x.Row > 7).ToList();
                    var housePieces3 = from e in pieces3
                                       orderby e.Row ascending
                                       group e by e.Column into gr
                                       select gr.First();
                    HousePieces = housePieces3.ToList();
                    break;
                case 4:
                    var pieces4 = this.Controls.OfType<PIECE>().Where(x => x.PieceType == COMPUTER.PiecesType && x.Column > 7 && x.Column < 14 && x.Row > 7).ToList();
                    var housePieces4 = from e in pieces4
                                       orderby e.Row ascending
                                       group e by e.Column into gr
                                       select gr.First();
                    HousePieces = housePieces4.ToList();
                    break;
            }

            PIECE p = HousePieces.Find(pi => pi.PossibleMovesList.Count > 0);
            p.MovePiece(p.PossibleMovesList[0].Column, p.PossibleMovesList[0].Row);
        }

        private void MoveRandomPiece(PLAYER COMPUTER)
        {
            List<PIECE> Pieces = null;
            var piecesTop = this.Controls.OfType<PIECE>().Where(x => x.PieceType == COMPUTER.PiecesType && x.Column > 0 && x.Column < 14 && x.Row <= 6);
            var piecesBottom = this.Controls.OfType<PIECE>().Where(x => x.PieceType == COMPUTER.PiecesType && x.Column > 0 && x.Column < 14 && x.Row >= 8);

            var pTop = from e in piecesTop
                       orderby e.Row descending
                       group e by e.Column into gr
                       select gr.First();

            var pBottom = from e in piecesBottom
                          orderby e.Row ascending
                          group e by e.Column into gr
                          select gr.First();

            Pieces = pTop.Concat(pBottom).ToList();

            while (true)
            {
                int rand = new Random().Next(0, Pieces.Count);
                if (Pieces[rand].PossibleMovesList.Count > 0)
                {
                    PIECE p = Pieces[rand];
                    p.MovePiece(p.PossibleMovesList[0].Column, p.PossibleMovesList[0].Row);
                    return;
                }
            }
        }

        public bool? ComputerMovePiece(PLAYER COMPUTER, PLAYER OPPONENT, int moves, bool DoubleDice)
        {
            if (COMPUTER.HasPiecesOnBar)
            {
                Console.WriteLine("Piesa pe bara");
                SetPossibleMovesBarPiece(COMPUTER); //Possible moves for bar piece
                if (COMPUTER.CountPossibleMoves == 0)
                    return false;
                PIECE p = (PIECE)this.GetControlFromPosition(7, COMPUTER.BarRow);

                if (DoubleDice)
                {
                    Console.WriteLine("DoubleDice = true");
                    p.MovePiece(p.PossibleMovesList[0].Column, p.PossibleMovesList[0].Row);
                    Console.WriteLine("Muta piesa pe zar");
                    return true;
                }
                //zar normal
                Console.WriteLine("DoubleDice = false");
                if (SafeHouse(COMPUTER))
                {
                    Console.WriteLine("SafeHouse = true, Cauta sa il scoata pe oponent");
                    for (int i = 0; i < p.PossibleMovesList.Count; i++)
                    {
                        if (GetFirstPieceFromColumn(p.PossibleMovesList[i].Column, p.PossibleMovesList[i].Row) != null)
                        {
                            if (GetFirstPieceFromColumn(p.PossibleMovesList[i].Column, p.PossibleMovesList[i].Row).PieceType != COMPUTER.PiecesType
                                && CountPiecesFromColumn(p.PossibleMovesList[i].Column, p.PossibleMovesList[i].Row) == 1)
                            {
                                p.MovePiece(p.PossibleMovesList[i].Column, p.PossibleMovesList[i].Row);
                                Console.WriteLine("Muta piesa sa scoti oponent");
                                return true;
                            }
                        }
                    }
                }
                //nu e safe house
                Console.WriteLine("SafeHouse = false");
                if (p.PieceStack.Count > 0)
                {
                    Console.WriteLine(">=2 piese pe bara");
                    p.MovePiece(p.PossibleMovesList[0].Column, p.PossibleMovesList[0].Row);
                    Console.WriteLine("Muta piesa pe zar ca nu ai ce face");
                    return true;
                }
                else
                {
                    Console.WriteLine("1 piesa pe bara");
                    if (moves == 1)
                    {
                        Console.WriteLine("1 mutare posibila");
                        p.MovePiece(p.PossibleMovesList[0].Column, p.PossibleMovesList[0].Row);
                        Console.WriteLine("Muta piesa pe zar ca mai e doar 1 mutare");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Mai mute mutari posibile");
                        //TODO Cauta cea mai favorabila mutare pe zarul urmator
                        //for now moves random
                        int rand = new Random().Next(0, p.PossibleMovesList.Count);
                        p.MovePiece(p.PossibleMovesList[rand].Column, p.PossibleMovesList[rand].Row);
                        Console.WriteLine("Muta piesa pe zar random");
                        return true;
                    }
                }
            }
            else //no pieces on BAR
            {
                Console.WriteLine("Fara piesa pe bara");
                SetPossibleMoves(COMPUTER);
                if (COMPUTER.CountPossibleMoves == 0)
                    return false;
                if(this.PiecesInZone(COMPUTER.Zone, COMPUTER.PiecesType) < COMPUTER.CountPieces)
                {
                    Console.WriteLine("Nu toate piesele in casa");
                    #region PRIORITATE 1 (Acopera piesele tale libere)
                    if (!SafeHouse(COMPUTER))
                    {
                        Console.WriteLine("Piese singure in casa");
                        //cauta piese singure
                        List<PIECE> unsafeHousePieces = GetUnsafeHousePieces(COMPUTER);

                        foreach (PIECE p in unsafeHousePieces)
                            Console.WriteLine($"Name= {p.Name}, c={p.Column}, r={p.Row}");

                        if (unsafeHousePieces.Count > 0)
                        {
                            if (CoverPieceHouse(COMPUTER, unsafeHousePieces))
                            {
                                Console.WriteLine("Piesa acoperita in casa");
                                return true;
                            }
                        }
                    }
                    //nu se poate acoperi piesa din casa
                    Console.WriteLine("Piese SIGURE in casa");
                    List<PIECE> unsafe2ZonePieces = GetUnsafe2ZonePieces(COMPUTER);
                    if(unsafe2ZonePieces.Count > 0)
                    {
                        foreach (PIECE p in unsafe2ZonePieces)
                            Console.WriteLine($"Name= {p.Name}, c={p.Column}, r={p.Row}");

                        if (CoverPiece2Zone(COMPUTER, unsafe2ZonePieces))
                        {
                            Console.WriteLine("Piesa acoperita in 2 zone");
                            return true;
                        }
                    }
                    //nu se poate face mutare safe in 2Zone

                    Console.WriteLine("Cauta piesa in casa oponent singura");
                    List<PIECE> unsafeInOpponentHousePieces = GetUnsafeInOpponentHousePieces(OPPONENT, COMPUTER);
                    if(unsafeInOpponentHousePieces.Count > 0)
                    {
                        foreach (PIECE p in unsafeInOpponentHousePieces)
                            Console.WriteLine($"Name= {p.Name}, c={p.Column}, r={p.Row}");

                        if (MoveFromOpponentHouse(COMPUTER, unsafeInOpponentHousePieces))
                        {
                            Console.WriteLine("Piesa safe din casa oponentului");
                            return true;
                        }
                    }
                    //nu se poate face mutare in casa oponentului
                    #endregion

                    Console.WriteLine("Cauta sa muti orice piesa safe ---------");
                    PIECE piece;

                    #region PRIORITATE 2 (Muta-ti piesele safe)
                    //incearca mutare in casa oponentului
                    piece = unsafeInOpponentHousePieces.Find(p => p.PossibleMovesList.Count > 0);
                    if (piece != null)
                    {
                        Console.WriteLine("Piesa unsafe din casa oponent rezolvata");
                        piece.MovePiece(piece.PossibleMovesList[0].Column, piece.PossibleMovesList[0].Row);
                        return true;
                    }

                    //incearca mutare in 2 zone
                    piece = unsafe2ZonePieces.Find(p => p.PossibleMovesList.Count > 0);
                    if (piece != null)
                    {
                        Console.WriteLine("Piesa unsafe din 2Zone rezolvata");
                        piece.MovePiece(piece.PossibleMovesList[0].Column, piece.PossibleMovesList[0].Row);
                        return true;
                    }
                    #endregion

                    //nu exista mutare safe
                    #region PRIORITATE 3 (Muta o piesa din casa oponentului spre casa ta)
                    List<PIECE> inOpponentHousePieces = GetInOpponentHousePieces(COMPUTER);
                    piece = inOpponentHousePieces.Find(p => p.PossibleMovesList.Count > 0);
                    if (piece != null)
                    {
                        Console.WriteLine("Muta o piesa din casa oponent");
                        piece.MovePiece(piece.PossibleMovesList[0].Column, piece.PossibleMovesList[0].Row);
                        return true;
                    }

                    List<PIECE> nearNextZonePieces = GetNearNextZonePieces(COMPUTER);
                    piece = nearNextZonePieces.Find(p => p.PossibleMovesList.Count > 0);
                    if (piece != null)
                    {
                        Console.WriteLine("Muta o piesa din near next zone");
                        piece.MovePiece(piece.PossibleMovesList[0].Column, piece.PossibleMovesList[0].Row);
                        return true;
                    }

                    List<PIECE> nearZonePieces = GetNearZonePieces(COMPUTER);
                    if (piece != null)
                    {
                        Console.WriteLine("Muta o piesa din near zone");
                        piece.MovePiece(piece.PossibleMovesList[0].Column, piece.PossibleMovesList[0].Row);
                        return true;
                    }
                    #endregion

                    Console.WriteLine("Muta piesa random");
                    MoveRandomPiece(COMPUTER);
                    return true;

                }
                else //toate piese in casa
                {
                    Console.WriteLine("Toate piesele in casa");
                    //TODO Can be improved
                    MovePieceOutComputer(COMPUTER);
                    return true;

                }
            }
        }
    }
}
