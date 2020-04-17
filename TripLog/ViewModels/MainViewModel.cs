using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TripLog.Models;
using TripLog.Services;
using Xamarin.Forms;

namespace TripLog.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public Command<TripLogEntry> ViewCommand => new Command<TripLogEntry>(
            async entry => await NavService.NavigateTo<DetailViewModel, TripLogEntry>(entry));

        public Command NewCommand => new Command(async () =>
            await NavService.NavigateTo<NewEntryViewModel>());

        private ObservableCollection<TripLogEntry> _logEntries;

        public ObservableCollection<TripLogEntry> LogEntries
        {
            get => _logEntries;
            set
            {
                _logEntries = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(INavService navService) : base(navService)
        {
            LogEntries = new ObservableCollection<TripLogEntry>();
        }

        public override void Init()
        {
            LoadEntries();
        }

        private void LoadEntries()
        {
            LogEntries.Clear();
            LogEntries.Add(
                new TripLogEntry
                {
                    Title = "Estrella de Puebla",
                    Notes = "La Estrella de Puebla es una noria turística ubicada en la ciudad de Puebla de Zaragoza, en Puebla, México. Fue inaugurada el 22 de julio de 2013 por el gobernador Rafael Moreno Valle Rosas.1 La rueda recibió la acreditación del récord Guinness como la rueda de observación portátil más grande del mundo, con un diámetro de 69.8 metros y una altura de 80 metros (no confundir con el título de la rueda de observación fija más grande del mundo, que ostenta la Singapore Flyer de Singapur, con 165 metros de altura).",
                    Rating = 4,
                    Date = new DateTime(2019, 9, 15),
                    Latitude = 19.0348473,
                    Longitude = -98.2321892
                });
            LogEntries.Add(
                new TripLogEntry
                {
                    Title = "Pirámide de Cholula",
                    Notes = "La Gran Pirámide de Cholula o Tlachihualtépetl (del náhuatl \"cerro hecho a mano\") es el basamento piramidal más grande del mundo con 400 metros por lado.​ Es el sitio arqueológico más grande de una pirámide (templo) en el Nuevo Mundo , así como la pirámide más grande que existe en el mundo hoy en día.​ La pirámide se encuentra a 55 metros (180 pies) sobre la llanura circundante, y en su forma final midió 450 por 450 metros (1,480 por 1,480 pies).​ La pirámide es un templo que tradicionalmente se consideraba dedicado al dios Quetzalcoatl. El estilo arquitectónico del edificio estaba estrechamente relacionado con el de Teotihuacan en el Valle de México , aunque la influencia de la costa del Golfo también es evidente, especialmente de El Tajín",
                    Rating = 5,
                    Date = new DateTime(2020, 1, 1),
                    Latitude = 19.0552611,
                    Longitude = -98.3015374
                });
            LogEntries.Add(
                new TripLogEntry
                {
                    Title = "Reserva de la Biósfera de Calakmul",
                    Notes = "Calakmul es el sitio arqueológico más grande que da testimonio de la colonización del territorio, el crecimiento de la población y la compleja organización de las sociedades-estado con una amplia variedad de vestigios.",
                    Rating = 5,
                    Date = new DateTime(2001, 2, 28),
                    Latitude = 18.8284815,
                    Longitude = -89.6360779
                });
            LogEntries.Add(
                new TripLogEntry
                {
                    Title = "Grutas de Tolantongo, Hidalgo",
                    Notes = "Un cañón y un conjunto de cuevas donde por la gruta principal corren aguas termales. En las orillas del cañón, los visitantes colocan tiendas de campaña. Un lugar para los aventureros donde ir en México que gozan de explorar el aire libre.",
                    Rating = 5,
                    Date = new DateTime(2015, 2, 3),
                    Latitude = 26.7410186,
                    Longitude = -103.820979
                });
            LogEntries.Add(
                new TripLogEntry
                {
                    Title = "Washington Monument",
                    Notes = "Amazing!",
                    Rating = 3,
                    Date = new DateTime(2019, 2, 5),
                    Latitude = 38.8895,
                    Longitude = -77.0352
                });
            LogEntries.Add(
                new TripLogEntry
                {
                    Title = "Statue of Liberty",
                    Notes = "Inspiring!",
                    Rating = 4,
                    Date = new DateTime(2019, 4, 13),
                    Latitude = 40.6892,
                    Longitude = -74.0444
                });
            LogEntries.Add(
                new TripLogEntry
                {
                    Title = "Golden Gate Bridge",
                    Notes = "Foggy, but beautiful.",
                    Rating = 5,
                    Date = new DateTime(2019, 4, 26),
                    Latitude = 37.8268,
                    Longitude = -122.4798
                });                   
        }
    }
}
