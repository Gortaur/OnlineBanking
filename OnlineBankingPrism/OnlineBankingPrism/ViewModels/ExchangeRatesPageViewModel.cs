using System.Collections.Generic;
using System.Collections.ObjectModel;
using OnlineBankingPrism.Constants;
using OnlineBankingPrism.Models;
using OnlineBankingPrism.SharedEntities.Entities;
using Prism.Navigation;

namespace OnlineBankingPrism.ViewModels
{
	public class ExchangeRatesPageViewModel : ViewModelBase
	{
        public ExchangeRatesPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Exchange rates";
            var rates = new List<ExchangeRateModel>();
            foreach (var rate in SharedApplicationData.ExchangeRates)
            {
                rates.Add(new ExchangeRateModel(rate));
            }
            ExchangeRatesCollection= new ObservableCollection<ExchangeRateModel>(rates);
        }
        public ObservableCollection<ExchangeRateModel> ExchangeRatesCollection { get; set; }
    }
}
