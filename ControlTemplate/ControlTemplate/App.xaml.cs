using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ControlTemplate
{
    public partial class App : Application
    {
        public App ()
        {
            InitializeComponent();
            
            MainPage = new MyPage1();
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("This is a log MyPage1");

            this.MainPage = new MyPage1();
        }

        void page2_Clicked(System.Object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("This is a log MyPage2");

            this.MainPage = new MyPage2();

        }

        void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("This is a log MyPage2");

            this.MainPage = new MyPage3();
        }

        void sombretheme_Clicked(System.Object sender, System.EventArgs e)
        {
            
        }

        void defaultname_Clicked(System.Object sender, System.EventArgs e)
        {
        }
    }
}

