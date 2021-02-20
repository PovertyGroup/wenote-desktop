using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Wenote.Core.Models;

namespace Wenote {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private void App_OnExit(object sender, ExitEventArgs e) {
            // saving configuration

            using var file = File.Create("./config.json");
            JsonSerializer.Serialize(new Utf8JsonWriter(file), Store.LoggedInUser);
        }

        private void App_OnStartup(object sender, StartupEventArgs e) {
            if (File.Exists("./config.json")) {
                try {
                    using var file = File.OpenRead("./config.json");

                    var buffer = new byte[file.Length];
                    file.Read(buffer);
                    var user = JsonSerializer.Deserialize<User>(buffer);
                    Store.LoggedInUser = user;
                    if (!string.IsNullOrEmpty(Store.LoggedInUser?.Token)) {
                        Store.IsLoggedIn = true;
                    }
                } catch(Exception ex) {
                    // TODO: Notify reading config error
                }
            }
        }
    }
}
