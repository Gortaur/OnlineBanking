using System.Collections.Generic;
using System.Linq;
using OnlineBankingPrism.Constants;
using OnlineBankingPrism.Models;
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

            var cards = new List<CardModel>();
            foreach (var card in SharedApplicationData.CurrentUser.Cards)
            {
                cards.Add(new CardModel(card));
            }
            Cards = new List<CardModel>(cards);
            OnCardSelectedCommand = new DelegateCommand(OnCardSelected);
        }
        public DelegateCommand OnCardSelectedCommand { get; set; }

        public async void OnCardSelected()
        {
            SharedApplicationData.SelectedCard = SharedApplicationData.CurrentUser.Cards.First(item=>item.Id==SelectedItem.Id);
            await NavigationService.NavigateAsync(PageNames.CardTransactionsPage);
        }

        public CardModel SelectedItem { get; set; }
        public List<CardModel> Cards { get; set; }

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