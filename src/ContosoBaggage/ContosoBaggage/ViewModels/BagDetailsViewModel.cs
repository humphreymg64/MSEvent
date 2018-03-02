
using Microsoft.AppCenter.Analytics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

using ContosoBaggage.Common.Models;
using ContosoBaggage.Services;
using ContosoBaggage.Models;
using System.Windows.Input;

namespace ContosoBaggage.ViewModels
{
    public class BagDetailsViewModel : BaseViewModel
    {
        #region Flight variables

        /// <summary>
        /// The baggage identifier.
        /// </summary>
        string _baggageId;

        /// <summary>
        /// Gets or sets the baggage identifier.
        /// </summary>
        /// <value>The baggage identifier.</value>
        public string BaggageId
        {
            get { return _baggageId; }
            set { this.RaiseAndSetIfChanged(ref _baggageId, value); }
        }

        /// <summary>
        /// The baggage identifier.
        /// </summary>
        double _weight;

        /// <summary>
        /// Gets or sets the baggage identifier.
        /// </summary>
        /// <value>The baggage identifier.</value>
        public double Weight
        {
            get { return _weight; }
            set { this.RaiseAndSetIfChanged(ref _weight, value); }
        }

        /// <summary>
        /// The status.
        /// </summary>
        string _status;

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public string Status
        {
            get { return _status; }
            set { this.RaiseAndSetIfChanged(ref _status, value); }
        }

        /// <summary>
        /// The status.
        /// </summary>
        string _bagId;

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public string BagId
        {
            get { return _bagId; }
            set { this.RaiseAndSetIfChanged(ref _bagId, value); }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ContosoBaggage.ViewModels.BagDetailsViewModel"/> class.
        /// </summary>
        public BagDetailsViewModel()
        {
        }

        /// <summary>
        /// Loads the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="parameters">Parameters.</param>
        public override Task LoadAsync(Navigation.NavigationParameters parameters)
        {
            if (parameters.Contains("bag"))
            {
                var flight = (BaggageItem)parameters.Get("bag");
                Apply(flight);
            }

            return base.LoadAsync(parameters);
        }

        /// <summary>
        /// Apply the specified bag.
        /// </summary>
        /// <returns>The apply.</returns>
        /// <param name="bag">Bag.</param>
        void Apply(BaggageItem bag)
        {
            Title = "Baggage Id#: " + bag.BaggageId;
            BaggageId = bag.BaggageId;
            Weight = bag.Weight;
            Status = bag.Status;
            BagId = bag.Id;
        }
    }
}