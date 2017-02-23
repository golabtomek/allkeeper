using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace allkeeper.ViewModel
{
    public class NoteConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Any(e => e == DependencyProperty.UnsetValue))
            {
                return new Model.Note("","");
            }
            else
            {
                string title = (string)values[0];
                string content = (string)values[1];
                if (!string.IsNullOrWhiteSpace(title) && !string.IsNullOrWhiteSpace(content))
                    return new Model.Note(title, content);
                else return new Model.Note("", "");
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
