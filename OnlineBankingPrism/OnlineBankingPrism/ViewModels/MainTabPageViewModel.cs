using Prism.Navigation;

namespace OnlineBankingPrism.ViewModels
{
	public class MainTabPageViewModel : ViewModelBase
	{
        public MainTabPageViewModel(INavigationService navigationService) :base(navigationService)
        {
            Constants.NavigationService.NavigationServiceInstance = navigationService;
        }
        
    }
}
