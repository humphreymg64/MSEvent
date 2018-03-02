using System;
using System.Collections.Generic;

using Xamarin.Forms;

using ContosoBaggage.ViewModels;
using ContosoBaggage.Controls;
using ContosoBaggage.Navigation;

namespace ContosoBaggage.Pages
{
    public partial class MainPage : BaseContentPage<MainViewModel>, INavigableXamarinFormsPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }
}