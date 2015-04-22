using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using GMap.NET.WindowsPresentation;
using System.Windows.Shapes;

namespace TwitterFind
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Marker : UserControl
    {
        Popup Popup;
        Label Label;
        GMapMarker gmapMarker;
        MainWindow MainWindow;
        public  Marker(MainWindow window, GMapMarker gmapMarker, string text)
        {
            InitializeComponent();

            this.MainWindow = window;
            this.gmapMarker = gmapMarker;

            Popup = new Popup();
            Label = new Label();

            this.MouseLeftButtonUp += new MouseButtonEventHandler(Marker_MouseLeftButtonUp);
            this.MouseEnter += new MouseEventHandler(MarkerControl_MouseEnter);
            this.MouseLeave += new MouseEventHandler(MarkerControl_MouseLeave);

            Popup.Placement = PlacementMode.Mouse;
            {
                Label.Background = Brushes.LightGray;
                Label.Foreground = Brushes.Black;
                Label.BorderBrush = Brushes.Black;
                Label.BorderThickness = new Thickness(2);
                Label.Padding = new Thickness(5);
                Label.FontSize = 14;
                Label.Content = text;
            }
            Popup.Child = Label;
        }

        void Marker_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsMouseCaptured)
            {
                Mouse.Capture(null);
            }
        }

        void MarkerControl_MouseEnter(object sender, MouseEventArgs e)
        {
            gmapMarker.ZIndex += 10000;
            Popup.IsOpen = true;
        }

        void MarkerControl_MouseLeave(object sender, MouseEventArgs e)
        {
            gmapMarker.ZIndex -= 10000;
            Popup.IsOpen = false;
        }
    }
}
