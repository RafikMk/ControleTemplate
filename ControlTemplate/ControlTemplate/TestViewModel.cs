using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;
using System.ServiceModel.Syndication;
using System.Xml;
using System.ServiceModel;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace ControlTemplate
{
    public class TestViewModel :BaseViewModel
    {
        Contrie selectedContrie;
        coffe selectedCofee;
        Post selectedPost;
        coffe previouslyselected;
        public coffe SelectedCofee 
        {
            get => selectedCofee ;
            set
            {
                if (value != null )
                {
                    Application.Current.MainPage.DisplayAlert("selected", value.Name, "ok");
                    previouslyselected = value;
                    value = null;
                }
                selectedCofee = value; 
                OnPropertyChanged();
            }
    }

        public Post SelectedPosts
        {
            get => selectedPost;
            set
            {
                if (value != null)
                {
                    Application.Current.MainPage.DisplayAlert("selected", value.title, "ok");

                    value = null;
                }
                selectedPost = value;
                OnPropertyChanged();
            }
        }
        public Contrie SelectedContrie
        {
            get => selectedContrie;
            set
            {
                if (value != null)
                {
                    Application.Current.MainPage.DisplayAlert("selected", value.countryName, "ok");
                   PostCountryCurrency(value.countryCode) ;
                    
                    value = null;
                }
                selectedContrie = value;
                OnPropertyChanged();
            }
        }

        
        public coffe SelectedCofee2
        {
            get => selectedCofee;
            set => SetProperty(ref selectedCofee, value);

        }

        public async void PostCountryCurrency(string sCountryISOCode)
        {
            var httpClient = new HttpClient();
            var url = "http://webservices.oorsprong.org/websamples.countryinfo/CountryInfoService.wso";
            var xmlBody = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                    <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                        <soap:Body>
                            <CountryCurrency xmlns=""http://www.oorsprong.org/websamples.countryinfo"">
                                <sCountryISOCode>{sCountryISOCode}</sCountryISOCode>
                            </CountryCurrency>
                        </soap:Body>
                    </soap:Envelope>";
            var content = new StringContent(xmlBody, Encoding.UTF8, "text/xml");  

            var response = await httpClient.PostAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var xmlContent = XElement.Parse(responseContent);
            // System.Diagnostics.Debug.WriteLine(xmlContent);
            /*   ^^^^^^Parse to json 
             XmlDocument doc = new XmlDocument();
             doc.LoadXml(xmlContent.ToString());
             var json = JsonConvert.SerializeXmlNode(doc);
             System.Diagnostics.Debug.WriteLine(json);*/
            var namespaceStr = "{http://www.oorsprong.org/websamples.countryinfo}";
            var sName = xmlContent.Descendants(namespaceStr + "sName").FirstOrDefault()?.Value;
            var sISOCode = xmlContent.Descendants(namespaceStr + "sISOCode").FirstOrDefault()?.Value;

            if (!string.IsNullOrEmpty(sName))
            {
                await Application.Current.MainPage.DisplayAlert("Currency Name"+sName+"", "sISOCode "+ sISOCode, "OK");
            }

        }

        async Task Favorite(coffe coffe)
        {
            if (coffe == null)
                return;
            await Application.Current.MainPage.DisplayAlert("Favorite", coffe.Name, "ok");


        }
        async Task Selected(object args)
        {
            var cofee = args as coffe;
            if (cofee == null)
                return;
            selectedCofee = null;
            await Application.Current.MainPage.DisplayAlert("Selected ", cofee.Name, "ok");


        }

     
        public TestViewModel()
        {
          //  client = new HttpClient { BaseAddress = new Uri(baseurl)};

            // GetContinents().Wait();
            increaseButton = new Command(buttonevent);
            Title = "ddd";
            cofee = new ObservableRangeCollection<coffe>();
            Contries = new ObservableRangeCollection<Contrie>();
            coffeeGroups = new ObservableRangeCollection<Grouping<string, coffe>>();
    
       // Contries.Add(new Contrie { continent = "bbb", countryCode = "nnnnn",countryName ="ddd "});
            var image = "";
            cofee.Add(new coffe { Image = image, Name = "rt" ,Roaster ="vv"});
            cofee.Add(new coffe { Image = image, Name = "rr", Roaster = "bb" });
            cofee.Add(new coffe { Image = image, Name = "nn", Roaster = "nn" });
            coffeeGroups.Add(new Grouping<string, coffe>("Roaster B",cofee.Take(3)));
            Refreshcommand = new AsyncCommand(Refresh);
            FavouriteCommand = new AsyncCommand<coffe>(Favorite);
            SelectedCommand = new AsyncCommand<coffe>(Selected);

            // *********************** consomation of soap  ************************
            Posts = new ObservableRangeCollection<Post>();
            AddPost = new Command(AddPostButton);


            Coffes = new ObservableRangeCollection<coffe>();
            Refreshcommand2 = new AsyncCommand(Refresh);
            //      AddCommand = new AsyncCommand(Add);
            //   RemoveCommand = new AsyncCommand<coffe>(Remove);

            //$$$$$$$$$$$$$$$
            try
            {
                GetData();

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);

            }

        }
         
        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(200);
            Coffes.Clear();
         //   var coffes = CoffeeServic.Get
            IsBusy = false;


        }
        public ObservableRangeCollection<coffe> cofee { get; }
        public ObservableRangeCollection<Contrie> Contries { get; }

        public ObservableRangeCollection<Grouping<string,coffe>> coffeeGroups { get; }
        public AsyncCommand Refreshcommand { get; }
        public AsyncCommand<coffe> FavouriteCommand { get; }
        public AsyncCommand<coffe> SelectedCommand { get; }
        public ObservableRangeCollection<Post> Posts { get; }
        public ICommand AddPost { get; }

        


        // *********************** consomation of soap  ************************
        public ObservableRangeCollection<coffe> Coffes { get; }
        public AsyncCommand Refreshcommand2 { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand RemoveCommand { get; }



        private bool CanExecuteIncrease(object parameter)
        {
            if (count < 5) { return false; };
            return true;

        }
        int count;
        string countdisplay = "Click me";
        public string Countdisplay
        {
            get => countdisplay;
            set
            {
                if (value == countdisplay)
                    return;
                countdisplay = value;
                OnPropertyChanged(nameof(Countdisplay));

            }
        }

     
        public ICommand increaseButton { get; }




        public async void AddPostButton()
        {

            string Title = await Application.Current.MainPage.DisplayPromptAsync("Title 1", "What's your Title?");
            string Author = await Application.Current.MainPage.DisplayPromptAsync("Question 1", "What's your Author?");

            var client = new HttpClient();

            var Post = new Post
            {
                author = Author,
                title = Title
            };
            var json = JsonConvert.SerializeObject(Post);
            System.Diagnostics.Debug.WriteLine("eeeee"+ json);
               
            try
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response1 = await client.PostAsync("http://192.168.137.51:3000/posts", content);
                if (response1.IsSuccessStatusCode)
                {
                    var response = await client.GetAsync("http://192.168.137.51:3000/posts");
                    if (response1.IsSuccessStatusCode)
                    {
                        var content1 = await response.Content.ReadAsStringAsync();
                        var posts = JsonConvert.DeserializeObject<List<Post>>(content1);
                        Posts.AddRange(posts);

                        System.Diagnostics.Debug.WriteLine(posts[0].title);
                    }
                    else
                    { 
                        System.Diagnostics.Debug.WriteLine("eeeee");

                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("eeeee");

                }

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("eeeee" + e);

            }

            
        }
        private void buttonevent(object parameter)
        {
            count++;
            System.Diagnostics.Debug.WriteLine(count);

            Countdisplay = $"you clickerd  {count}";
        }
          
        private async void GetData()
        {
            var client = new HttpClient();
            try 
            {
                var response = await client.GetAsync("http://webservices.oorsprong.org/websamples.countryinfo/CountryInfoService.wso/ListOfCountryNamesGroupedByContinent");
             //  System.Diagnostics.Debug.WriteLine(response);
                var content = await response.Content.ReadAsStringAsync();
             //   System.Diagnostics.Debug.WriteLine(content);
                var xmlContent = XElement.Parse(content);
                /*   System.Diagnostics.Debug.WriteLine(xmlContent);
                       XmlDocument doc = new XmlDocument();
                       doc.LoadXml(xmlContent.ToString());
                       var json = JsonConvert.SerializeXmlNode(doc);
                       System.Diagnostics.Debug.WriteLine(json);*/
                var countryGroups = xmlContent.Descendants("tCountryCodeAndNameGroupedByContinent");

                JArray countriesArray = new JArray();

                foreach (var countryGroup in countryGroups)
                {
                    var continent = countryGroup.Element("Continent").Element("sName").Value;

                    var countries = countryGroup.Element("CountryCodeAndNames").Descendants("tCountryCodeAndName");

                    foreach (var country in countries)
                    {
                        var countryCode = country.Element("sISOCode").Value;
                        var countryName = country.Element("sName").Value;
                        var contr = new Contrie { continent = continent ,countryName =countryName ,countryCode = countryCode };
                        JObject countryObject = new JObject();
                        countryObject.Add("continent", continent);
                        countryObject.Add("countryCode", countryCode);
                        countryObject.Add("countryName", countryName);
                        Contries.Add(contr);
                       
                       // System.Diagnostics.Debug.WriteLine("contr" + contr);

                        //   countriesArray.Add(countryObject);
                    }
                }

              //  string json = countriesArray.ToString();

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);

            }

            var response1 = await client.GetAsync("http://192.168.137.51:3000/posts");
            if (response1.IsSuccessStatusCode)
            {
                var content1 = await response1.Content.ReadAsStringAsync();
                var posts = JsonConvert.DeserializeObject<List<Post>>(content1);
                try
                {
                
                    Posts.AddRange(posts);
                    System.Diagnostics.Debug.WriteLine(posts);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);


                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("eeeee");

            }
        }
    }
}

