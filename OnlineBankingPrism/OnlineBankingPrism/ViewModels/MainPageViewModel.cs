using OnlineBankingPrism.Constants;
using Prism.Commands;
using Prism.Navigation;

namespace OnlineBankingPrism.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService) 
            : base (navigationService)
        {
            Title = "Main Page";
            NavigateToReplenishmentPageCommand = new DelegateCommand(NavigateToReplenishmentPage);
            NavigateToTransferPageCommand = new DelegateCommand(NavigateToTransferPage);
        }
        public async void NavigateToTransferPage()
        {
            await NavigationService.NavigateAsync(PageNames.ReplenishmentPage);
        }      
        public async void NavigateToReplenishmentPage()
        {
            await NavigationService.NavigateAsync(PageNames.ReplenishmentPage);
        }
        public DelegateCommand NavigateToReplenishmentPageCommand { get; }
        public DelegateCommand NavigateToTransferPageCommand { get; }
    }
}
