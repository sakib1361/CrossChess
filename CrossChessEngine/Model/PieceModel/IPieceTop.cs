using CrossChessEngine.Model.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossChessEngine.Model.PieceModel
{
    public interface IPieceTop
    {
        #region Public Properties

        /// <summary>
        /// Gets the Abbreviated name for the piece.
        /// </summary>
        string Abbreviation { get; }

        /// <summary>
        /// Gets the base <see cref="Piece"/>.
        /// </summary>
        Piece Base { get; }

        /// <summary>
        /// Gets the BasicValue for this piece. e.g. 9 for Queen, 1 for a Pawn.
        /// </summary>
        int BasicValue { get; }

        /// <summary>
        /// Gets ImageIndex for the piece.
        /// </summary>
        int ImageIndex { get; }

        /// <summary>
        /// Gets a value indicating whether the piece can be captured.
        /// </summary>
        bool IsCapturable { get; }

        /// <summary>
        /// Gets the name of the piece.
        /// </summary>
        PieceNames Name { get; }

        /// <summary>
        /// Gets the positional score points of the piece.
        /// </summary>
        int PositionalPoints { get; }

        /// <summary>
        /// Gets the base score value for the piece e.g. 9000 for Queen, 1000 for a Pawn.
        /// </summary>
        int Value { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// The generate lazy moves.
        /// </summary>
        /// <param name="moves">
        /// The moves.
        /// </param>
        /// <param name="movesType">
        /// The moves type.
        /// </param>
        void GenerateLazyMoves(Moves moves, Moves.MoveListNames movesType);

        bool CanAttackSquare(Square square);

        #endregion
    }
}
