
namespace ContosoBaggage
{
    using Autofac;

    using ContosoBaggage.Controls;
    using ContosoBaggage.Pages;

    using Xamarin.Forms;

    /// <summary>
    /// Core module.
    /// </summary>
    public class CoreModule : IModule
    {
        #region Public Methods

        /// <summary>
        /// Register the specified builder.
        /// </summary>
        /// <param name="builder">builder.</param>
        public void Register(ContainerBuilder builder)
        {
            builder.Register(x => new NavigationPage(new MainPage()))
                   .AsSelf()
                   .SingleInstance();

            builder.RegisterType<NavigationService>()
                   .As<INavigationService>().SingleInstance();
        }

        #endregion
    }
}