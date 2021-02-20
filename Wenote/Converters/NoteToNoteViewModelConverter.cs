using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Wenote.Core.Models;
using Wenote.ViewModels;

namespace Wenote.Converters {
    class NoteToNoteViewModelConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return new NoteViewModel() {
                Note = value as Note,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return (value as NoteViewModel)?.Note;
        }
    }
}
