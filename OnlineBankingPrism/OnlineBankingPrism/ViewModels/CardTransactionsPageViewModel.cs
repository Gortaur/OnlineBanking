using System.Collections.Generic;
using System.Linq;
using OnlineBankingPrism.Constants;
using OnlineBankingPrism.Models;
using OnlineBankingPrism.SharedEntities.Entities;
using Prism.Commands;
using Prism.Navigation;

namespace OnlineBankingPrism.ViewModels
{
	public class CardTransactionsPageViewModel : ViewModelBase
	{
        public CardTransactionsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            SetTransactionsList();
            RefreshCommand = new DelegateCommand(OnRefresh);
        }
	    private void SetTransactionsList()
	    {
	        var transactions =
	            SharedApplicationData.CurrentUser.CardTransactions.FirstOrDefault(item =>
	                item.Id == SharedApplicationData.SelectedCard.Id)?.Transactions;
            if (transactions == null)
	        {
	            return;
	        }

	        Transactions = new List<TransactionModel>();
	        foreach (var transaction in transactions)
	        {
	            Transactions.Add(new TransactionModel(transaction));
	        }
            
        }
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
	        SetTransactionsList();
	        IsRefreshing = false;
	    }

        public Card SelectedItem { get; set; }
	    public List<TransactionModel> Transactions { get; set; }

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
