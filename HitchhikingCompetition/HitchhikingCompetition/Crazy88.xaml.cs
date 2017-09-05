using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System.Diagnostics;
using PCLStorage;

namespace HitchhikingCompetition
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Crazy88 : ContentPage
    {
        public Crazy88()
        {
            InitializeComponent();
            TestVoid();
            Crazy88List.ItemsSource = Data.crazy88list;

        }
        async void TestVoid()
        {
            IFile file = await FileHandling.getFile("Crazy88Data", "AssignmentList.txt");            
        }
        async void updaten(){
            string text = "";
            foreach (Crazy88Assignments dinges in Data.crazy88list)
            {
                Debug.WriteLine("Item: "+dinges.Item + " ID: " + dinges.IsChecked);
                text += dinges.Item + "*" + dinges.Description + "*"+dinges.Points+"*" + dinges.IsChecked + ";";
            }
            IFile file = await FileHandling.getFile("Crazy88Data", "AssignmentList.txt");
            await file.WriteAllTextAsync(text);
        }
        
    }
}