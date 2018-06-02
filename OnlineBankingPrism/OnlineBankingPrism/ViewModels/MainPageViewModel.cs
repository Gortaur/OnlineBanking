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
            : base(navigationService)
        {
            Title = "Main Page";
            NavigateToReplenishmentPageCommand = new DelegateCommand(NavigateToReplenishmentPage);
            NavigateToTransferPageCommand = new DelegateCommand(NavigateToTransferPage);

            SetCardList();
            OnCardSelectedCommand = new DelegateCommand(OnCardSelected);
            RefreshCommand = new DelegateCommand(OnRefresh);
        }

        private void SetCardList()
        {
            var cards = new List<CardModel>();
            foreach (var card in SharedApplicationData.CurrentUser.Cards)
            {
                cards.Add(new CardModel(card));
            }

            Cards = new List<CardModel>(cards);
        }
        public DelegateCommand OnCardSelectedCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }

        public async void OnRefresh()
        {
            IsRefreshing = true;
            var user = await RequestManager.RequestManager.GetAuthenticatedUser(SharedApplicationData.CurrentUser
                .Login);
            if (user == null)
            {
                IsRefreshing = false;
                return;
            }

            SharedApplicationData.CurrentUser = user;
            SetCardList();
            IsRefreshing = false;
        }

        public async void OnCardSelected()
        {
            SharedApplicationData.SelectedCard =
                SharedApplicationData.CurrentUser.Cards.First(item => item.Id == SelectedItem.Id);
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

        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                RaisePropertyChanged(nameof(IsRefreshing));
            }
        }
    }
}