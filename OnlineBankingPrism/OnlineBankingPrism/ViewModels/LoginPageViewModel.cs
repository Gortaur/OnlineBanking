using System;
using OnlineBankingPrism.Constants;
using OnlineBankingPrism.Services;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace OnlineBankingPrism.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public LoginPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Sign In";
            SignInCommand = new DelegateCommand(SignIn);
        }

        public String Login { get; set; } = "user";
        public String Password { get; set; }
        public async void SignIn()
        {
            IsBusy = true;
            if (await AuthorizationService.Authorize(Login, Password))
            {
                NavigateToMainPage();
                IsBusy = false;
                return;
            }
            MessagingCenter.Send(this, "Error");
            IsBusy = false;
        }

        public async void NavigateToMainPage()
        {
            await NavigationService.NavigateAsync(PageNames.MainTabPage);
        }
        public DelegateCommand SignInCommand { get; }

        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                RaisePropertyChanged(nameof(IsBusy));
            }
        }
    }
}
