using System;
using OnlineBankingPrism.ViewModels;
using Xamarin.Forms;

namespace OnlineBankingPrism.Views
{
    public partial class MainTabPage
    {
        public MainTabPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void MainTabPage_OnCurrentPageChanged(object sender, EventArgs e)
        {
            var i = Children.IndexOf(CurrentPage);
            if (i == 0)
            {
                CurrentPage.BindingContext = new MainPageViewModel(Constants.NavigationService.NavigationServiceInstance);
            }
            else
            {
                CurrentPage.BindingContext = new ExchangeRatesPageViewModel(Constants.NavigationService.NavigationServiceInstance);
            }
        }
    }
}
