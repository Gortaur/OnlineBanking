using System.Collections.Generic;
using System.Collections.ObjectModel;
using OnlineBankingPrism.Constants;
using OnlineBankingPrism.Models;
using Prism.Commands;
using Prism.Navigation;

namespace OnlineBankingPrism.ViewModels
{
	public class ExchangeRatesPageViewModel : ViewModelBase
	{
        public ExchangeRatesPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            SetExchangeRates();
            RefreshCommand = new DelegateCommand(OnRefresh);
        }
        public ObservableCollection<ExchangeRateModel> ExchangeRatesCollection { get; set; }

	    private void SetExchangeRates()
	    {
	        var rates = new List<ExchangeRateModel>();
	        foreach (var rate in SharedApplicationData.ExchangeRates)
	        {
	            rates.Add(new ExchangeRateModel(rate));
	        }
	        ExchangeRatesCollection = new ObservableCollection<ExchangeRateModel>(rates);
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
	        SetExchangeRates();
	            
            IsRefreshing = false;
	    }

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
