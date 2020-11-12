using CrossChessEngine.Model.Data;
using CrossChessEngine.Model.PieceModel;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CrossChess.Model
{
    [AddINotifyPropertyChangedInterface]
    public class SquareData
    {
        static Color dark = Color.FromArgb(122, 77, 78);
        static Color light = Color.FromArgb(199, 157, 96);
        public SquareData(Square square)
        {
            Square = square;
            Column =  square.File;
            Row =7- square.Rank;
            ID = square.Ordinal;
            Piece = square.Piece;
            BoardColor = square.Colour == Square.ColourNames.Black ? dark : light;
        }

        public Square Square { get; }

        public int Column { get; }
        public int Row { get; }
        public int ID { get; private set; }
        public Piece Piece { get; set; }
        public Color BoardColor { get; }
        public bool AvailableMove { get; set; }
        public bool LastMove { get; set; }
        public Move PieceMove { get; internal set; }

        internal void UpdatePiece(Move lastMove)
        {
            Piece = Square.Piece;
            LastMove = lastMove?.From.Ordinal == ID || lastMove?.To.Ordinal == ID;
        }
    }
}
