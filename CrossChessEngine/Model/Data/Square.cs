using CrossChessEngine.Model.PieceModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossChessEngine.Model.Data
{
    public class Square
    {
        #region Constants and Fields

        /// <summary>
        /// Simple square values.
        /// </summary>
        private static readonly int[] SquareValues =
        {
            1,  1,  1,  1,  1,  1,  1, 1,    0, 0, 0, 0, 0, 0, 0, 0,
            1, 10, 10, 10, 10, 10, 10, 1,    0, 0, 0, 0, 0, 0, 0, 0,
            1, 10, 25, 25, 25, 25, 10, 1,    0, 0, 0, 0, 0, 0, 0, 0,
            1, 10, 25, 50, 50, 25, 10, 1,    0, 0, 0, 0, 0, 0, 0, 0,
            1, 10, 25, 50, 50, 25, 10, 1,    0, 0, 0, 0, 0, 0, 0, 0,
            1, 10, 25, 25, 25, 25, 10, 1,    0, 0, 0, 0, 0, 0, 0, 0,
            1, 10, 10, 10, 10, 10, 10, 1,    0, 0, 0, 0, 0, 0, 0, 0,
            1,  1,  1,  1,  1,  1,  1, 1,    0, 0, 0, 0, 0, 0, 0, 0
        };

        /// <summary>
        /// The king attackers.
        /// </summary>
        private static char[] kingAttackers =
        {
            '.', '.', '.', '.', '.', '.', '.', '.',   '.', '.', '.', '.', '.', '.', '.', '.',
            '.', '.', '.', '.', '.', '.', '.', '.',   '.', '.', '.', '.', '.', '.', '.', '.',
            '.', '.', '.', '.', '.', '.', '.', '.',   '.', '.', '.', '.', '.', '.', '.', '.',
            '.', '.', '.', '.', '.', '.', '.', '.',   '.', '.', '.', '.', '.', '.', '.', '.',
            '.', '.', '.', '.', '.', '.', '.', '.',   '.', '.', '.', '.', '.', '.', '.', '.',
            '.', '.', '.', '.', '.', '.', '.', '.',   '.', '.', '.', '.', '.', '.', '.', '.',
            '.', '.', '.', '.', '.', '.', '.', '.',   '.', '.', '.', '.', '.', '.', '.', 'K',
            'K', 'K', '.', '.', '.', '.', '.', '.',   '.', '.', '.', '.', '.', '.', '.', 'K',
            '.',
            'K', '.', '.', '.', '.', '.', '.', '.',   '.', '.', '.', '.', '.', '.', 'K', 'K',
            'K', '.', '.', '.', '.', '.', '.', '.',   '.', '.', '.', '.', '.', '.', '.', '.',
            '.', '.', '.', '.', '.', '.', '.', '.',   '.', '.', '.', '.', '.', '.', '.', '.',
            '.', '.', '.', '.', '.', '.', '.', '.',   '.', '.', '.', '.', '.', '.', '.', '.',
            '.', '.', '.', '.', '.', '.', '.', '.',   '.', '.', '.', '.', '.', '.', '.', '.',
            '.', '.', '.', '.', '.', '.', '.', '.',   '.', '.', '.', '.', '.', '.', '.', '.',
            '.', '.', '.', '.', '.', '.', '.', '.',   '.', '.', '.', '.', '.', '.', '.', '.',
            '.', '.', '.', '.', '.', '.', '.', '.',   '.', '.', '.', '.', '.', '.', '.', '.'
        };

        /// <summary>
        /// The minor attackers.
        /// </summary>
        private static char[] minorAttackers =
        {
            '.', '.', '.', '.', '.', '.', '.', '.',   'B', 'B', '.', '.', '.', '.', '.', '.',
            'R', '.', '.', '.', '.', '.', '.', 'B',   '.', '.', 'B', '.', '.', '.', '.', '.',
            'R', '.', '.', '.', '.', '.', 'B', '.',   '.', '.', '.', 'B', '.', '.', '.', '.',
            'R', '.', '.', '.', '.', 'B', '.', '.',   '.', '.', '.', '.', 'B', '.', '.', '.',
            'R', '.', '.', '.', 'B', '.', '.', '.',   '.', '.', '.', '.', '.', 'B', '.', '.',
            'R', '.', '.', 'B', '.', '.', '.', '.',   '.', '.', '.', '.', '.', '.', 'B', 'N',
            'R', 'N', 'B', '.', '.', '.', '.', '.',   '.', '.', '.', '.', '.', '.', 'N', 'B',
            'R', 'B', 'N', '.', '.', '.', '.', '.',   '.', 'R', 'R', 'R', 'R', 'R', 'R', 'R',
            '.',
            'R', 'R', 'R', 'R', 'R', 'R', 'R', '.',   '.', '.', '.', '.', '.', 'N', 'B', 'R',
            'B', 'N', '.', '.', '.', '.', '.', '.',   '.', '.', '.', '.', '.', 'B', 'N', 'R',
            'N', 'B', '.', '.', '.', '.', '.', '.',   '.', '.', '.', '.', 'B', '.', '.', 'R',
            '.', '.', 'B', '.', '.', '.', '.', '.',   '.', '.', '.', 'B', '.', '.', '.', 'R',
            '.', '.', '.', 'B', '.', '.', '.', '.',   '.', '.', 'B', '.', '.', '.', '.', 'R',
            '.', '.', '.', '.', 'B', '.', '.', '.',   '.', 'B', '.', '.', '.', '.', '.', 'R',
            '.', '.', '.', '.', '.', 'B', '.', '.',   'B', '.', '.', '.', '.', '.', '.', 'R',
            '.', '.', '.', '.', '.', '.', 'B', 'B',   '.', '.', '.', '.', '.', '.', '.', '.'
        };

        /// <summary>
        /// The queen attackers.
        /// </summary>
        private static char[] queenAttackers =
        {
            '.', '.', '.', '.', '.', '.', '.', '.', 'Q', 'Q', '.', '.', '.', '.', '.', '.',
            'Q', '.', '.', '.', '.', '.', '.', 'Q',   '.', '.', 'Q', '.', '.', '.', '.', '.',
            'Q', '.', '.', '.', '.', '.', 'Q', '.',   '.', '.', '.', 'Q', '.', '.', '.', '.',
            'Q', '.', '.', '.', '.', 'Q', '.', '.',   '.', '.', '.', '.', 'Q', '.', '.', '.',
            'Q', '.', '.', '.', 'Q', '.', '.', '.',   '.', '.', '.', '.', '.', 'Q', '.', '.',
            'Q', '.', '.', 'Q', '.', '.', '.', '.',   '.', '.', '.', '.', '.', '.', 'Q', '.',
            'Q', '.', 'Q', '.', '.', '.', '.', '.',   '.', '.', '.', '.', '.', '.', '.', 'Q',
            'Q', 'Q', '.', '.', '.', '.', '.', '.',   '.', 'Q', 'Q', 'Q', 'Q', 'Q', 'Q', 'Q',
            '.',
            'Q', 'Q', 'Q', 'Q', 'Q', 'Q', 'Q', '.',   '.', '.', '.', '.', '.', '.', 'Q', 'Q',
            'Q', '.', '.', '.', '.', '.', '.', '.',   '.', '.', '.', '.', '.', 'Q', '.', 'Q',
            '.', 'Q', '.', '.', '.', '.', '.', '.',   '.', '.', '.', '.', 'Q', '.', '.', 'Q',
            '.', '.', 'Q', '.', '.', '.', '.', '.',   '.', '.', '.', 'Q', '.', '.', '.', 'Q',
            '.', '.', '.', 'Q', '.', '.', '.', '.',   '.', '.', 'Q', '.', '.', '.', '.', 'Q',
            '.', '.', '.', '.', 'Q', '.', '.', '.',   '.', 'Q', '.', '.', '.', '.', '.', 'Q',
            '.', '.', '.', '.', '.', 'Q', '.', '.',   'Q', '.', '.', '.', '.', '.', '.', 'Q',
            '.', '.', '.', '.', '.', '.', 'Q', 'Q',   '.', '.', '.', '.', '.', '.', '.', '.'
        };

        /// <summary>
        /// The vectors.
        /// </summary>
        private static int[] vectors =
        {
              0,   0,   0,   0,   0,   0,   0,  0,   -15, -17,   0,   0,   0,   0,   0,   0,
            -16,   0,   0,   0,   0,   0,   0, -15,    0,   0, -17,   0,   0,   0,   0,   0,
            -16,   0,   0,   0,   0,   0, -15,  0,     0,   0,   0, -17,   0,   0,   0,   0,
            -16,   0,   0,   0,   0, -15,   0,  0,     0,   0,   0,   0, -17,   0,   0,   0,
            -16,   0,   0,   0, -15,   0,   0,  0,     0,   0,   0,   0,   0, -17,   0,   0,
            -16,   0,   0, -15,   0,   0,   0,  0,     0,   0,   0,   0,   0,   0, -17, 100,
            -16, 100, -15,   0,   0,   0,   0,  0,     0,   0,   0,   0,   0,   0, 100, -17,
            -16, -15, 100,   0,   0,   0,   0,  0,     0,  -1,  -1,  -1,  -1,  -1,  -1,  -1,
              0,
              1,  1,    1,   1,   1,   1,   1,  0,     0,   0,   0,   0,   0, 100,  15,  16,
             17, 100,   0,   0,   0,   0,   0,  0,     0,   0,   0,   0,   0,  15, 100,  16,
            100,  17,   0,   0,   0,   0,   0,  0,     0,   0,   0,   0,  15,   0,   0,  16,
              0,   0,  17,   0,   0,   0,   0,  0,     0,   0,   0,  15,   0,   0,   0,  16,
              0,   0,   0,  17,   0,   0,   0,  0,     0,   0,  15,   0,   0,   0,   0,  16,
              0,   0,   0,   0,  17,   0,   0,  0,     0,  15,   0,   0,   0,   0,   0,  16,
              0,   0,   0,   0,   0,  17,   0,  0,    15,   0,   0,   0,   0,   0,   0,  16,
              0,   0,   0,   0,   0,   0,  17, 15,     0,   0,   0,   0,   0,   0,   0,   0
        };
        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Square"/> class.
        /// </summary>
        /// <param name="ordinal">
        /// The ordinal index of this square.
        /// </param>
        public Square(int ordinal)
        {
            this.Ordinal = ordinal;
            this.File = ordinal % Board.MatrixWidth;
            this.Rank = ordinal / Board.MatrixWidth;
            this.Colour = (this.File % 2 == this.Rank % 2) ? ColourNames.Black : ColourNames.White;
        }

        #endregion

        #region Enums

        /// <summary>
        /// Possible sqaure colours: black or white.
        /// </summary>
        public enum ColourNames
        {
            /// <summary>
            ///   The white.
            /// </summary>
            White,

            /// <summary>
            ///   The black.
            /// </summary>
            Black
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets the colour of this square: black or white!
        /// </summary>
        public ColourNames Colour { get; private set; }

        /// <summary>
        ///   Gets file number for this square.
        /// </summary>
        public int File { get; private set; }

        /// <summary>
        ///   Gets the file letter for this square.
        /// </summary>
        public string FileName
        {
            get
            {
                string[] fileNames = { "a", "b", "c", "d", "e", "f", "g", "h" };
                return fileNames[this.File];
            }
        }

        /// <summary>
        ///   Gets HashCodeA.
        /// </summary>
        public ulong HashCodeA
        {
            get
            {
                return this.Piece == null ? 0UL : this.Piece.HashCodeAForSquareOrdinal(this.Ordinal);
            }
        }

        /// <summary>
        ///   Gets HashCodeB.
        /// </summary>
        public ulong HashCodeB
        {
            get
            {
                return this.Piece == null ? 0UL : this.Piece.HashCodeBForSquareOrdinal(this.Ordinal);
            }
        }

        /// <summary>
        ///   Gets the display name fo this square.
        /// </summary>
        public string Name
        {
            get
            {
                return this.FileName + this.RankName;
            }
        }

        /// <summary>
        ///   Gets the ordinal index of this square.
        /// </summary>
        public int Ordinal { get; private set; }

        /// <summary>
        ///   Gets or sets Piece.
        /// </summary>
        public Piece Piece { get; set; }

        /// <summary>
        ///   Gets Rank.
        /// </summary>
        public int Rank { get; private set; }

        /// <summary>
        ///   Gets RankName.
        /// </summary>
        public string RankName
        {
            get
            {
                return (this.Rank + 1).ToString();
            }
        }

        /// <summary>
        ///   Gets a simple positonal value for this square.
        /// </summary>
        public int Value
        {
            get
            {
                return SquareValues[this.Ordinal];
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends a list of moves of all the pieces that are attacking this square.
        /// </summary>
        /// <param name="moves">
        /// Moves of pieces that are attacking this square.
        /// </param>
        /// <param name="player">
        /// Player whose turn it is
        /// </param>
        public void AttackersMoveList(Moves moves, Player player)
        {
            foreach (Piece p in player.Pieces)
            {
                if (p.CanAttackSquare(this))
                    moves.Add(0, 0, Move.MoveNames.Standard, p, p.Square, this, this.Piece, 0, 0);
            }
        }

        /// <summary>
        /// Returns a list of player's pieces attacking this square.
        /// </summary>
        /// <param name="player">
        /// Player who owns the attacking pieces that you want to find.
        /// </param>
        /// <returns>
        /// List of pieces.
        /// </returns>
        public Pieces PlayersPiecesAttackingThisSquare(Player player)
        {
            Pieces pieces = new Pieces();

            foreach (Piece p in player.Pieces)
            {
                if (p.CanAttackSquare(this))
                    pieces.Add(p);
            }

            return pieces;
        }

        /// <summary>
        /// Determines whether the specified player can attack this square.
        /// </summary>
        /// <param name="player">
        /// The player being tested. (
        /// </param>
        /// <returns>
        /// True if player can move a piece to this square.
        /// </returns>
        public bool PlayerCanAttackSquare(Player player)
        {

            foreach (PieceModel.PieceNames pieceName in player.PieceTypes())
            {
                if (Piece.CanPlayerPieceNameAttackSquare(this, player, pieceName))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether a sliding piece could slide to this square from the specified start square, 
        /// in the specified direction-offset. Checks that no pieces are blocking the route.
        /// </summary>
        /// <param name="squareStart">
        /// The starting square.
        /// </param>
        /// <param name="directionOffset">
        /// The direciton offset.
        /// </param>
        /// <returns>
        /// True if the piece can be slid.
        /// </returns>
        /// <exception cref="ApplicationException">
        /// An exception indicting that the alogrithm has hit the edge of the board.
        /// </exception>
        public bool CanSlideToHereFrom(Square squareStart, int directionOffset)
        {
            int intOrdinal = squareStart.Ordinal;
            Square square;

            intOrdinal += directionOffset;
            while ((square = Board.GetSquare(intOrdinal)) != null)
            {
                if (square == this)
                {
                    return true;
                }

                if (square.Piece != null)
                {
                    return false;
                }

                intOrdinal += directionOffset;
            }

            throw new ApplicationException("CanSlideToHereFrom: Hit edge of board!");
        }

        /// <summary>
        /// Calculates defense points for the player on this square. Returns the value of the cheapest piece defending the square.
        /// If no pieces are defending, then returns a high value (15,000).
        /// </summary>
        /// <param name="player">
        /// The defending player.
        /// </param>
        /// <returns>
        /// Defense points.
        /// </returns>
        public int DefencePointsForPlayer(Player player)
        {

            Piece piece;

            foreach (PieceModel.PieceNames pieceName in player.PieceTypes())
            {
                if (Piece.CanPlayerPieceNameAttackSquare(this, player, pieceName, out piece))
                    return piece.Value;
            }
            return 15000;
        }

        /// <summary>
        /// Gets the cheapest piece defending this square.
        /// </summary>
        /// <param name="player">
        /// Defending player who pieces should be listed.
        /// </param>
        /// <returns>
        /// List of pieces.
        /// </returns>
        public Piece CheapestPieceDefendingThisSquare(Player player)
        {
            Piece piece;

            foreach (PieceModel.PieceNames pieceName in player.PieceTypes())
            {
                if (Piece.CanPlayerPieceNameAttackSquare(this, player, pieceName, out piece))
                    return piece;
            }
            return null;
        }

        #endregion
    }
}
