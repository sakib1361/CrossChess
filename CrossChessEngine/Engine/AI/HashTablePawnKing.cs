﻿using CrossChessEngine.Model.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossChessEngine.Engine.AI
{
    public static class HashTablePawn
    {
        #region Constants and Fields

        /// <summary>
        ///   Indicates that a position was not found in the Hash Table.
        /// </summary>
        public const int NotFoundInHashTable = int.MinValue;

        /// <summary>
        ///   Pointer to the HashTable
        /// </summary>
        private static HashEntry[] hashTableEntries;

        /// <summary>
        ///   Size of the HashTable.
        /// </summary>
        private static uint hashTableSize;

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets the number of hash table Collisions that have occured.
        /// </summary>
        public static int Collisions { get; private set; }

        /// <summary>
        ///   Gets the number of hash table Hits that have occured.
        /// </summary>
        public static int Hits { get; private set; }

        /// <summary>
        ///   Gets the number of hash table Overwrites that have occured.
        /// </summary>
        public static int Overwrites { get; private set; }

        /// <summary>
        ///   Gets the number of hash table Probes that have occured.
        /// </summary>
        public static int Probes { get; private set; }

        /// <summary>
        ///   Gets the number of hash table slots used.
        /// </summary>
        public static int SlotsUsed
        {
            get
            {
                int intCounter = 0;

                for (uint intIndex = 0; intIndex < hashTableSize; intIndex++)
                {
                    if (hashTableEntries[intIndex].HashCodeA != 0)
                    {
                        intCounter++;
                    }
                }

                return intCounter;
            }
        }

        /// <summary>
        ///   Gets the number of hash table Writes that have occured.
        /// </summary>
        public static int Writes { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Clears all entries in the hash table.
        /// </summary>
        public static void Clear()
        {
            ResetStats();
            for (uint intIndex = 0; intIndex < hashTableSize; intIndex++)
            {
                hashTableEntries[intIndex].HashCodeA = 0;
                hashTableEntries[intIndex].HashCodeB = 0;
                hashTableEntries[intIndex].Points = NotFoundInHashTable;
            }
        }

        /// <summary>
        /// Initialises the HashTable.
        /// </summary>
        public static void Initialise()
        {
            hashTableSize = Game.AvailableMegaBytes * 3000;
            hashTableEntries = new HashEntry[hashTableSize];
            Clear();
        }

        /// <summary>
        /// Search pawn and king hash table for a pawn and king specific score for the specific position hash.
        /// </summary>
        /// <param name="hashCodeA">
        /// Hash Code for Board position A
        /// </param>
        /// <param name="hashCodeB">
        /// Hash Code for Board position B
        /// </param>
        /// <param name="colour">
        /// The player colour.
        /// </param>
        /// <returns>
        /// Pawn and king specific score for the specified position.
        /// </returns>
        public static unsafe int ProbeHash(ulong hashCodeA, ulong hashCodeB, Player.PlayerColourNames colour)
        {
            if (colour == Player.PlayerColourNames.Black)
            {
                hashCodeA |= 0x1;
                hashCodeB |= 0x1;
            }
            else
            {
                hashCodeA &= 0xFFFFFFFFFFFFFFFE;
                hashCodeB &= 0xFFFFFFFFFFFFFFFE;
            }

            Probes++;

            fixed (HashEntry* phashBase = &hashTableEntries[0])
            {
                HashEntry* phashEntry = phashBase;
                phashEntry += (uint)(hashCodeA % hashTableSize);

                if (phashEntry->HashCodeA == hashCodeA && phashEntry->HashCodeB == hashCodeB)
                {
                    Hits++;
                    return phashEntry->Points;
                }
            }

            return NotFoundInHashTable;
        }

        /// <summary>
        /// Record the pawn and kind specific positional score in the pawn hash table.
        /// </summary>
        /// <param name="hashCodeA">
        /// Hash Code for Board position A
        /// </param>
        /// <param name="hashCodeB">
        /// Hash Code for Board position B
        /// </param>
        /// <param name="val">
        /// Pawn specific score.
        /// </param>
        /// <param name="colour">
        /// Player colour.
        /// </param>
        public static unsafe void RecordHash(ulong hashCodeA, ulong hashCodeB, int val, Player.PlayerColourNames colour)
        {
            if (colour == Player.PlayerColourNames.Black)
            {
                hashCodeA |= 0x1;
                hashCodeB |= 0x1;
            }
            else
            {
                hashCodeA &= 0xFFFFFFFFFFFFFFFE;
                hashCodeB &= 0xFFFFFFFFFFFFFFFE;
            }

            fixed (HashEntry* phashBase = &hashTableEntries[0])
            {
                HashEntry* phashEntry = phashBase;
                phashEntry += (uint)(hashCodeA % hashTableSize);
                phashEntry->HashCodeA = hashCodeA;
                phashEntry->HashCodeB = hashCodeB;
                phashEntry->Points = val;
            }

            Writes++;
        }

        /// <summary>
        /// Reset all hash table stats.
        /// </summary>
        public static void ResetStats()
        {
            Probes = 0;
            Hits = 0;
            Writes = 0;
            Collisions = 0;
            Overwrites = 0;
        }

        #endregion

        /// <summary>
        /// Hash Table entry data structure.
        /// </summary>
        private struct HashEntry
        {
            #region Constants and Fields

            /// <summary>
            ///   Pawn Position Hash code A.
            /// </summary>
            public ulong HashCodeA;

            /// <summary>
            ///   Pawn Position Hash code A.
            /// </summary>
            public ulong HashCodeB;

            /// <summary>
            ///   Pawn positional score.
            /// </summary>
            public int Points;

            #endregion
        }
    }
}
