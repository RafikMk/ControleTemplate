using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ControlTemplate
{
    public partial class MyPage1 : ContentPage
    {
        public MyPage1()
        { 
            InitializeComponent();
        }

        void sombretheme_Clicked(System.Object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("This is a log sombreTemplate");

            this.ControlTemplate = (Xamarin.Forms.ControlTemplate)App.Current.Resources["sombretheme"];
            /* Get the 'sombretheme' control template from the application's resources
            ControlTemplate sombreTemplate = (ControlTemplate)App.Current.Resources["sombretheme"];
            
            // Set the control template to the 'sombretheme' 
            App.Current.ControlTemplate = sombreTemplate;*/
        }

        void defaultname_Clicked(System.Object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("This is a log CustomNavBar");

            this.ControlTemplate = (Xamarin.Forms.ControlTemplate)App.Current.Resources["CustomNavBar"];

        }

    }
}

