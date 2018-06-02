using System;
using System.Linq;

namespace OnlineBankingPrism.Views
{
    public partial class ReplenishmentPage
    {
        public ReplenishmentPage()
        {
            InitializeComponent();
        }

        private Boolean VerifyReplenishmentTransactionDestination(string destination)
        {
            if (destination.Length != 13)
            {
                return false;
            }

            if (destination[0] != '+')
            {
                return false;
            }

            var substring = destination.Substring(1, destination.Length - 1);

            return substring.All(char.IsDigit);
        }

        private void SubmitButton_OnClicked(object sender, EventArgs e)
        {
            if (CardPicker.SelectedItem == null || String.IsNullOrEmpty(TransactionSummEditor.Text) ||
                !VerifyReplenishmentTransactionDestination(ReceiverCardNumberEditor.Text))
            {
                DisplayAlert("Error", "Not all fields are filled correctly, please try again.", "OK");
            }
        }
    }
}
