using BattleMech.WPF.ViewModels;

using System.Windows;

namespace BattleMech.WPF {
    public partial class MainWindow {
        MainModel viewModel => (MainModel)DataContext;

        public MainWindow() {
            InitializeComponent();

            Loaded += OnLoaded;
            DataContext = new MainModel(App.Token);
        }

        private async void OnLoaded(object sender, RoutedEventArgs e) {
            await viewModel.LoadData();
        }
        
        private void miExit_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e) {
            var result = await viewModel.AttemptLogin();

            if (!result) {
                MessageBox.Show("Invalid email or passsword");

                return;
            }

            App.Token = viewModel.Token;

            viewModel.Username = string.Empty;
            viewModel.Password = string.Empty;

            pLogin.IsOpen = false;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            pLogin.IsOpen = false;

            viewModel.Username = string.Empty;
            viewModel.Password = string.Empty;
        }

        private void miLogin_Click(object sender, RoutedEventArgs e) {
            pLogin.IsOpen = true;
        }
    }
}