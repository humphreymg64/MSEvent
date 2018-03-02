﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

using ContosoBaggage.Common.Models;
using ContosoBaggage.Controls;
using ContosoBaggage.Models;
using ContosoBaggage.ViewModels;

namespace ContosoBaggage.Pages
{
    public partial class FlightDetailsPage : BaseContentPage<FlightDetailsViewModel>, 
        INavigableXamarinFormsPage
    {
        public FlightDetailsPage()
        {
            InitializeComponent();
        }
    }
}