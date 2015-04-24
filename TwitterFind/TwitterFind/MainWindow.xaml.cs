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
using System.Windows.Navigation;
using System.Windows.Documents;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.Web;
using Leap;
using Vector = Leap.Vector;

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
        String search_data = "";
        events theController;
        private CustomHandler handler;

        public MainWindow()
        {            
            InitializeComponent();

            theController = new events(this);

            // config map
            MainMap.MapProvider = GMapProviders.OpenStreetMap;
            MainMap.Position = new PointLatLng(38, -85);
            MainMap.MapProvider = GMapProviders.GoogleMap;

            //Map Events
            MainMap.ShowTileGridLines = false;

            //MainMap.OnPositionChanged += new PositionChanged(MainMap_OnCurrentPositionChanged);
            comboBoxMapType.ItemsSource = GMapProviders.List;
            comboBoxMapType.DisplayMemberPath = "Name";
            comboBoxMapType.SelectedItem = MainMap.MapProvider;

            //Execute JSON data
            json_data = theController.download_serialized_json_data();
            theController.parse_json(json_data, MainMap);
            try
            {
                handler = new CustomHandler();
                this.handler.Listener.OnGestureMade += GestureHandler;
            }
            catch
            {

            }

            LeapHelpDisplayButton.Click += leapHelpVisible;
            ReturnToMapButton.Click += leapHelpHidden;
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

         private void KeyButton_Click(object sender, RoutedEventArgs e)
         {
             //clear Markers off map
             var clear = MainMap.Markers.Where(p => p != null && p != currentMarker);
             if (clear != null)
             {
                 for (int i = 0; i < clear.Count(); i++)
                 {
                     MainMap.Markers.Remove(clear.ElementAt(i));
                     i--;
                 }
             }
             String s = this.KeywordSearchText.Text;
             String buildUrl = "http://enter77.ius.edu:3221/text?terms=" + s + "&count=7500";
             WebUtility.UrlEncode(buildUrl);
             var url = buildUrl;
             Console.WriteLine(url);
             search_data = theController.download_search_json(url);
             theController.parse_json(search_data, MainMap);
         }

         private void Area_Search_Click(object sender, RoutedEventArgs e)
         {
             //clear Markers off map
             var clear = MainMap.Markers.Where(p => p != null && p != currentMarker);
             if (clear != null)
             {
                 for (int i = 0; i < clear.Count(); i++)
                 {
                     MainMap.Markers.Remove(clear.ElementAt(i));
                     i--;
                 }
             }
             String latSearch = this.LatSearchText.Text;
             String longSearch = this.LongSearchText.Text;
             String buildUrl = "http://enter77.ius.edu:3221/near?lat=" + latSearch + "&lng=" + longSearch + "&count=7500";
             var url = buildUrl;
             Uri.EscapeDataString(url);
             Console.WriteLine(url);
             search_data = theController.download_search_json(url);
             theController.parse_json(search_data, MainMap);
         }
         public void leapHelpVisible(object sender, RoutedEventArgs e)
         {
             LEAP_Motion_Help.Visibility = Visibility.Visible;
             MainMap.Visibility = Visibility.Hidden;
         }

         public void leapHelpHidden(object sender, RoutedEventArgs e)
         {
             LEAP_Motion_Help.Visibility = Visibility.Hidden;
             MainMap.Visibility = Visibility.Visible;
         }
         public void GestureHandler(GestureList gestures)
         {
             foreach (Gesture gesture in gestures)
             {
                 if (gesture.Type == Gesture.GestureType.TYPESCREENTAP)
                 {
                     Dispatcher.Invoke(() =>
                     {
                         MainMap.Zoom++;
                     });
                 }
                 if (gesture.Type == Gesture.GestureType.TYPEKEYTAP)
                 {
                     Dispatcher.Invoke(() =>
                     {
                         MainMap.Zoom--;
                     });
                 }
             }
         }

        

         private void Cluster_Button_Click(object sender, RoutedEventArgs e)
         {
             //clear Markers off map
             var clear = MainMap.Markers.Where(p => p != null && p != currentMarker);
             if (clear != null)
             {
                 for (int i = 0; i < clear.Count(); i++)
                 {
                     MainMap.Markers.Remove(clear.ElementAt(i));
                     i--;
                 }
             }

             json_data = theController.cluster_download_serialized_json_data();
             theController.cluster_parse_json(json_data, MainMap);
         }

         

         

         

         
     }   
}
