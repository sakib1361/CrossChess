using CrossChessEngine.Model.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossChessEngine.Engine.AI
{
    public static class HistoryHeuristic
    {
        #region Constants and Fields

        /// <summary>
        /// History table entries for black.
        /// </summary>
        private static readonly int[,] HistoryTableEntriesforBlack = new int[Board.SquareCount, Board.SquareCount];

        /// <summary>
        /// History table entries for white.
        /// </summary>
        private static readonly int[,] HistoryTableEntriesforWhite = new int[Board.SquareCount, Board.SquareCount];

        #endregion

        #region Public Methods

        /// <summary>
        /// Clear all history heuristic values.
        /// </summary>
        public static void Clear()
        {
            for (int i = 0; i < Board.SquareCount; i++)
            {
                for (int j = 0; j < Board.SquareCount; j++)
                {
                    HistoryTableEntriesforWhite[i, j] = 0;
                    HistoryTableEntriesforBlack[i, j] = 0;
                }
            }
        }

        /// <summary>
        /// Record a new history entry.
        /// </summary>
        /// <param name="colour">
        /// The player colour.
        /// </param>
        /// <param name="ordinalFrom">
        /// The From square ordinal.
        /// </param>
        /// <param name="ordinalTo">
        /// The To square ordinal.
        /// </param>
        /// <param name="value">
        /// The history heuristic weighting value.
        /// </param>
        public static void Record(Player.PlayerColourNames colour, int ordinalFrom, int ordinalTo, int value)
        {
            // Disable if this feature when switched off.
            if (!Game.EnableHistoryHeuristic)
            {
                return;
            }

            if (colour == Player.PlayerColourNames.White)
            {
                HistoryTableEntriesforWhite[ordinalFrom, ordinalTo] += value;
            }
            else
            {
                HistoryTableEntriesforBlack[ordinalFrom, ordinalTo] += value;
            }
        }

        /// <summary>
        /// Retrieve a value from the History Heuristic table.
        /// </summary>
        /// <param name="colour">
        /// The player colour.
        /// </param>
        /// <param name="ordinalFrom">
        /// The From square ordinal.
        /// </param>
        /// <param name="ordinalTo">
        /// The To square ordinal.
        /// </param>
        /// <returns>
        /// The history heuristic weighting value.
        /// </returns>
        public static int Retrieve(Player.PlayerColourNames colour, int ordinalFrom, int ordinalTo)
        {
            return colour == Player.PlayerColourNames.White ? HistoryTableEntriesforWhite[ordinalFrom, ordinalTo] : HistoryTableEntriesforBlack[ordinalFrom, ordinalTo];
        }

        #endregion
    }
}
