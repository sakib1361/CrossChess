using CrossChessEngine.Model.Data;
using CrossChessEngine.Model.PieceModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossChess.Model
{
    public class SquareData
    {
        public SquareData(Square square)
        {
            _square = square;
            Column = square.File;
            Row = square.Rank;
            ID = square.Ordinal;
            ColorName = square.Colour;
            Piece = square.Piece;
        }

        private Square _square;

        public int Column { get; }
        public int Row { get; }
        public int ID { get; private set; }
        public Square.ColourNames ColorName { get; }
        public Piece Piece { get; set; }
        public bool AvailableMove { get; set; }
        public bool LastMove { get; set; }
    }
}
