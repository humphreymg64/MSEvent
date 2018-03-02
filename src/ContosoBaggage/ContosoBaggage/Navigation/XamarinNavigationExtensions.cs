// --------------------------------------------------------------------------------------------------
//  <copyright file="XamarinNavigationExtensions.cs" company="Flush Arcade Pty Ltd.">
//    Copyright (c) 2014 Flush Arcade Pty Ltd. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

namespace ContosoBaggage.Controls
{
	using System.Collections.Generic;

    using Xamarin.Forms;

    using ContosoBaggage.ViewModels;
    using ContosoBaggage.Navigation;

    /// <summary>
    /// Xamarin navigation extensions.
    /// </summary>
    public static class XamarinNavigationExtensions
	{
		#region Public Methods and Operators

		/// <summary>
		/// Show the specified page and parameters.
		/// </summary>
		/// <param name="page">Page.</param>
		/// <param name="parameters">Parameters.</param>
        public static void Show(this ContentPage page, NavigationParameters parameters)
		{
			var target = page.BindingContext as BaseViewModel;

			if (target != null)
			{
                target.LoadAsync(parameters).ConfigureAwait(false);
			}
		}

		#endregion
	}
}