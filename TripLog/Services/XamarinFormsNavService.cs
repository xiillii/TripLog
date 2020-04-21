using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TripLog.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace TripLog.Services
{
    public class XamarinFormsNavService : INavService
    {
        public INavigation XamarinFormsNav { get; set; }
        private readonly IDictionary<Type, Type> _map = new Dictionary<Type, Type>();

        public event PropertyChangedEventHandler CanGoBackChanged;

        public bool CanGoBack => XamarinFormsNav.NavigationStack != null &&
            XamarinFormsNav.NavigationStack.Count > 0;



        public void RegisterViewMapping(Type viewModel, Type view)
        {
            _map.Add(viewModel, view);
        }

        public async Task GoBack()
        {
            if (CanGoBack)
            {
                await XamarinFormsNav.PopAsync(true);
                OnCanGoBackChanged();
            }
        }

        public async Task NavigateTo<TVM>() where TVM : BaseViewModel
        {
            await NavigateToView(typeof(TVM));

            if (XamarinFormsNav.NavigationStack.Last().BindingContext is BaseViewModel)
            {
                ((BaseViewModel)XamarinFormsNav.NavigationStack.Last().BindingContext).Init();
            }
        }

        public async Task NavigateTo<TVM, TParameter>(TParameter parameter)
            where TVM : BaseViewModel
        {
            await NavigateToView(typeof(TVM));

            if (XamarinFormsNav.NavigationStack.Last().BindingContext is BaseViewModel<TParameter>)
            {
                ((BaseViewModel<TParameter>)XamarinFormsNav.NavigationStack.Last().BindingContext)
                    .Init(parameter);
            }
        }

        public void RemoveLastView()
        {
            if (XamarinFormsNav.NavigationStack.Count < 2)
            {
                return;
            }
            var lastView = XamarinFormsNav
                .NavigationStack[XamarinFormsNav.NavigationStack.Count - 2];
            XamarinFormsNav.RemovePage(lastView);
        }

        public void ClearBackStack()
        {
            if (XamarinFormsNav.NavigationStack.Count < 2)
            {
                return;
            }

            for (var i = 0; i < XamarinFormsNav.NavigationStack.Count - 1; i++)
            {
                XamarinFormsNav.RemovePage(XamarinFormsNav.NavigationStack[i]);
            }
        }

        public async void NavigateToUri(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentException("Invalid URI");
            }

            //Device.OpenUri(uri);
            await Launcher.OpenAsync(uri);
            
        }

        private async Task NavigateToView(Type viewModelType)
        {
            if (!_map.TryGetValue(viewModelType, out var viewType))
            {
                throw new ArgumentException(
                    "No view found in view mapping for " + viewModelType.FullName);
            }

            // user reflection to get the View's constructor and create an
            // instance of the view

            var constructor = viewType.GetTypeInfo()
                .DeclaredConstructors
                .FirstOrDefault(dc => !dc.GetParameters().Any());

            var view = constructor.Invoke(null) as Page;
            var vm = ((App)Application.Current).Kernel
                .GetService(viewModelType);
            view.BindingContext = vm;

            await XamarinFormsNav.PushAsync(view, true);
        }

        private void OnCanGoBackChanged() => CanGoBackChanged?.Invoke(this,
            new PropertyChangedEventArgs("CanGoBack"));
    }
}
