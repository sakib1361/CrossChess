
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossChess.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashPage : ContentPage
    {
        public SplashPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Application.Current.MainPage = new ChessPage();
        }
    }
}