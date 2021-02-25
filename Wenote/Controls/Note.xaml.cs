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
using Wenote.ViewModels;

namespace Wenote.Controls {
    /// <summary>
    /// Note.xaml 的交互逻辑
    /// </summary>
    public partial class Note : UserControl {
        public Note() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            var viewWindow = new NoteView {
                DataContext = new NoteViewViewModel {
                    NoteMd = (this.DataContext as NoteViewModel).Note.Content,
                    NoteTitle = (this.DataContext as NoteViewModel).Note.Title
                }
            };
            viewWindow.ShowDialog();
        }
    }
}
