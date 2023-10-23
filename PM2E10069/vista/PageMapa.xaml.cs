using Plugin.Geolocator.Abstractions;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Position = Xamarin.Forms.Maps.Position;
using System.Net.NetworkInformation;
using PM2E10069.modelo;

namespace PM2E10069.vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMapa : ContentPage
    {
        Sitio sitio = new Sitio();
        public PageMapa()
        {
            InitializeComponent();
        }

        private void OnMapClicked(object sender, MapClickedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"MapClick: {e.Position.Latitude}, {e.Position.Longitude}");
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var conectividad = Connectivity.NetworkAccess;
            var locl = CrossGeolocator.Current;

            if (conectividad == NetworkAccess.Internet)
            {


                if (locl != null)
                {
                    locl.PositionChanged += Locl_PositionChanged;

                    if (!locl.IsListening)
                    {
                        await locl.StartListeningAsync(TimeSpan.FromSeconds(10), 100);
                    }

                    var posicion = await locl.GetPositionAsync();

                    Pin pin = new Pin
                    {
                        Label = "",
                        Type = PinType.Place,
                        Position = new Position(posicion.Latitude, posicion.Longitude)
                    };
                    mapa.Pins.Add(pin);
                    mapa.MoveToRegion(new MapSpan(pin.Position, 1, 1));

                }
            }
            else
            {
                var posicion = await locl.GetLastKnownLocationAsync();
                var mapcenter = new Position(posicion.Latitude, posicion.Longitude);
                mapa.MoveToRegion(new MapSpan(mapcenter, 1, 1));
            }
        }


        private void Locl_PositionChanged(object sender, PositionEventArgs e)
        {
            var mapcenter = new Position(e.Position.Latitude, e.Position.Longitude);
            mapa.MoveToRegion(new MapSpan(mapcenter, 1, 1));
        }
    }
}