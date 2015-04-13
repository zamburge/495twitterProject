using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterFind;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System.Data;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Documents;
using System.Windows.Shapes;
using System.Windows.Media;



namespace TwitterFind
{

    class events
    {

        public static String download_serialized_json_data()
        {
            var url = "http://enter77.ius.edu/~cjkimmer/t2.json";
            using (var w = new WebClient())
            {

                var json = string.Empty;
                // attempt to download JSON data as a string
                
                    json = w.DownloadString(url);

                    return json;
                }
               
        }

        public static void parse_json(String json_data, GMapControl map)
        {
            DataSet dataset;
            DataTable dataTable = null;
            try
            {
                dataset = JsonConvert.DeserializeObject<DataSet>(json_data);
                dataTable = dataset.Tables["Tweets"];
                foreach (DataRow row in dataTable.Rows)
                {
                    double latitude = Convert.ToDouble(row["lat"]);
                    double longitude = Convert.ToDouble(row["long"]);

                    GMapMarker marker = new GMapMarker(new PointLatLng(latitude, longitude));
                    marker.ZIndex = int.MaxValue;
                    Ellipse el = new Ellipse();
                    el.Height = 15;
                    el.Width = 15;
                    el.Fill = Brushes.Black;
                    marker.Shape = el;
                    map.Markers.Add(marker);

                }
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
            }

        }
    }
}
