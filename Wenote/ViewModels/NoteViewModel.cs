using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Humanizer;
using Wenote.Core.Models;

namespace Wenote.ViewModels {
    class NoteViewModel : ViewModelBase {
        private Note _note;
        public Note Note {
            get => _note;
            set {
                SetProperty(ref _note, value);
                OnPropertyChanged("DateStr");
            }
        }

        public string DateStr => Note.Date.Humanize();
    }
}
