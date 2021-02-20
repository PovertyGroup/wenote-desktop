using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Wenote.Core.Models;

namespace Wenote.ViewModels {
    internal class MainWindowViewModel : ViewModelBase {
        public MainWindowViewModel() {
            Store.LoginStateChanged += (_, _) => OnPropertyChanged(string.Empty);
        }

        public ImageSource AvatarImage => new BitmapImage(new Uri(Store.IsLoggedIn ? Store.LoggedInUser.AvatarUrl : "pack://application:,,,/Resources/default-avatar.png"));
        public string Username => Store.IsLoggedIn ? Store.LoggedInUser.Username : "游客";
        public bool ShowLogoutButton => Store.IsLoggedIn;
        public bool ShowLoginButton => !Store.IsLoggedIn;

        public List<Note> _recommendNotes;
        public List<Note> RecommendNotes {
            get => _recommendNotes;
            set => SetProperty(ref _recommendNotes, value);
        }

        public List<Note> _ownedNotes;
        public List<Note> OwnedNotes {
            get => _ownedNotes;
            set => SetProperty(ref _ownedNotes, value);
        }
    }
}
