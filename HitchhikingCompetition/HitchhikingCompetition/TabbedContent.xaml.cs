using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using PCLStorage;
namespace HitchhikingCompetition
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedContent : TabbedPage
    {
       public TabbedContent()
        {
            InitializeComponent();
            Children.Add(new Location());
            Children.Add(new Crazy88());
            Children.Add(new Settings());
            try
            {
                Device.StartTimer(TimeSpan.FromMinutes(2), () =>
                {

                    // call your method to check for notifications here
                    UpdateLocation();
                    // Returning true means you want to repeat this timer
                    

                    return true;
                });
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
                Debug.WriteLine("Yolo");
            }
        }
       void UpdateLocation()
        {
            Debug.WriteLine("yo");
            Settings setting = new Settings();
            setting.GetLocation();
        }
    }
}