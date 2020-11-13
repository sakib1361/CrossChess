using CrossChess.Helpers;
using CrossChess.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossChess
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DispatcherHelper.Init(Current.Dispatcher);
            MainPage = new SplashPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
