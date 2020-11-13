using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CrossChess.Helpers
{
    class DispatcherHelper
    {
        private static IDispatcher _dispatcher;

        internal static void Init(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        internal static void Run(Action action)
        {
            _dispatcher.BeginInvokeOnMainThread(action);
        }
    }
}
