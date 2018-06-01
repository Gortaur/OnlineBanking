using System.Collections.Generic;
using System.Linq;
using OnlineBankingPrism.Constants;
using OnlineBankingPrism.Models;
using OnlineBankingPrism.SharedEntities.Entities;
using Prism.Navigation;

namespace OnlineBankingPrism.ViewModels
{
	public class CardTransactionsPageViewModel : ViewModelBase
	{
        public CardTransactionsPageViewModel(INavigationService navigationService) : base(navigationService)
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

	    public Card SelectedItem { get; set; }
	    public List<TransactionModel> Transactions { get; set; }
    }
}
