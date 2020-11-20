using CrossChess.Helpers;
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
        public ObservableCollection<Move> Moves { get; }
        public ChessPlayer CurrentPlayer { get; private set; }
        public ChessPlayer OpponentPlayer { get; private set; }
        public ChessViewModel()
        {
            SquareDatas = new ObservableCollection<SquareData>();
            Moves = new ObservableCollection<Move>();
            Pieces = new List<Piece>();
        }

        internal void OnAppear()
        {
            Game.DifficultyLevel = 16;
            Game.MaximumSearchDepth = 0;
            Game.PlayerBlack.Intellegence = Player.PlayerIntellegenceNames.Human;
            Game.PlayerWhite.Intellegence = Player.PlayerIntellegenceNames.Computer;
            Game.New(this);
            CurrentPlayer = new ChessPlayer(Game.PlayerBlack, Pieces);
            OpponentPlayer = new ChessPlayer(Game.PlayerWhite, Pieces);
        }

        public void BoardPositionChanged()
        {
            if (SquareDatas.Count == 0)
            {
                Position = 0.5;
                CreateBoard();
            }
            else
            {
                Move LastMove = null;
                
                if (!Game.EditModeActive && Game.MoveHistory.Count > 0)
                {
                    LastMove = Game.MoveHistory[Game.MoveHistory.Count - 1];
                    Moves.Insert(0, LastMove);
                }
                foreach (var square in SquareDatas)
                {
                    square.UpdatePiece(LastMove);
                }
                CurrentPlayer.Update();
                OpponentPlayer.Update();
                var diff = CurrentPlayer.Points - OpponentPlayer.Points;
                var total = CurrentPlayer.Points + OpponentPlayer.Points;
                Position = Math.Abs(0.5 + (diff / (total / 2.0)));
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
            foreach (Move move in moves)
            {
                if (all.ContainsKey(move.To.Ordinal) == false)
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
