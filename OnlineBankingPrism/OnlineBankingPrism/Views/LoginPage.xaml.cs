using System;
using OnlineBankingPrism.ViewModels;
using Xamarin.Forms;

namespace OnlineBankingPrism.Views
{
    public partial class LoginPage
    {
        private int _wrongAttempts = 0;
        public LoginPage()
        {

            InitializeComponent();
            MessagingCenter.Subscribe<LoginPageViewModel>(this, "Error", (sender) =>
            {
                ShowAlert();
            });
        }

        private void ShowAlert()
        {
            if (_wrongAttempts <= 3)
            {
                DisplayAlert("Error", "Wrong login or password! Please, try again.", "Ok");
                _wrongAttempts++;
                return;
            }
            DisplayAlert("Error", "Too many attempts has been made, login option is blocked. Try again later", "Ok");
            LoginButton.IsEnabled = false;
        }
        private void OnLoginButtonClicked(object sender, EventArgs e)
        {

        }
    }
}
