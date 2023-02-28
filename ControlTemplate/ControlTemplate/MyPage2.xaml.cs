
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ControlTemplate
{
    public partial class MyPage2 : ContentPage
    {
        public MyPage2()
        {
            InitializeComponent();

            openmodal.Clicked += Openmodal_Clicked;
           BindingContext = new TestViewModel() ;
        

        }

     

        private void Openmodal_Clicked(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("This is a log MyPage2");
            this.Navigation.PushModalAsync(new MyPage3());
        }
        int count;
        string countdisplay = "Click me";
        public string Countdisplay2
        {
            get => countdisplay;
            set
            {
                if (value == countdisplay)
                    return;
                countdisplay = value;
                OnPropertyChanged(nameof(Countdisplay2));

            }
        }
        private void cloick(object sender, EventArgs e)
        {

            System.Diagnostics.Debug.WriteLine(countdisplay);
            System.Diagnostics.Debug.WriteLine("******* Countdisplay *******");

            System.Diagnostics.Debug.WriteLine(Countdisplay2);

            count++;
            System.Diagnostics.Debug.WriteLine(count);

            Countdisplay2 = $"you clickerd  {count}";
        }

        async void ListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var cofee = ((ListView)sender).SelectedItem as coffe;
            if (cofee == null)
                return;
            await DisplayAlert("cofee",cofee.Name,"ok");
        }

        void ListView_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(((ListView)sender).SelectedItem as coffe);

            ((ListView)sender).SelectedItem = null;
        }

        async void MenuItem_Clicked(System.Object sender, System.EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var coffe = menuItem.BindingContext as coffe;
            if (coffe == null)
                return;
            await DisplayAlert("Favorite cofee", coffe.Name, "ok");
        }

        void addpost_Clicked(System.Object sender, System.EventArgs e)
        {
        }

         void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var random = new Random();
            var random1 = random.Next(0, 256);
            var random2 = random.Next(0, 256);
            var random3 = random.Next(0, 256);
            // await DisplayAlert("Favorite cofee", "ee", "ok");
            Resources["LayoutBackgroundColor"] = Color.FromRgb(random.Next(256), random.Next(256), random.Next(256));

            //   App.Current.Resources["LayoutBackground"] = Color.FromHex("#FFFFFF");

        }
    }
}

