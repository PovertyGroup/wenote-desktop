using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wenote.Core;
using Wenote.Core.Models;
using Wenote.ViewModels;

namespace Wenote {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            // load owned notes
            Store.LoginStateChanged += (_, _) => {
                var dataContext = (MainWindowViewModel)this.DataContext;
                if (Store.IsLoggedIn) {
                    Task.Run(() => {
                        dataContext.OwnedNotes = Store.LoggedInUser.Notes.Select(id => Actions.GetNote(id)).ToList();
                    });
                } else {
                    dataContext.OwnedNotes = new List<Note>();
                }
            };
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e) {
            _ = new LoginWindow().ShowDialog();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e) {
            Store.Logout();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e) {
            var dataContext = (MainWindowViewModel)this.DataContext;
            Task.Run(async () => {
                dataContext.RecommendNotes = await Actions.GetRecommendNotesAsync();
            });
            
            if (Store.IsLoggedIn) {
                Task.Run(() => {
                    dataContext.OwnedNotes = Store.LoggedInUser.Notes.Select(id => Actions.GetNote(id)).ToList();
                });
            }
        }
    }
}
