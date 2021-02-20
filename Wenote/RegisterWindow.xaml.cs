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
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using Wenote.Core;
using Wenote.ViewModels;

namespace Wenote {
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterWindow : Window {
        public RegisterWindow() {
            InitializeComponent();
            this.DataContext = new LoginViewModel();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e) {
            (this.DataContext as LoginViewModel).Password = PasswordBox.Password;
            this.LoginButton.IsEnabled = !string.IsNullOrWhiteSpace(PasswordBox.Password) &&
                                         !string.IsNullOrWhiteSpace(UsernameBox.Text);
        }

        private void UsernameBox_TextChanged(object sender, TextChangedEventArgs e) {
            this.LoginButton.IsEnabled = !string.IsNullOrWhiteSpace(PasswordBox.Password) &&
                                         !string.IsNullOrWhiteSpace(UsernameBox.Text);
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e) {
            LoginButton.SetValue(ButtonProgressAssist.IsIndeterminateProperty, true);
            LoginButton.IsEnabled = false;

            try {
                Store.LoggedInUser = await Actions.LoginAsync(this.UsernameBox.Text, this.PasswordBox.Password);
                this.DialogResult = true;
                this.Close();
            } catch (Exception ex) {
                var msg = ex.Message;
                MsgSnackbar.MessageQueue.Enqueue(msg, "知道了", () => MsgSnackbar.IsActive = false);
            }
            
            LoginButton.SetValue(ButtonProgressAssist.IsIndeterminateProperty, false);
            LoginButton.IsEnabled = true;
        }
    }
}
