using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TripLog.Services;
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
                    //color = Color.Black;
                    color = new Color(Color.Default.R < 0 ? 0 : Color.Default.R,
                        Color.Default.G < 0 ? 0 : Color.Default.G,
                        Color.Default.B < 0 ? 0 : Color.Default.B);
                }
            }
            else
            {
                color = new Color(Color.Default.R < 0 ? 0 : Color.Default.R,
                        Color.Default.G < 0 ? 0 : Color.Default.G,
                        Color.Default.B < 0 ? 0 : Color.Default.B);
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