using System;
using System.Collections.Generic;
using System.Text;

namespace CrossChessEngine.Engine.App
{
    public interface IBoardListener
    {
        void BoardPositionChanged();
        void StartedThinking();
        void MoveReady();
    }
}
