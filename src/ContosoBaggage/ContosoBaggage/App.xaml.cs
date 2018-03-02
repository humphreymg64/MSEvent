using System;
using System.Collections.Generic;

using Xamarin.Forms;

using ContosoBaggage.Pages;
using ContosoBaggage.Navigation;
using ContosoBaggage.Controls;

namespace ContosoBaggage
{
    /// <summary>
    /// App.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ContosoBaggage.App"/> class.
        /// </summary>
        public App()
        {
            InitializeComponent();

            var navPage = IoC.Resolve<NavigationPage>();
            // call OnNavigatedTo when page first loads.
            (navPage.CurrentPage as INavigableXamarinFormsPage).OnNavigatedTo(null);
            MainPage = IoC.Resolve<NavigationPage>();
        }

        /// <summary>
        /// Ons the start.
        /// </summary>
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        /// <summary>
        /// Ons the sleep.
        /// </summary>
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        /// <summary>
        /// Ons the resume.
        /// </summary>
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

