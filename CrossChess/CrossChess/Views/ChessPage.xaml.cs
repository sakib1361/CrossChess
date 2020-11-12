using CrossChess.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossChess.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChessPage : ContentPage
    {
        private ChessViewModel _vm;

        public ChessPage()
        {
            InitializeComponent();
            _vm = new ChessViewModel();
            this.BindingContext = _vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _vm.OnAppear();

        }
    }
}