using System.Windows;

namespace BattleMech.WPF {
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
        }   

        private void miExit_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }
    }
}