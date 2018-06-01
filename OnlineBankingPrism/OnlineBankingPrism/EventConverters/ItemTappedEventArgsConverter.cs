using System;
using System.Globalization;
using Xamarin.Forms;

namespace OnlineBankingPrism.EventConverters
{
    class ItemSelectedEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is SelectedItemChangedEventArgs eventArgs))
                throw new ArgumentException("Expected TappedEventArgs as value", nameof(value));

            return eventArgs.SelectedItem;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
