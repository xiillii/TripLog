using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripLog.Models;
using TripLog.ViewModels;
using Xamarin.Forms;

namespace TripLog.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            
            BindingContext = new MainViewModel();
        }

        private async void New_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewEntryPage());
        }

        private async void Trips_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            var trip = (TripLogEntry)e.CurrentSelection.FirstOrDefault();

            if (trip != null)
            {
                await Navigation.PushAsync(new DetailPage(trip));
            }

            trips.SelectedItem = null;
        }
    }
}
