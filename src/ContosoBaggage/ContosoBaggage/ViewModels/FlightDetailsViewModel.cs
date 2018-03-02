
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
using ContosoBaggage.Navigation;
using ContosoBaggage.Controls;

namespace ContosoBaggage.ViewModels
{
    public class FlightDetailsViewModel : BaseViewModel
    {
        #region Flight variables

        /// <summary>
        /// The flight number.
        /// </summary>
        string _flightNumber;

        /// <summary>
        /// Gets or sets the flight number.
        /// </summary>
        /// <value>The flight number.</value>
        public string FlightNumber
        {
            get { return _flightNumber; }
            set { this.RaiseAndSetIfChanged(ref _flightNumber, value); }
        }

        /// <summary>
        /// The flight date.
        /// </summary>
        DateTime _flightDate;

        /// <summary>
        /// Gets or sets the flight date.
        /// </summary>
        /// <value>The flight date.</value>
        public DateTime FlightDate
        {
            get { return _flightDate; }
            set { this.RaiseAndSetIfChanged(ref _flightDate, value); }
        }

        /// <summary>
        /// The arrival airport code.
        /// </summary>
        string _arrivalAirportCode;

        /// <summary>
        /// Gets or sets the arrival airport code.
        /// </summary>
        /// <value>The arrival airport code.</value>
        public string ArrivalAirportCode
        {
            get { return _arrivalAirportCode; }
            set { this.RaiseAndSetIfChanged(ref _arrivalAirportCode, value); }
        }

        /// <summary>
        /// The arrival city.
        /// </summary>
        string _arrivalCity;

        /// <summary>
        /// Gets or sets the arrival city.
        /// </summary>
        /// <value>The arrival city.</value>
        public string ArrivalCity
        {
            get { return _arrivalCity; }
            set { this.RaiseAndSetIfChanged(ref _arrivalCity, value); }
        }

        /// <summary>
        /// The arrival time.
        /// </summary>
        DateTime _arrivalTime;

        /// <summary>
        /// Gets or sets the arrival time.
        /// </summary>
        /// <value>The arrival time.</value>
        public DateTime ArrivalTime
        {
            get { return _arrivalTime; }
            set
            {
                this.OnPropertyChanged(Duration);
                this.RaiseAndSetIfChanged(ref _arrivalTime, value);
            }
        }

        /// <summary>
        /// The departure airport code.
        /// </summary>
        string _departureAirportCode;

        /// <summary>
        /// Gets or sets the departure airport code.
        /// </summary>
        /// <value>The departure airport code.</value>
        public string DepartureAirportCode
        {
            get { return _departureAirportCode; }
            set { this.RaiseAndSetIfChanged(ref _departureAirportCode, value); }
        }

        /// <summary>
        /// The departure city.
        /// </summary>
        string _departureCity;

        /// <summary>
        /// Gets or sets the departure city.
        /// </summary>
        /// <value>The departure city.</value>
        public string DepartureCity
        {
            get { return _departureCity; }
            set { this.RaiseAndSetIfChanged(ref _departureCity, value); }
        }

        /// <summary>
        /// The departure time.
        /// </summary>
        DateTime _departureTime;

        /// <summary>
        /// Gets or sets the departure time.
        /// </summary>
        /// <value>The departure time.</value>
        public DateTime DepartureTime
        {
            get { return _departureTime; }
            set
            {
                this.OnPropertyChanged(Duration);
                this.RaiseAndSetIfChanged(ref _departureTime, value);
            }
        }

        /// <summary>
        /// The boarding time.
        /// </summary>
        DateTime _boardingTime;

        /// <summary>
        /// Gets or sets the bording time.
        /// </summary>
        /// <value>The bording time.</value>
        public DateTime BoardingTime
        {
            get { return _boardingTime; }
            set
            {
                this.OnPropertyChanged(Duration);
                this.RaiseAndSetIfChanged(ref _boardingTime, value);
            }
        }

