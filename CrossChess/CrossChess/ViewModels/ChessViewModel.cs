using CrossChess.Model;
using CrossChessEngine.Engine.App;
using CrossChessEngine.Model.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CrossChess.ViewModels
{
    public class ChessViewModel : BaseViewModel, IBoardListener
    {
        public ObservableCollection<SquareData> SquareDatas { get; }
        public ChessViewModel()
        {
            SquareDatas = new ObservableCollection<SquareData>();
        }

        internal void OnAppear()
        {
            Game.New(this);
            for (int intOrdinal = 0; intOrdinal < Board.SquareCount; intOrdinal++)
            {
               var square = Board.GetSquare(intOrdinal);

                if (square != null)
                {
                    SquareDatas.Add(new SquareData(square));
                }
            }
        }

        public void BoardPositionChanged()
        {
            
        }
    }
}
