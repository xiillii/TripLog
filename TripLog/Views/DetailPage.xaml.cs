using TripLog.Models;
using TripLog.Services;
using TripLog.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TripLog.Views
{
    public partial class DetailPage : ContentPage
    {
        DetailViewModel ViewModel => BindingContext as DetailViewModel;

        public DetailPage()
        {
            InitializeComponent();

            BindingContext = new DetailViewModel(DependencyService.Get<INavService>());           
        }

        private void UpdateMap()
        {
            if (ViewModel.Entry == null)
            {
                return;
            }

            // center the  map
            map.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Position(ViewModel.Entry.Latitude, ViewModel.Entry.Longitude),
                Distance.FromMiles(.5)));

            // place a pin
            map.Pins.Add(new Pin
            {
                Type = PinType.Place,
                Label = ViewModel.Entry.Title,
                Position = new Position(ViewModel.Entry.Latitude, ViewModel.Entry.Longitude)
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel != null)
            {
                ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (ViewModel != null)
            {
                ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
            }
        }

        private void ViewModel_PropertyChanged(object sender,
            System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DetailViewModel.Entry))
            {
                UpdateMap();
            }
        }
    }
}
