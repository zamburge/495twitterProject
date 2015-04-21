using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using GMap.NET.WindowsPresentation;
using System.Diagnostics;
using System.Windows;

namespace TwitterFind
{
    class Markers
    {
        Popup Popup;
        Label Label;
        GMapMarker Marker;
        MainWindow MainWindow;

        public Markers(MainWindow window, GMapMarker marker, string text)
        {
            this.MainWindow = window;
            this.Marker = marker;

            Popup = new Popup();
            Label = new Label();

            //this.MouseLeftButtonUp += new MouseButtonEventHandler(CustomMarker_MouseLeftButtonUp);

            Popup.Placement = PlacementMode.Mouse;
            {
                Label.Background = Brushes.Blue;
                Label.Foreground = Brushes.White;
                Label.BorderBrush = Brushes.WhiteSmoke;
                Label.BorderThickness = new Thickness(2);
                Label.Padding = new Thickness(5);
                Label.FontSize = 22;
                Label.Content = text;
            }
        }

       /* void CustomMarker_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsMouseCaptured)
            {
                Mouse.Capture(null);
            }
        }*/
    }
}
