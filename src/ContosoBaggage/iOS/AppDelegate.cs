using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using ContosoBaggage;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

using FFImageLoading.Forms.Touch;
using FFImageLoading.Svg.Forms;
using FFImageLoading;

namespace ContosoBaggage.iOS
{
    /// <summary>
    /// App delegate.
    /// </summary>
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            AppCenter.Start("e2b72caf-65e8-4355-ba1d-88fe9ad7ea12",
                   typeof(Analytics), typeof(Crashes));

            InitIoC();

            global::Xamarin.Forms.Forms.Init();

            CachedImageRenderer.Init();
            var ignore = typeof(SvgCachedImage);


            var config = new FFImageLoading.Config.Configuration()
            {
                VerboseLogging = false,
                VerbosePerformanceLogging = false,
                VerboseMemoryCacheLogging = false,
                VerboseLoadingCancelledLogging = false,
                Logger = new CustomLogger(),
            };
            ImageService.Instance.Initialize(config);

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        /// <summary>
        /// Custom logger.
        /// </summary>
        public class CustomLogger : FFImageLoading.Helpers.IMiniLogger
        {
            public void Debug(string message)
            {
                Console.WriteLine(message);
            }

            public void Error(string errorMessage)
            {
                Console.WriteLine(errorMessage);
            }

            public void Error(string errorMessage, Exception ex)
            {
                Error(errorMessage + System.Environment.NewLine + ex.ToString());
            }
        }

        #region Private Methods

        /// <summary>
        /// Inits the IoC container and modules
        /// </summary>
        /// <returns>The io c.</returns>
        private void InitIoC()
        {
            IoC.CreateContainer();
            IoC.RegisterModule(new CoreModule());
            IoC.StartContainer();
        }

        #endregion
    }
}
