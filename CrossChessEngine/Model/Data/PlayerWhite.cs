using CrossChessEngine.Model.PieceModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossChessEngine.Model.Data
{
    public class PlayerWhite : Player
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerWhite"/> class.
        /// </summary>
        public PlayerWhite()
        {
            this.Colour = PlayerColourNames.White;
            this.Intellegence = PlayerIntellegenceNames.Human;

            this.SetPiecesAtStartingPositions();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets PawnAttackLeftOffset.
        /// </summary>
        public override int PawnAttackLeftOffset
        {
            get
            {
                return 15;
            }
        }

        /// <summary>
        /// Gets PawnAttackRightOffset.
        /// </summary>
        public override int PawnAttackRightOffset
        {
            get
            {
                return 17;
            }
        }

        /// <summary>
        /// Gets PawnForwardOffset.
        /// </summary>
        public override int PawnForwardOffset
        {
            get
            {
                return 16;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The set pieces at starting positions.
        /// </summary>
        protected override sealed void SetPiecesAtStartingPositions()
        {
            this.Pieces.Add(this.King = new Piece(PieceNames.King, this, 4, 0, Piece.PieceIdentifierCodes.WhiteKing));

            this.Pieces.Add(new Piece(PieceNames.Queen, this, 3, 0, Piece.PieceIdentifierCodes.WhiteQueen));

            this.Pieces.Add(new Piece(PieceNames.Rook, this, 0, 0, Piece.PieceIdentifierCodes.WhiteQueensRook));
            this.Pieces.Add(new Piece(PieceNames.Rook, this, 7, 0, Piece.PieceIdentifierCodes.WhiteKingsRook));

            this.Pieces.Add(new Piece(PieceNames.Bishop, this, 2, 0, Piece.PieceIdentifierCodes.WhiteQueensBishop));
            this.Pieces.Add(new Piece(PieceNames.Bishop, this, 5, 0, Piece.PieceIdentifierCodes.WhiteKingsBishop));

            this.Pieces.Add(new Piece(PieceNames.Knight, this, 1, 0, Piece.PieceIdentifierCodes.WhiteQueensKnight));
            this.Pieces.Add(new Piece(PieceNames.Knight, this, 6, 0, Piece.PieceIdentifierCodes.WhiteKingsKnight));

            this.Pieces.Add(new Piece(PieceNames.Pawn, this, 0, 1, Piece.PieceIdentifierCodes.WhitePawn1));
            this.Pieces.Add(new Piece(PieceNames.Pawn, this, 1, 1, Piece.PieceIdentifierCodes.WhitePawn2));
            this.Pieces.Add(new Piece(PieceNames.Pawn, this, 2, 1, Piece.PieceIdentifierCodes.WhitePawn3));
            this.Pieces.Add(new Piece(PieceNames.Pawn, this, 3, 1, Piece.PieceIdentifierCodes.WhitePawn4));
            this.Pieces.Add(new Piece(PieceNames.Pawn, this, 4, 1, Piece.PieceIdentifierCodes.WhitePawn5));
            this.Pieces.Add(new Piece(PieceNames.Pawn, this, 5, 1, Piece.PieceIdentifierCodes.WhitePawn6));
            this.Pieces.Add(new Piece(PieceNames.Pawn, this, 6, 1, Piece.PieceIdentifierCodes.WhitePawn7));
            this.Pieces.Add(new Piece(PieceNames.Pawn, this, 7, 1, Piece.PieceIdentifierCodes.WhitePawn8));
        }

        #endregion
    }
}
