using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLStorage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using Plugin.Geolocator;
using System.Net.Http;

namespace HitchhikingCompetition
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Location : ContentPage
    {
        public Location()
        {
            InitializeComponent();

        }

        public void RefreshPage (){
                LocationWebsite.Source = (LocationWebsite.Source as UrlWebViewSource).Url;
        }

       
    }
}