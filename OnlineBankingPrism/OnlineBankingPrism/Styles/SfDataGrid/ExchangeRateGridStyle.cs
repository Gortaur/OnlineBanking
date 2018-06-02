using Syncfusion.SfDataGrid.XForms;
using Xamarin.Forms;

namespace OnlineBankingPrism.Styles.SfDataGrid
{
    class ExchangeRateGridStyle : DataGridStyle
    {
        public override Color GetBorderColor()
        {
            return Color.Green;
        }

        public override Color GetAlternatingRowBackgroundColor()
        {
            return Color.LightBlue;
        }
    }
}
