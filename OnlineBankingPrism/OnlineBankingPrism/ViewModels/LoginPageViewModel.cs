using System;
using OnlineBankingPrism.Constants;
using OnlineBankingPrism.Services;
using Prism.Commands;
using Prism.Navigation;

namespace OnlineBankingPrism.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public LoginPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Sign In";
            SignInCommand = new DelegateCommand(SignIn);
        }
        public String Login { get; set; }
        public String Password { get; set; }
        public async void SignIn()
        {
            if (await AuthorizationService.Authorize(Login, Password))
                NavigateToMainPage();
        }

        public async void NavigateToMainPage()
        {
            await NavigationService.NavigateAsync(PageNames.MainTabPage);
        }
        public DelegateCommand SignInCommand { get; }
    }
}
