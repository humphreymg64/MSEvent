
namespace ContosoBaggage.Controls
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

    using ContosoBaggage.Navigation;

    /// <summary>
    /// Navigation service.
    /// </summary>
    public interface INavigationService
	{
		#region Methods

		/// <summary>
		/// Navigate the specified pageName and navigationParameters.
		/// </summary>
		/// <param name="pageName">Page name.</param>
		/// <param name="navigationParameters">Navigation parameters.</param>
		Task Navigate (PageNames pageName, NavigationParameters parameters);

		#endregion
	}
}