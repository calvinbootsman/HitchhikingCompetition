using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HitchhikingCompetition.Classes
{
    class Markers
    {
        public string ID { get; set; }
        public string coupleName { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string Speed { get; set; }
        public string timeStamp { get; set; }
        public string Mood { get; set; }
        public string Message { get; set; }

        

        public async Task<List<Markers>> GetMarkers()
        {
            try
            {
                var client = new System.Net.Http.HttpClient();
                var uri = new Uri("http://trickingnederland.nl/lift/JsonPoints.php");
                var message = await client.GetAsync(uri);
                var received = await message.Content.ReadAsStringAsync();                
                var list = JsonConvert.DeserializeObject<List<Markers>>(received);
                return list;
                
            }
            catch (Exception e) { var list = new List<Markers>();
                return list;
            }
        }
    }
}
