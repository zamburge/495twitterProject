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
            var url = "http://enter77.ius.edu:3221/all?count=10";
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

        public String cluster_download_serialized_json_data()
        {
            var url = "http://enter77.ius.edu:3221/bins?minct=200";
            using (var w = new WebClient())
            {
                var json = string.Empty;
                // attempt to download JSON data as a string
                json = w.DownloadString(url);
                return json;
            }
        }

        public void cluster_parse_json(String json_data, GMapControl map)
        {
            DataSet dataset;
            DataTable dataTable = null;
            try
            {
                dataset = JsonConvert.DeserializeObject<DataSet>(json_data);
                dataTable = dataset.Tables["BinnedTweets"];
                foreach (DataRow row in dataTable.Rows)
                {
                    double latitude = Convert.ToDouble(row["lng"]);
                    double longitude = Convert.ToDouble(row["lat"]);
                    double count = (Convert.ToDouble(row["count"]));


                    GMapMarker marker = new GMapMarker(new PointLatLng(latitude, longitude));
                    marker.ZIndex = int.MaxValue;
                    Ellipse el = new Ellipse();

                    if (count <= 1000)
                    {
                        el.Height = 2;
                        el.Width = 2;
                    }

                    if (count >= 1001 && count <= 3000)
                    {
                        el.Height = 4;
                        el.Width = 4;
                    }

                    if (count >= 3001 && count <= 5000)
                    {
                        el.Height = 6;
                        el.Width = 6;
                    }

                    if (count >= 5001 && count <= 10000)
                    {
                        el.Height = 8;
                        el.Width = 8;
                    }

                    if (count >= 10001 && count <= 20000)
                    {
                        el.Height = 10;
                        el.Width = 10;
                    }

                    if (count >= 20001 && count <= 40000)
                    {
                        el.Height = 12;
                        el.Width = 12;
                    }

                    if (count > 40000)
                    {
                        el.Height = 14;
                        el.Width = 14;
                    }
                    
                    el.Fill = Brushes.Black;
                    marker.Shape = el;
                    map.Markers.Add(marker);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        
    }
}
