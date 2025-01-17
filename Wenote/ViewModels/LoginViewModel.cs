﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wenote.ViewModels {
    internal class LoginViewModel : ViewModelBase {
        private string _username;
        private string _password;

        public string Username {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Password {
            get => _password;
            set => SetProperty(ref _password, value);
        }
    }
}
