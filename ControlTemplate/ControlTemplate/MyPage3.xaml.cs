using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ControlTemplate
{
    public partial class MyPage3 : ContentPage
    {
        public MyPage3()
        {
            InitializeComponent();

          
            label.BindingContext = slider;
          //  slider.Value = 90;
            label.SetBinding(Label.FontSizeProperty, "Value");
            label.SetBinding(Label.BackgroundColorProperty, "BackgroundColor");
        }

        void FermerModal_Clicked(System.Object sender, System.EventArgs e)
        {
            this.Navigation.PopModalAsync();
        }
    }
}

