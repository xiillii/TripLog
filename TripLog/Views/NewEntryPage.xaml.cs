using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TripLog.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TripLog.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewEntryPage : ContentPage
    {
        NewEntryViewModel ViewModel => BindingContext as NewEntryViewModel;

        public NewEntryPage()
        {
            InitializeComponent();

            BindingContextChanged += Page_BindingContextChanged;

            BindingContext = new NewEntryViewModel();
                       
        }

        private void Page_BindingContextChanged(object sender, EventArgs e)
        {
            ViewModel.ErrorsChanged += ViewModel_ErrorsChanged;
        }

        private void ViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            var propHasErrors =
                (ViewModel.GetErrors(e.PropertyName) as List<string>)?.Any() == true;

            Color color;

            // Getting the dynamic color. Dark Theme or Light Theme
            if (Application.Current.Resources != null)
            {
                var y = Application.Current.Resources.FirstOrDefault(x => x.Key == "TextColorLabel");
                if (y.Key != null)
                {
                    color = (Color)y.Value;
                }
                else
                {
                    color = Color.Default;
                }
            }
            else
            {
                color = Color.Default;
            }

            switch (e.PropertyName)
            {
                case nameof(ViewModel.Title):
                    title.LabelColor = propHasErrors ? Color.Red : color;
                    break;
                case nameof(ViewModel.Rating):
                    rating.TextColor = propHasErrors ? Color.Red : color;
                    break;
                default:
                    break;
            }
        }
    }
}