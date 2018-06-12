using System;
using System.Collections.ObjectModel;
using System.Linq;
using OnlineBankingPrism.Constants;
using OnlineBankingPrism.SharedEntities.Entities;
using OnlineBankingPrism.SharedEntities.Enums;
using Prism.Commands;
using Prism.Navigation;

namespace OnlineBankingPrism.ViewModels
{
	public class ReplenishmentPageViewModel : ViewModelBase
	{
        public ReplenishmentPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            var cardNumberList = SharedApplicationData.CurrentUser.Cards.Select(item => item.Id).ToList();
            SubmitTransactionCommand = new DelegateCommand(SubmitTransaction);
            UserCards = new ObservableCollection<string>(cardNumberList);
        }
	    public DelegateCommand SubmitTransactionCommand { get; }
	    public String SelectedCard { get; set; }
	    public String TransactionSumm { get; set; }
	    public String ReceiverCardNumber { get; set; }
	    public ObservableCollection<String> UserCards { get; set; }

	    public async void SubmitTransaction()
	    {
	        IsBusy = true;
	        if (!Double.TryParse(TransactionSumm, out var summ) || String.IsNullOrEmpty(ReceiverCardNumber))
	        {
	            return;
	        }

	        var transaction = new Transaction
	        {
	            Date = DateTime.Now,
	            Destination = ReceiverCardNumber,
	            SourceCard = SelectedCard,
	            TransactionSum = summ,
                TransactionType = TransactionTypes.Replenishment
	        };
	        if (!VerifyReplenishmentTransactionDestination(transaction))
	        {
	            IsBusy = false;
                return;
	        }

	        if (await RequestManager.RequestManager.PostTransfer(transaction))
	        {
	            var user = await RequestManager.RequestManager.GetAuthenticatedUser(SharedApplicationData.CurrentUser.Login);
	            if (user != null)
	            {
	                SharedApplicationData.CurrentUser = user;
	            }
	        }

            await NavigationService.NavigateAsync($"{PageNames.MainTabPage}");
	        IsBusy = false;
        }

	    private Boolean VerifyReplenishmentTransactionDestination(Transaction transaction)
	    {
	        if (transaction.Destination.Length != 13)
	        {
	            return false;
	        }

	        if (transaction.Destination[0] != '+')
	        {
	            return false;
	        }

	        var substring = transaction.Destination.Substring(1, transaction.Destination.Length - 1);

	        return substring.All(char.IsDigit);
	    }

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
