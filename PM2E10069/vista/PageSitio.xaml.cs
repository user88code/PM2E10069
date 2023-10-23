using Plugin.Geolocator.Abstractions;
using Plugin.Geolocator;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace PM2E10069.vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSitio : ContentPage
    {
        Plugin.Media.Abstractions.MediaFile photo = null;
        public PageSitio()
        {
            InitializeComponent();
        }

        private async void btnguardar_Clicked(object sender, EventArgs e)
        {
            var sitio = new modelo.Sitio()
            {

                latitude = Double.Parse(Latitud.Text),
                longitude = Double.Parse(Longitud.Text),
                descripcion = Descripcion.Text,
                foto = GetimageBytes()
            };

            if (await App.Instancia.AddSitio(sitio) > 0)
            {
                await DisplayAlert("Aviso", "Sitio ingresado con exito", "OK");
            }
            else
                await DisplayAlert("Aviso", "a ocurrido un error", "OK");
        }

        private async void btnfoto_Clicked(object sender, EventArgs e)
        {
            photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "MYAPP",
                Name = "Foto.jpg",
                SaveToAlbum = true
            });

            if (photo != null)
            {
                foto.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
            }
        }

        public String Getimage64()
        {
            if (photo != null)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    Stream stream = photo.GetStream();
                    stream.CopyTo(memory);
                    byte[] fotobyte = memory.ToArray();

                    String Base64 = Convert.ToBase64String(fotobyte);

                    return Base64;
                }
            }

            return null;
        }

        public byte[] GetimageBytes()
        {
            if (photo != null)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    Stream stream = photo.GetStream();
                    stream.CopyTo(memory);
                    byte[] fotobyte = memory.ToArray();

                    return fotobyte;
                }

            }

            return null;
        }

        private async void btngetubicacion_Clicked(object sender, EventArgs e)
        {
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

                    double latitude = posicion.Latitude;
                    Latitud.Text = latitude.ToString();

                    double longitude = posicion.Longitude;
                    Longitud.Text = longitude.ToString();

                    await locl.StopListeningAsync();
                }
            }
            else
            {
                var posicion = await locl.GetLastKnownLocationAsync();
                double latitude = posicion.Latitude;
                Latitud.Text = latitude.ToString();

                double longitude = posicion.Longitude;
                Longitud.Text = longitude.ToString();
            }
        }

        private void Locl_PositionChanged(object sender, PositionEventArgs e)
        {
            double latitude = e.Position.Latitude;
            Latitud.Text = latitude.ToString();

            double longitude = e.Position.Longitude;
            Longitud.Text = longitude.ToString();
        }
    }

}
