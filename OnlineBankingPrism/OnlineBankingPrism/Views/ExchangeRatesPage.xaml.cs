using OnlineBankingPrism.Styles.SfDataGrid;
using Syncfusion.SfDataGrid.XForms;
using Xamarin.Forms;

namespace OnlineBankingPrism.Views
{
    public partial class ExchangeRatesPage : ContentPage
    {
        public ExchangeRatesPage()
        {
            InitializeComponent();
            DataGrid.GridStyle = new  ExchangeRateGridStyle();
            GridTextColumn orderIdColumn = new GridTextColumn
            {
                MappingName = "BaseCurrency",
                HeaderText = "Currency",
                CellTextSize = 18,
                HeaderCellTextSize = 19,
                HeaderFontAttribute = FontAttributes.Bold
            };

            GridTextColumn customerIdColumn = new GridTextColumn
            {
                MappingName = "ConvertToCurrency",
                HeaderText = "Exchange",
                CellTextSize = 18,
                HeaderCellTextSize = 19,
                HeaderFontAttribute = FontAttributes.Bold
            };

            GridTextColumn countryColumn = new GridTextColumn
            {
                MappingName = "ExchangeRate",
                HeaderText = "Exchange Rate",
                CellTextSize = 18,
                HeaderCellTextSize = 19,
                HeaderFontAttribute = FontAttributes.Bold
            };


            DataGrid.Columns.Add(orderIdColumn);
            DataGrid.Columns.Add(customerIdColumn);
            DataGrid.Columns.Add(countryColumn);
        }
    }
}
