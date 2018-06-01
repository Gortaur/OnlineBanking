using System.Collections.Generic;
using OnlineBankingPrism.Constants;
using OnlineBankingPrism.SharedEntities.Entities;
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
            Cards = new List<Card>(SharedApplicationData.CurrentUser.Cards);
            OnCardSelectedCommand = new DelegateCommand(OnCardSelected);
        }
        public DelegateCommand OnCardSelectedCommand { get; set; }

        public async void OnCardSelected()
        {
            SharedApplicationData.SelectedCard = SelectedItem;
            await NavigationService.NavigateAsync(PageNames.CardTransactionsPage);
        }

        public Card SelectedItem { get; set; }
        public List<Card> Cards { get; set; }

        public async void NavigateToTransferPage()
        {
            await NavigationService.NavigateAsync(PageNames.TransferPage);
        }      
        public async void NavigateToReplenishmentPage()
        {
            await NavigationService.NavigateAsync(PageNames.ReplenishmentPage);
        }
        public DelegateCommand NavigateToReplenishmentPageCommand { get; }
        public DelegateCommand NavigateToTransferPageCommand { get; }
    }
}
