using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Wenote.Core.Models;

namespace Wenote {
    internal static class Store {
        private static User _user;
        private static bool _isLoggedIn;

        public static bool IsLoggedIn {
            get => _isLoggedIn;
            set {
                _isLoggedIn = value;
                LoginStateChanged?.Invoke(null, null);
            }
        }
        public static User LoggedInUser {
            get => _user;
            set {
                _user = value;
                IsLoggedIn = value != null;
                LoginStateChanged?.Invoke(null, null);
            }
        }
        public static string Jwt => LoggedInUser is null ? "" : LoggedInUser.Token;

        public static void Logout() {
            _isLoggedIn = false;
            _user = null;
            LoginStateChanged(null, null);
        }

        public static event EventHandler LoginStateChanged;
    }
}
