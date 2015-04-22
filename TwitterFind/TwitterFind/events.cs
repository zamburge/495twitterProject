using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterFind;
using System.Net.Http;
using System.Net.Http.Headers;
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
using System.Windows;
using System.Windows.Controls.Primitives;

namespace TwitterFind
{
    class events
    {
        MainWindow theMainWindow;

        public events(MainWindow mainWindow)
        {
            theMainWindow = mainWindow;
        }
        public String download_serialized_json_data()
        {
            var url = "http://enter77.ius.edu:3221/all?count=7500";
            using (var w = new WebClient())
            {
                var json = string.Empty;
                // attempt to download JSON data as a string
                    json = w.DownloadString(url);
                    return json;
                }          
        }

        public void parse_json(String json_data, GMapControl map)
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
                    double longitude = Convert.ToDouble(row["lng"]);
                    String text = (Convert.ToString(row["text"]));
                    GMapMarker marker = new GMapMarker(new PointLatLng(latitude, longitude));
                    {
                        marker.Shape = new Marker(theMainWindow, marker, text);
                        marker.Offset = new System.Windows.Point(-15, -15);
                        marker.ZIndex = int.MaxValue;
                        map.Markers.Add(marker);
                    }
                }
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
            }
        }

        public String download_search_json(String concat)
        {
            
            using (var w = new WebClient())
            {
                var json = string.Empty;
                // attempt to download JSON data as a string
                json = w.DownloadString(concat);
                return json;
            }
        }

        
    }
}
