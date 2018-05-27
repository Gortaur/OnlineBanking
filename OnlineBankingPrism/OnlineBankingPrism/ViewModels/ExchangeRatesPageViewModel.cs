using System.Collections.ObjectModel;
using OnlineBankingPrism.Constants;
using OnlineBankingPrism.SharedEntities.Entities;
using Prism.Navigation;

namespace OnlineBankingPrism.ViewModels
{
	public class ExchangeRatesPageViewModel : ViewModelBase
	{
        public ExchangeRatesPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Exchange rates";
            ExchangeRatesCollection= new ObservableCollection<NbuExchangeRates>(SharedApplicationData.ExchangeRates);
        }
        public ObservableCollection<NbuExchangeRates> ExchangeRatesCollection { get; set; }
    }
}
