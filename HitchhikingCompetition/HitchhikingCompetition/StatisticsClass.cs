using System;
using PCLStorage;

namespace HitchhikingCompetition
{
    public class Carride
    {
        //public int
        public string Gender { get; set; }
        public int age { get; set; }
        public string CarBrand { get; set; }
        public int AmountOfKm { get; set; }
        public string WayOfGettingTheRide { get; set; }
        public string CountryOfOrigin { get; set; }
        public DateTime TimeInbetweenCars { get; set; }
    }

    public class Adventure
    {
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int AmountOfKm { get; set; }
    }

    public class Adventures
    {
        
        public async void AddAdventureToText(Adventure adventure){
            var file = await FileHandling.getFile("Adventure","Adventures");
            var text = await file.ReadAllTextAsync();
            text = text +          
                                   adventure.AmountOfKm.ToString() + "*" +
                                   adventure.EndLocation + "*" +
                                   adventure.EndTime.ToString() + "*" +
                                   adventure.StartLocation.ToString() + "*" +
                                   adventure.StartTime + ";" ;
        }

        public void AddCarrideToText(Carride carride){
            
        }
    }


}
