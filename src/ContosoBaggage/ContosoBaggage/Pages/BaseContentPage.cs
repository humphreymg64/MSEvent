using System;
using System.Threading.Tasks;
using ContosoBaggage.Navigation;
using ContosoBaggage.ViewModels;
using ContosoBaggage.Controls;

using Microsoft.AppCenter.Analytics;

using Xamarin.Forms;

namespace ContosoBaggage.Pages
{
    public class BaseContentPage<T> : ContentPage where T : BaseViewModel, 
        new()
    {
        #region Properties & Constructors

        T _viewModel;

        public T ViewModel
        {
            get { return _viewModel; }

            protected set { _viewModel = value; }
        }

        #endregion

        #region Constructors

        public BaseContentPage()
        {
            _viewModel = new T();

            Initialize();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        public BaseContentPage(T viewModel)
        {
            _viewModel = viewModel;
            Initialize();
        }

        #endregion

        #region Lifecycle

        void Initialize()
        {
            BindingContext = _viewModel;

            Analytics.TrackEvent($"{GetType().Name} loaded");
        }

        /// <summary>
        /// Ons the appearing.
        /// </summary>
        protected override void OnAppearing()
        {
            ViewModel?.OnAppear();

            base.OnAppearing();
        }

        /// <summary>
        /// Ons the disappearing.
        /// </summary>
        protected override void OnDisappearing()
        {
            ViewModel?.OnDisappear();

            base.OnDisappearing();
        }

        /// <summary>
        /// Ons the app backgrounded.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void OnAppBackgrounded(object sender, EventArgs e)
        {
            OnSleep();
        }

        /// <summary>
        /// Ons the app resumed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void OnAppResumed(object sender, EventArgs e)
        {
            OnResume();
        }

        /// <summary>
        /// Ons the sleep.
        /// </summary>
        protected virtual void OnSleep()
        {
            ViewModel?.OnSleep();
        }

        /// <summary>
        /// Ons the resume.
        /// </summary>
        protected virtual void OnResume()
        {
            ViewModel?.OnResume();
        }

        /// <summary>
        /// Ons the navigated to.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
            this.Show(parameters);
        }

        #endregion
    }
}