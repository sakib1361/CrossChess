﻿using CrossChess.Helpers;
using CrossChess.Model;
using CrossChessEngine.Engine.App;
using CrossChessEngine.Model.Data;
using CrossChessEngine.Model.PieceModel;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CrossChess.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ChessViewModel : BaseViewModel, IBoardListener
    {
        private SquareData currentSquare;
        private Moves allMoves;

        private List<Piece> Pieces;
        public double Position { get; set; }
        public bool IsWhite { get; private set; }
        public ObservableCollection<SquareData> SquareDatas { get; }
        public IEnumerable<Piece> OpponentPieces { get; private set; }
        public IEnumerable<Piece> PlayerPieces { get; private set; }
        public ObservableCollection<Move> Moves { get; }
        public int OpponentPiecePoint { get; private set; }
        public int CurrentPiecePoint { get; private set; }
        public ChessViewModel()
        {
            SquareDatas = new ObservableCollection<SquareData>();
            Moves = new ObservableCollection<Move>();
            Pieces = new List<Piece>();
        }

        internal void OnAppear()
        {
            Game.DifficultyLevel = 3;
            Game.PlayerBlack.Intellegence = Player.PlayerIntellegenceNames.Computer;
            Opponent = Game.PlayerBlack;
            CurrentPlayer = Game.PlayerWhite;
            Game.New(this);
        }

        public void BoardPositionChanged()
        {
            if (SquareDatas.Count == 0)
            {
                Position = 0.5;
                CreateBoard();
                PlayerPieces = null;
                OpponentPieces = null;
            }
            else
            {
                Move LastMove = null;
                if (!Game.EditModeActive && Game.MoveHistory.Count > 0)
                {
                    LastMove = Game.MoveHistory[Game.MoveHistory.Count - 1];
                    Moves.Insert(0,LastMove);
                }
                foreach (var square in SquareDatas)
                {
                    square.UpdatePiece(LastMove);
                }

                var curPos = Math.Max(0, CurrentPlayer.PositionPoints);
                var oppPos = Math.Max(0, Opponent.PositionPoints);

                //Position = curPos / ((curPos + oppPos) * 1.0);
                PlayerPieces = Pieces.Where(x => x.Player == CurrentPlayer &&
                                                 x.IsInPlay == false);
                OpponentPieces = Pieces.Where(x => x.Player == Opponent &&
                                                 x.IsInPlay == false);
                if (Game.PlayerToPlay.IsInCheckMate)
                {

                }
            }
        }

        private void CreateBoard()
        {
            for (int intOrdinal = 0; intOrdinal < Board.SquareCount; intOrdinal++)
            {
                var square = Board.GetSquare(intOrdinal);

                if (square != null)
                {
                    SquareDatas.Add(new SquareData(square));

                    if (square.Piece != null)
                    {
                        Pieces.Add(square.Piece);
                    }
                }
            }
        }

        public ICommand PieceCommand => new RelayCommand<SquareData>(PieceAction);
        public ICommand UndoCommand => new RelayCommand(UndoAction);

        public Player Opponent { get; private set; }
        public Player CurrentPlayer { get; private set; }

        private void UndoAction()
        {
            Game.UndoMove();
        }

        private void PieceAction(SquareData obj)
        {
            if (currentSquare == null && obj.Piece != null && obj.Piece.Player == Game.PlayerToPlay)
            {
                currentSquare = obj;
                Game.SuspendPondering();
                allMoves = new Moves();
                obj.Piece.GenerateLegalMoves(allMoves);
                GenerateMoves(allMoves);
                Game.ResumePondering();
            }
            else
            {
                if (currentSquare!=null &&
                    obj.AvailableMove &&
                    obj.PieceMove!=null)
                {
                    Game.SuspendPondering();

                    Game.MakeAMove(obj.PieceMove.Name, currentSquare.Piece, obj.Square);
                }
                allMoves?.Clear();
          
                foreach (var square in SquareDatas)
                {
                    square.AvailableMove = false;
                }
                currentSquare = null;
            }
        }

        private void GenerateMoves(Moves moves)
        {
            var all = new Dictionary<int, Move>();
            foreach(Move move in moves)
            {
                all.Add(move.To.Ordinal, move);
            }
            foreach(var square in SquareDatas)
            {
                if (all.TryGetValue(square.ID, out Move move))
                {
                    square.AvailableMove = true;
                    square.PieceMove = move;
                }
                else
                {
                    square.AvailableMove = false;
                    square.PieceMove = null;
                }
            }
        }

        public void StartedThinking()
        {
            
        }

        public void MoveReady()
        {
            
        }
    }
}
