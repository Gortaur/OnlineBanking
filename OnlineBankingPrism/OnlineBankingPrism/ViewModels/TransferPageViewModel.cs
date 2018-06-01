using System;
using System.Collections.ObjectModel;
using System.Linq;
using OnlineBankingPrism.SharedEntities.Entities;
using OnlineBankingPrism.SharedEntities.Enums;
using Prism.Commands;
using Prism.Navigation;

namespace OnlineBankingPrism.ViewModels
{
    public class TransferPageViewModel : ViewModelBase
    {
        public TransferPageViewModel(INavigationService navigationService) : base(navigationService)
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
                TransactionType = TransactionTypes.Transfer
            };
            if (!VerifyTransferTransactionDestination(transaction))
            {
                return;
            }

           await RequestManager.RequestManager.PostTransfer(transaction);
        }

        private Boolean VerifyTransferTransactionDestination(Transaction transaction)
            => transaction.Destination.Length == 16 && transaction.Destination.All(char.IsDigit);
    }
}