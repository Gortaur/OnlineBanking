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
            var cardNumberList = Constants.SharedApplicationData.CurrentUser.Cards.Select(item => item.Id).ToList();
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
	            return;
	        }

	        await RequestManager.RequestManager.PostTransfer(transaction);
	        await NavigationService.NavigateAsync($"CustomNavigationPage/{PageNames.MainTabPage}");
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
    }
}
