using Prism;
using Prism.Ioc;
using OnlineBankingPrism.ViewModels;
using OnlineBankingPrism.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace OnlineBankingPrism
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainTabPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage,LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<ExchangeRatesPage, ExchangeRatesPageViewModel>();
            containerRegistry.RegisterForNavigation<MainTabPage, MainTabPageViewModel>();
            containerRegistry.RegisterForNavigation<ReplenishmentPage, ReplenishmentPageViewModel>();
            containerRegistry.RegisterForNavigation<TransferPage, TransferPageViewModel>();
        }
    }
}
