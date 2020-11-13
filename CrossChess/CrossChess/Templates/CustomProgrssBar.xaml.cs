using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossChess.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomProgrssBar : ContentView
    {
        public CustomProgrssBar()
        {
            InitializeComponent();
        }

        public Color ProgrssColor
        {
            get => BottomView.Color;
            set => BottomView.Color = value;
        }

        public bool Horizontal { get; set; } = false;
        public double Progress
        {
            get => (double)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }
        public static BindableProperty ProgressProperty =
            BindableProperty.Create(
                nameof(Progress),
                typeof(double),
                typeof(CustomProgrssBar),
                0.5,
                BindingMode.TwoWay,
                propertyChanged: PropertyDataChanged);

        private static void PropertyDataChanged(BindableObject bindable, object oldValue, object newValue)
        {
           if(bindable is CustomProgrssBar custom && newValue is double data)
            {
                custom.Reset(data);
            }
        }

        internal async void Reset(double data)
        {
            if (data == 0) return;
            if (this.Height <= 0) await Task.Delay(250);
            if (Horizontal)
            {
                BottomView.WidthRequest = this.Width * data;
                BottomView.HeightRequest = this.Height;
            }
            else
            {
                BottomView.HeightRequest = this.Height * data;
                BottomView.WidthRequest = this.Width;
            }
        }
    }
}