using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E10069.vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageListSitio : ContentPage
    {
        public PageListSitio()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            list.ItemsSource = await App.Instancia.GetAll();
        }
        private async void tooladd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new vista.PageSitio());
        }

        private async void toolist_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new vista.PageListSitio());
        }

        private async void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var aceptar = "Si";
            await DisplayAlert("Aviso", "Desea ir a ese sitio", "No", aceptar);
            if (aceptar.Equals("Si"))
            {
                await Navigation.PushAsync(new vista.PageMapa());
            }
        }

        private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new vista.PageMapa());
        }

    }
}