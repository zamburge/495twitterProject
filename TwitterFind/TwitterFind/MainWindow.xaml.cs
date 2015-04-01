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
using TwitterFind.events;
using TwitterFind.model;


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

        public MainWindow()
        {
            InitializeComponent();

            // add your custom map db provider
            //MySQLPureImageCache ch = new MySQLPureImageCache();
            //ch.ConnectionString = @"server=sql2008;User Id=trolis;Persist Security Info=True;database=gmapnetcache;password=trolis;";
            //MainMap.Manager.SecondaryCache = ch;

            // set your proxy here if need
            //GMapProvider.IsSocksProxy = true;
            //GMapProvider.WebProxy = new WebProxy("127.0.0.1", 1080);
            //GMapProvider.WebProxy.Credentials = new NetworkCredential("ogrenci@bilgeadam.com", "bilgeada");
            // or
            //GMapProvider.WebProxy = WebRequest.DefaultWebProxy;
            //

            // set cache mode only if no internet avaible

            // config map
            MainMap.MapProvider = GMapProviders.OpenStreetMap;
            MainMap.Position = new PointLatLng(38, -85);

            comboBoxMapType.ItemsSource = GMapProviders.List;
            comboBoxMapType.DisplayMemberPath = "Name";
            comboBoxMapType.SelectedItem = MainMap.MapProvider;

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
