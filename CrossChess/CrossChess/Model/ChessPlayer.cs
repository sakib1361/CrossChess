using CrossChessEngine.Model.Data;
using CrossChessEngine.Model.PieceModel;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CrossChess.Model
{
    [AddINotifyPropertyChangedInterface]
    public class ChessPlayer
    {
        public string Name { get; }
        public Piece KingPiece { get; }
        public int PiecePoint { get; private set; }

        private Player _player;
        private List<Piece> _pieces;
        public List<Piece> CapturedPiece { get; private set; }
        public int Position { get; private set; }
        public int Points { get; private set; }

        public ChessPlayer(Player player, List<Piece> pieces)
        {
            Name = player.Colour.ToString();
            KingPiece = player.King;
            _player = player;
            _pieces = pieces;
        }


        public void Update()
        {
            if (PiecePoint != _player.CapturedEnemyPiecesTotalBasicValue)
            {
                CapturedPiece = _pieces.Where(x => x.Player == _player &&
                                                   x.IsInPlay == false)
                                       .ToList();
                PiecePoint = _player.CapturedEnemyPiecesTotalBasicValue;
            }
            Position = _player.PositionPoints;
            Points = _player.Points;
        }
    }
}
