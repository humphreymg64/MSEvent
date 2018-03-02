
namespace ContosoBaggage.Controls
{
	using System.Threading.Tasks;
	using System.Collections.Generic;

	using Xamarin.Forms;

    using ContosoBaggage.Controls;
    using ContosoBaggage.Navigation;
    using ContosoBaggage.Pages;
    using System;

    /// <summary>
    /// Navigation service.
    /// </summary>
    public class NavigationService : INavigationService
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ContosoBaggage.Controls.NavigationService"/> class.
        /// </summary>
        public NavigationService()
        {
        }

		#region INavigationService implementation

		/// <summary>
        /// Navigate the specified pageName and parameters.
        /// </summary>
        /// <returns>The navigate.</returns>
        /// <param name="pageName">Page name.</param>
        /// <param name="parameters">Parameters.</param>
		public async Task Navigate (PageNames pageName, NavigationParameters parameters)
		{
            var page = GetPage(pageName);

            if (page != null)
            {
                var navigablePage = page as INavigableXamarinFormsPage;

                if (navigablePage != null)
                {
                    await IoC.Resolve<NavigationPage>().PushAsync(page);
                    navigablePage.OnNavigatedTo(parameters);
                }
            }
		}

		/// <summary>
		/// Pop this instance.
		/// </summary>
		public async Task Pop()
		{
            await IoC.Resolve<NavigationPage>().PopAsync();
		}

		#endregion

		/// <summary>
		/// Gets the page.
		/// </summary>
		/// <returns>The page.</returns>
		/// <param name="page">Page.</param>
		private Page GetPage(PageNames page)
		{
			switch(page)
			{
				case PageNames.MainPage:
                    return new MainPage();
				case PageNames.FlightListPage:
                    return new FlightListPage();
                case PageNames.FlightDetailsPage:
                    return new FlightDetailsPage();
                case PageNames.BagDetailsPage:
                    return new BagDetailsPage();
				default:
					return null;
			}
		}
	}
}