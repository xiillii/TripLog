﻿
using TripLog.Models;

namespace TripLog.ViewModels
{
    public class DetailViewModel : BaseViewModel
    {
        private TripLogEntry _entry;

        public TripLogEntry Entry
        {
            get => _entry;
            set
            {
                _entry = value;
                OnPropertyChanged();
            }
        }

        public DetailViewModel(TripLogEntry entry)
        {
            Entry = entry;
        }
    }
}
