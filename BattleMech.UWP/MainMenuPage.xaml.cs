using System;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Popups;

using BattleMech.PCL.ViewModels;
using Windows.UI.Xaml.Navigation;

namespace BattleMech.UWP {
    public sealed partial class MainMenuPage : Page {
        public async void ShowDialog(string messageText, string title = "Battlemech") {
            var dialog = new MessageDialog(messageText, title);

            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => {
                await dialog.ShowAsync();
            });
        }

        private MainMenuModel viewModel => (MainMenuModel)DataContext;

        public MainMenuPage() {
            InitializeComponent();

            DataContext = new MainMenuModel(App.Token);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) { viewModel.LoadData(); }

        private void btnNewGame_OnClick(object sender, RoutedEventArgs e) {
            App.Game = new GamePage();
            
            Window.Current.Content = App.Game;
        }

        private void btnCustomGame_OnCLick(object sender, RoutedEventArgs e) { Frame.Navigate(typeof (CustomGamePage)); }

        private void btnCharacterProfile_OnClick(object sender, RoutedEventArgs e) { Frame.Navigate(typeof (CharacterProfilePage)); }

        private void btnOptions_OnClick(object sender, RoutedEventArgs e) { Frame.Navigate(typeof (OptionsPage)); }

        private void btnCredits_OnClick(object sender, RoutedEventArgs e) { Frame.Navigate(typeof (CreditsPage)); }

        private void btnLeaderboard_OnClick(object sender, RoutedEventArgs e) { Frame.Navigate(typeof (LeaderboardPage)); }

        private void btnLoginLogout_OnClick(object sender, RoutedEventArgs e) {
            if (viewModel.IsLoggedIn) {
                viewModel.Logout();

                App.Token = string.Empty;
            } else {
                pLogin.HorizontalOffset = (Window.Current.Bounds.Width - gLogin.Width) / 2;
                pLogin.VerticalOffset = (Window.Current.Bounds.Height - gLogin.Height) / 2;

                pLogin.IsOpen = true;
            }
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e) {
            var result = await viewModel.AttemptLogin();

            if (!result) {
                ShowDialog("Invalid email or passsword");

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
    }
}