        /// <summary>
        /// The airline.
        /// </summary>
        string _airline;

        /// <summary>
        /// Gets or sets the airline.
        /// </summary>
        /// <value>The airline.</value>
        public string Airline
        {
            get { return _airline; }
            set { this.RaiseAndSetIfChanged(ref _airline, value); }
        }

        /// <summary>
        /// The flight status.
        /// </summary>
        string _flightStatus;

        /// <summary>
        /// Gets or sets the flight status.
        /// </summary>
        /// <value>The flight status.</value>
        public string FlightStatus
        {
            get { return _flightStatus; }
            set { this.RaiseAndSetIfChanged(ref _flightStatus, value); }
        }

        /// <summary>
        /// The flight status icon.
        /// </summary>
        string _flightStatusIcon;

        /// <summary>
        /// Gets or sets the flight status icon.
        /// </summary>
        /// <value>The flight status icon.</value>
        public string FlightStatusIcon
        {
            get { return _flightStatusIcon; }
            set { this.RaiseAndSetIfChanged(ref _flightStatusIcon, value); }
        }

        /// <summary>
        /// The color of the flight status.
        /// </summary>
        string _flightStatusColor;

        /// <summary>
        /// Gets or sets the color of the flight status.
        /// </summary>
        /// <value>The color of the flight status.</value>
        public string FlightStatusColor
        {
            get { return _flightStatusColor; }
            set { this.RaiseAndSetIfChanged(ref _flightStatusColor, value); }
        }

        /// <summary>
        /// The gate.
        /// </summary>
        int _gate;

        /// <summary>
        /// Gets or sets the gate.
        /// </summary>
        /// <value>The gate.</value>
        public int Gate
        {
            get { return _gate; }
            set { this.RaiseAndSetIfChanged(ref _gate, value); }
        }

        /// <summary>
        /// The zone.
        /// </summary>
        int _zone;

        /// <summary>
        /// Gets or sets the zone.
        /// </summary>
        /// <value>The zone.</value>
        public int Zone
        {
            get { return _zone; }
            set { this.RaiseAndSetIfChanged(ref _zone, value); }
        }

        /// <summary>
        /// The number of passengers.
        /// </summary>
        int _numberOfPassengers;

        /// <summary>
        /// Gets or sets the number of passengers.
        /// </summary>
        /// <value>The number of passengers.</value>
        public int NumberOfPassengers
        {
            get { return _numberOfPassengers; }
            set { this.RaiseAndSetIfChanged(ref _numberOfPassengers, value); }
        }

        /// <summary>
        /// Gets the duration.
        /// </summary>
        /// <value>The duration.</value>
        public string Duration
        {
            get
            {
                var duration = ArrivalTime - DepartureTime;
                return duration.ToString("%h") + "h " + duration.ToString("%m") + "m";
            }
        }

        /// <summary>
        /// The total bags on first flight.
        /// </summary>
        int _totalBagsOnFlight;

        /// <summary>
        /// Gets or sets the total bags on first flight.
        /// </summary>
        /// <value>The total bags on first flight.</value>
        public int TotalBagsOnFlight
        {
            get { return _totalBagsOnFlight; }
            set { this.RaiseAndSetIfChanged(ref _totalBagsOnFlight, value); }
        }

        /// <summary>
        /// The flight selected command.
        /// </summary>
        Command _bagSelectedCommand;

        /// <summary>
        /// Gets the flight selected command.
        /// </summary>
        /// <value>The flight selected command.</value>
        public Command BagSelectedCommand
        {
            get => _bagSelectedCommand ?? (_bagSelectedCommand = new Command<BaggageItem>((obj) => GoToPage(PageNames.BagDetailsPage,
                                                         new NavigationParameters()
            {
                {"bag", obj}
            })));
        }

        #endregion

