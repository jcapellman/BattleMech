using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using BattleMech.PCL.ViewModels;
using BattleMech.UWP.PSI;

namespace BattleMech.UWP {
    public sealed partial class MainMenuPage : BasePage {

        private MainMenuModel viewModel => (MainMenuModel)DataContext;

        public MainMenuPage() {
            InitializeComponent();

            DataContext = new MainMenuModel(App.Token, new SettingsPSI(), new LFSPSI());
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e) {
            var result = await viewModel.LoadData();

            if (result == null) {
                return;
            }

            App.Token = result.Token;
            
            viewModel.Username = string.Empty;
            viewModel.Password = string.Empty;

            if (App.Assets == null) {
                App.Assets = await viewModel.LoadAssetData();
            }

            App.PlayerAsset = App.Assets.FirstOrDefault(a => a.ID == result.PlayerAssetID);

            pLogin.IsOpen = false;
        }

        private async void btnNewGame_OnClick(object sender, RoutedEventArgs e) {
            var result = await viewModel.GenerateLevel(App.Assets);

            App.CurrentLevel = result;

            App.Game = new GamePage();
            
            Window.Current.Content = App.Game;
        }

        private void btnCustomGame_OnCLick(object sender, RoutedEventArgs e) { Frame.Navigate(typeof (CustomGamePage)); }

        private void btnCharacterProfile_OnClick(object sender, RoutedEventArgs e) { Frame.Navigate(typeof (CharacterProfilePage)); }

        private void btnOptions_OnClick(object sender, RoutedEventArgs e) { Frame.Navigate(typeof (OptionsPage)); }

        private void btnCredits_OnClick(object sender, RoutedEventArgs e) { Frame.Navigate(typeof (CreditsPage)); }

        private void btnLevelEditor_OnClick(object sender, RoutedEventArgs e) { Frame.Navigate(typeof(LevelEditor)); }

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

            if (result == null) {
                ShowDialog("Invalid email or passsword");

                return;
            }

            App.Token = result.Token;
            
            viewModel.Username = string.Empty;
            viewModel.Password = string.Empty;

            pLogin.IsOpen = false;

            if (App.Assets == null) {
                App.Assets = await viewModel.LoadAssetData();
            }

            App.PlayerAsset = App.Assets.FirstOrDefault(a => a.ID == result.PlayerAssetID);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            pLogin.IsOpen = false;

            viewModel.Username = string.Empty;
            viewModel.Password = string.Empty;
        }

        private void btnCancelCreateAccount_Click(object sender, RoutedEventArgs e) {
            pCreateAccount.IsOpen = false;

            viewModel.ClearCreateAccountForm();
        }

        private void btnCreateAccount_OnClick(object sender, RoutedEventArgs e) {
            pCreateAccount.HorizontalOffset = (Window.Current.Bounds.Width - gCreateAccount.Width) / 2;
            pCreateAccount.VerticalOffset = (Window.Current.Bounds.Height - gCreateAccount.Height) / 2;

            pCreateAccount.IsOpen = true;
        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e) {
            var result = await viewModel.AttemptCreateAccount();

            if (!result) {
                ShowDialog("Email already in use");

                return;
            }

            viewModel.Username = viewModel.caUsername;
            viewModel.Password = viewModel.caPassword;

            var loginResult = await viewModel.AttemptLogin();

            App.Token = loginResult.Token;

            if (App.Assets == null) {
                App.Assets = await viewModel.LoadAssetData();
            }

            App.PlayerAsset = App.Assets.FirstOrDefault(a => a.ID == loginResult.PlayerAssetID);

            pCreateAccount.IsOpen = false;

            viewModel.ClearCreateAccountForm();
        }
    }
}