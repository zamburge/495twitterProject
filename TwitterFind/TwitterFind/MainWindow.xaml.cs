using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System.Net;
using TwitterFind;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Documents;
using System.Windows.Shapes;
using System.Windows.Media;
using Newtonsoft.Json;


namespace TwitterFind
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PointLatLng start;
        PointLatLng end;
        GMapMarker currentMarker;
        String json_data = "";

        public MainWindow()
        {
            
            InitializeComponent();

            

            // config map
            MainMap.MapProvider = GMapProviders.OpenStreetMap;
            MainMap.Position = new PointLatLng(38, -85);

            comboBoxMapType.ItemsSource = GMapProviders.List;
            comboBoxMapType.DisplayMemberPath = "Name";
            comboBoxMapType.SelectedItem = MainMap.MapProvider;

            

            events.download_serialized_json_data(json_data);
            events.parse_json(json_data, MainMap);

        }

            private void button2_Click(object sender, RoutedEventArgs e)
      {
         try
         {
            double lat = double.Parse(textBoxLat.Text, CultureInfo.InvariantCulture);
            double lng = double.Parse(textBoxLong.Text, CultureInfo.InvariantCulture);

            MainMap.Position = new PointLatLng(lat, lng);
         }
         catch(Exception ex)
         {
            MessageBox.Show("incorrect coordinate format: " + ex.Message);
         }
      }

            private void textBoxGeo_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
            {
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    GeoCoderStatusCode status = MainMap.SetPositionByKeywords(textBoxGeo.Text);
                    if (status != GeoCoderStatusCode.G_GEO_SUCCESS)
                    {
                        MessageBox.Show("Geocoder can't find: '" + textBoxGeo.Text + "', reason: " + status.ToString(), "GMap.NET", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        currentMarker.Position = MainMap.Position;
                    }
                }
            }
       
        }


    
}
