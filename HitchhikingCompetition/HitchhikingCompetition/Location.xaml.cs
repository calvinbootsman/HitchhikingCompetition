using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HitchhikingCompetition
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Location : ContentPage
    {
       
        public Location()
        {
            InitializeComponent();
            var UpdateLocation = new Button()
            {
                Text = "Get Location"
            };
            UpdateLocation.Clicked += UpdateLocation_Clicked;
            MainStack.Children.Add(UpdateLocation);
            var test = new object();
            var test1 = new EventArgs();
            RefreshPage(test, test1);
        }

        private void UpdateLocation_Clicked(object sender, EventArgs e)
        {
            var tabbedcontent = new TabbedContent();
            tabbedcontent.UpdateLocation();
            LocationWebsite.Source = (LocationWebsite.Source as UrlWebViewSource).Url;
        }

        public void RefreshPage (object sender, EventArgs e)
        {
            LocationWebsite.Source = "http://trickingnederland.nl/lift/maps.php"; //(LocationWebsite.Source as UrlWebViewSource).Url;
        }

       
    }
}