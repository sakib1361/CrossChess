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
        bool init = false;
        bool isHorizontal = false;
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

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width == 0 || height == 0) return;

            var curHorizontal= width > height*1.2;
            if(curHorizontal != isHorizontal || !init)
            {
                init = true;
                isHorizontal = curHorizontal;
                if (isHorizontal)
                {
                    var ct = new ChessHorizontalView()
                    {
                        BindingContext = this.BindingContext
                    };
                    mContainer.Content = ct;
                }
            }
            
        }
    }
}