        /// <summary>
        /// Gets or sets the bags for flight.
        /// </summary>
        /// <value>The bags for flight.</value>
        public ObservableCollection<BaggageItem> BagsForFlight { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ContosoBaggage.ViewModels.FlightDetailsViewModel"/> class.
        /// </summary>
        public FlightDetailsViewModel()
        {
            BagsForFlight = new ObservableCollection<BaggageItem>();
        }

        /// <summary>
        /// Gets the bag count.
        /// </summary>
        /// <value>The bag count.</value>
        public int BagCount 
        {
            get => BagsForFlight.Count;
        }

        /// <summary>
        /// The get bags for flight command.
        /// </summary>
        Command _getBagsForFlightCommand;

        /// <summary>
        /// Gets the get flights command.
        /// </summary>
        /// <value>The get flights command.</value>
        public Command GetBagsForFlightCommand
        {
            get => _getBagsForFlightCommand ??
                    (_getBagsForFlightCommand = new Command(async () => await ExecuteBagsForFlightCommand(), 
                                                            () => { return !IsBusy; }));
        }

        /// <summary>
        /// Executes the bags for flight command.
        /// </summary>
        /// <returns>The bags for flight command.</returns>
        private async Task ExecuteBagsForFlightCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            GetBagsForFlightCommand.ChangeCanExecute();
            try
            {
                BagsForFlight.Clear();

                var bagsForFlight = await FlightService.GetBagsForFlight(FlightNumber);
                foreach (var bag in bagsForFlight)
                {
                    BagsForFlight.Add(bag);
                }

                TotalBagsOnFlight = BagsForFlight.Count;
            }
            catch (Exception ex)
            {
                //_page.DisplayAlert("Uh Oh :(", "Unable to retrieve bags for flight.", "OK");
                Analytics.TrackEvent("Exception", new Dictionary<string, string> {
                    { "Message", ex.Message },
                    { "StackTrace", ex.ToString() }
                });
            }
            finally
            {
                IsBusy = false;
                GetBagsForFlightCommand.ChangeCanExecute();
            }
        }

        /// <summary>
        /// Loads the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="parameters">Parameters.</param>
        public override async Task LoadAsync(Navigation.NavigationParameters parameters)
        {
            if (parameters.Contains("flight"))
            {
                var flight = (Flight)parameters.Get("flight");
                Apply(flight);
                // get bag list
                await ExecuteBagsForFlightCommand();
            }
        }

        /// <summary>
        /// Apply the specified flight.
        /// </summary>
        /// <returns>The apply.</returns>
        /// <param name="flight">Flight.</param>
        void Apply(Flight flight)
        {
            Title = "Flight No: " + flight.FlightNumber;

            FlightNumber = flight.FlightNumber;
            FlightDate = flight.FlightDate;
            ArrivalAirportCode = flight.ArrivalAirportCode;
            ArrivalCity = flight.ArrivalCity;
            ArrivalTime = flight.ArrivalTime;
            DepartureAirportCode = flight.DepartureAirportCode;
            DepartureCity = flight.DepartureCity;
            DepartureTime = flight.DepartureTime;
            BoardingTime = flight.BoardingTime;
            Airline = flight.Airline;

            switch (flight.FlightStatus)
            {
                // awaiting take off
                case "ATO":
                    FlightStatus = "AWAITING TAKE OFF";
                    FlightStatusIcon = "awaiting-take-off.svg";
                    FlightStatusColor = "#1d7223";
                    break;
                // boarding
                case "BOA":
                    FlightStatus = "BOARDING";
                    FlightStatusIcon = "boarding.svg";
                    FlightStatusColor = "#B00000";
                    break;
                // At gate
                case "AG":
                default:
                    FlightStatus = "AT GATE";
                    FlightStatusIcon = "gate.svg";
                    FlightStatusColor = "#77767b";
                    break;
            }

            Gate = flight.Gate;
            Zone = flight.Zone;
            NumberOfPassengers = flight.NumberOfPassengers;
        }
    }
}