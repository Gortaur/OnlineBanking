using System;
using System.Linq;
using OnlineBankingPrism.SharedEntities.Entities;
using Xamarin.Forms;

namespace OnlineBankingPrism.Views
{
    public partial class TransferPage : ContentPage
    {
        public TransferPage()
        {
            InitializeComponent();
        }

        private Boolean VerifyTransferTransactionDestination(String destination)
            => destination.Length == 16 && destination.All(char.IsDigit);

        private void SubmitButton_OnClicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(TransactionSummEditor.Text) ||
                !VerifyTransferTransactionDestination(ReceiverCardNumberEditor.Text))
            {
                DisplayAlert("Error", "Not all fields are filled correctly, please try again.", "OK");
            } 
        }
    }
}
