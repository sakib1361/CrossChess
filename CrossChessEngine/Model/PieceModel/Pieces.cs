using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CrossChessEngine.Model.PieceModel
{
    public class Pieces : IEnumerable
    {
        #region Constants and Fields

        /// <summary>
        /// Internal ArrayList of pieces.
        /// </summary>
        private readonly ArrayList pieces = new ArrayList();
        private static PieceSort sorter = new PieceSort();

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Count.
        /// </summary>
        public int Count
        {
            get
            {
                return this.pieces.Count;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="piece">
        /// The piece.
        /// </param>
        public void Add(Piece piece)
        {
            this.pieces.Add(piece);
        }

        /// <summary>
        /// Return a close of this list.
        /// </summary>
        /// <returns>
        /// The clone.
        /// </returns>
        public object Clone()
        {
            return this.pieces.Clone();
        }

        /// <summary>
        /// Get the enumerator for this list.
        /// </summary>
        /// <returns>
        /// The enumerator.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            return this.pieces.GetEnumerator();
        }

        /// <summary>
        /// Searches for the specified piece and returns its index.
        /// </summary>
        /// <param name="piece">
        /// The piece to search for.
        /// </param>
        /// <returns>
        /// Index value of the found piece. or null if not found.
        /// </returns>
        public int IndexOf(Piece piece)
        {
            return this.pieces.IndexOf(piece);
        }

        /// <summary>
        /// Insert a piece into the list. at the specified index position.
        /// </summary>
        /// <param name="ordinal">
        /// The ordinal index position where the piece will be inserted.
        /// </param>
        /// <param name="piece">
        /// The piece.
        /// </param>
        public void Insert(int ordinal, Piece piece)
        {
            this.pieces.Insert(ordinal, piece);
        }

        /// <summary>
        /// Returns the piece at the specified index position in the list.
        /// </summary>
        /// <param name="intIndex">
        /// Index position.
        /// </param>
        /// <returns>
        /// The piece at the specified index.
        /// </returns>
        public Piece Item(int intIndex)
        {
            return (Piece)this.pieces[intIndex];
        }

        /// <summary>
        /// Remove the piece from the list.
        /// </summary>
        /// <param name="piece">
        /// The piece to remove.
        /// </param>
        public void Remove(Piece piece)
        {
            this.pieces.Remove(piece);
        }

        /// <summary>
        /// The sort the pieces by their score value.
        /// </summary>
        public void SortByScore()
        {
            this.pieces.Sort(sorter);
        }

        #endregion
    }

    public class PieceSort : IComparer
    {
        public int Compare(object a, object b)
        {
            if (b == null)
                return 1;
            Piece x = (Piece)a;
            Piece y = (Piece)b;
            if (x.Value > y.Value)
                return 1;
            else if (x.Value < y.Value)
                return -1;
            else if (x.Value == y.Value)
            {
                if (y.Name == PieceNames.Knight) // bishops beat knights
                    return 1;
                else
                    return -1;
            }
            return 1;
        }
    }
}
