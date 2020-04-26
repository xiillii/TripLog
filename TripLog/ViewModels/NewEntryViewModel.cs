﻿using System;
using System.Threading.Tasks;
using TripLog.Models;
using TripLog.Services;
using Xamarin.Forms;

namespace TripLog.ViewModels
{
    public class NewEntryViewModel : BaseValidationViewModel
    {
        private readonly ILocationService _locService;
        private Command _saveCommand;

        public Command SaveCommand =>
            _saveCommand ?? (_saveCommand = new Command(
                async () => await Save(), CanSave));

        


        private async Task Save()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                /**/
                var newItem = new TripLogEntry
                {
                    Title = Title,
                    Latitude = Latitude,
                    Longitude = Longitude,
                    Date = Date,
                    Rating = Rating,
                    Notes = Notes
                };

                // TODO: Persist entry

                await Task.Delay(3000);

                await NavService.GoBack();
            }
            finally
            {
                IsBusy = false;
            }
        }

        bool CanSave() => !string.IsNullOrWhiteSpace(Title) && !HasErrors;

        private string _title;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                Validate(() => !string.IsNullOrWhiteSpace(_title),
                    "Title must be provided");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        private double _latitude;

        public double Latitude
        {
            get => _latitude;
            set
            {
                _latitude = value;
                OnPropertyChanged();
            }
        }

        private double _longitude;

        public double Longitude
        {
            get => _longitude;
            set
            {
                _longitude = value;
                OnPropertyChanged();
            }
        }

        private DateTime _date;

        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        private int _rating;

        public int Rating
        {
            get => _rating;
            set
            {
                _rating = value;
                Validate(() => _rating >= 1 && _rating <= 5,
                    "Rating must be between 1 and 5");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        private string _notes;

        public string Notes
        {
            get => _notes;
            set
            {
                _notes = value;
                OnPropertyChanged();
            }
        }

        public NewEntryViewModel(INavService navService,
            ILocationService locService) : base(navService)
        {
            _locService = locService;

            Date = DateTime.Today;
            Rating = 1;
        }

        public override async void Init()
        {
            try
            {
                var coords = await _locService.GetGeoCoordinatesAsync();

                Latitude = coords.Latitude;
                Longitude = coords.Longitude;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
