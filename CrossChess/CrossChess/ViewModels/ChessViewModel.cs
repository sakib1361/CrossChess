using CrossChess.Helpers;
using CrossChess.Model;
using CrossChessEngine.Engine.App;
using CrossChessEngine.Model.Data;
using CrossChessEngine.Model.PieceModel;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace CrossChess.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ChessViewModel : BaseViewModel, IBoardListener
    {
        private SquareData currentSquare;
        private Moves allMoves;

        public double Position { get; set; }
        public bool IsWhite { get; private set; }
        public ObservableCollection<SquareData> SquareDatas { get; }
        public ObservableCollection<Piece> OpponentPieces { get; }
        public ObservableCollection<Piece> PlayerPieces { get; }
        public ObservableCollection<Move> Moves { get; }
        public int OpponentPiecePoint { get; private set; }
        public int CurrentPiecePoint { get; private set; }
        public ChessViewModel()
        {
            SquareDatas = new ObservableCollection<SquareData>();
            OpponentPieces = new ObservableCollection<Piece>();
            PlayerPieces = new ObservableCollection<Piece>();
            Moves = new ObservableCollection<Move>();
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
                OpponentPieces.Clear();
                PlayerPieces.Clear();
            }
            else
            {
                Move LastMove = null;
                if (!Game.EditModeActive && Game.MoveHistory.Count > 0)
                {
                    LastMove = Game.MoveHistory[Game.MoveHistory.Count - 1];
                    Moves.Add(LastMove);
                }
                foreach (var square in SquareDatas)
                {
                    square.UpdatePiece(LastMove);
                }

                var curPos = Math.Max(0, CurrentPlayer.PositionPoints);
                var oppPos = Math.Max(0, Opponent.PositionPoints);
                Position = curPos / ((curPos + oppPos) * 1.0);
                if(CurrentPiecePoint != CurrentPlayer.CapturedEnemyPiecesTotalBasicValue)
                {
                    CurrentPiecePoint = CurrentPlayer.CapturedEnemyPiecesTotalBasicValue;
                    foreach(Piece item in CurrentPlayer.CapturedEnemyPieces)
                    {
                        if (PlayerPieces.Contains(item) == false)
                            PlayerPieces.Add(item);
                    }
                }
                if (OpponentPiecePoint != Opponent.CapturedEnemyPiecesTotalBasicValue)
                {
                    OpponentPiecePoint = Opponent.CapturedEnemyPiecesTotalBasicValue;
                    foreach (Piece item in Opponent.CapturedEnemyPieces)
                    {
                        if (OpponentPieces.Contains(item) == false)
                            OpponentPieces.Add(item);
                    }
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
                Game.ResumePondering();
                GenerateMoves(allMoves);
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
                allMoves.Clear();
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
