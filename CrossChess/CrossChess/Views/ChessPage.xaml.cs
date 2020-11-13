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

                if (isHorizontal) SetHorizontalUI();
                else SetVerticalUI();
                //ChessGrid.HeightRequest = ChessGrid.WidthRequest = Math.Min(ChessGrid.Height, ChessGrid.Width);
            }
            
        }

        private void SetVerticalUI()
        {
            MGrid.ColumnDefinitions.Clear();
            ChessGrid.Margin = new Thickness(0);
            MRogress.Margin = new Thickness(5);
            MGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
            MGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });

            Grid.SetRowSpan(MRogress, 1);
            Grid.SetRow(MRogress, 0);
            Grid.SetColumnSpan(MRogress, 2);

            Grid.SetRowSpan(ChessGrid, 1);
            Grid.SetRow(ChessGrid, 1);
            Grid.SetColumn(ChessGrid, 0);
            Grid.SetColumnSpan(ChessGrid, 2);
            

            Grid.SetRow(OpponentBar, 2);
            Grid.SetColumn(OpponentBar, 1);

            Grid.SetRow(PlayerBar, 2);
            Grid.SetColumn(PlayerBar, 0);

            Grid.SetColumnSpan(MenuBar, 2);
            Grid.SetRow(MenuBar, 3);
            Grid.SetColumn(MenuBar, 0);

            MRogress.Horizontal = false;
        }

        private void SetHorizontalUI()
        {
            MGrid.ColumnDefinitions.Clear();
            //MGrid.RowDefinitions.Clear();

            ChessGrid.Margin = new Thickness(20, 0);
            MRogress.Margin = new Thickness(0);
            MGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 30 });
            MGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            MGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });

            Grid.SetRowSpan(MRogress, 4);
            Grid.SetColumnSpan(MRogress, 1);
            Grid.SetRow(MRogress, 0);

            Grid.SetRowSpan(ChessGrid, 4);
            Grid.SetColumnSpan(ChessGrid, 1);
            Grid.SetRow(ChessGrid, 0);
            Grid.SetColumn(ChessGrid, 1);


            Grid.SetRow(PlayerBar, 3);
            Grid.SetColumn(PlayerBar, 2);

            Grid.SetColumn(OpponentBar, 2);
            Grid.SetRow(OpponentBar, 0);

            Grid.SetRow(MenuBar, 2);
            Grid.SetColumn(MenuBar, 2);
            Grid.SetColumnSpan(MenuBar, 1);

            MRogress.Horizontal = true;
           
        }
    }
}