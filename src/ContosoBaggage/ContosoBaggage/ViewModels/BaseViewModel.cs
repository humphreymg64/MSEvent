using System;
using System.Threading.Tasks;

using ContosoBaggage.Controls;
using ContosoBaggage.Models;
using ContosoBaggage.Navigation;
using ContosoBaggage.Services;

namespace ContosoBaggage.ViewModels
{
    /// <summary>
    /// Base view model.
    /// </summary>
    public class BaseViewModel : ObservableObject
    {
        /// <summary>
        /// The navigation.
        /// </summary>
        protected INavigationService Navigation;

        /// <summary>
        /// The current date.
        /// </summary>
        private DateTime _currentDate;

        /// <summary>
        /// The flight service.
        /// </summary>
        protected FlightService FlightService;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:XChange.Portable.ViewModels.ViewModelBase"/> is connected.
        /// </summary>
        /// <value><c>true</c> if is connected; otherwise, <c>false</c>.</value>
        public DateTime CurrentDate
        {
            get { return _currentDate; }
            set { this.RaiseAndSetIfChanged(ref _currentDate, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ContosoBaggage.ViewModels.BaseViewModel"/> class.
        /// </summary>
        public BaseViewModel()
        {
            FlightService = new FlightService();
            Navigation = IoC.Resolve<INavigationService>();
        }

        /// <summary>
        /// Private backing field to hold the title
        /// </summary>
        string title = string.Empty;
        /// <summary>
        /// Public property to set and get the title of the item
        /// </summary>
        public string Title
        {
            get { return title; }
            set { this.RaiseAndSetIfChanged(ref title, value); }
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { this.RaiseAndSetIfChanged(ref isBusy, value); }
        }

        public virtual void OnAppear()
        {
        }

        public virtual void OnDisappear()
        {
        }

        public virtual void OnSleep()
        {
            
        }

        public virtual void OnResume()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="parameters">
        /// </param>
        public virtual async Task LoadAsync(NavigationParameters parameters)
        {
            CurrentDate = DateTime.Now;
        }

        /// <summary>
        /// Gos to page.
        /// </summary>
        /// <param name="page">Page.</param>
        /// <param name="parameters">Parameters.</param>
        public void GoToPage(PageNames page, NavigationParameters parameters)
        {
            Navigation.Navigate(page, parameters);
        }
    }
}
