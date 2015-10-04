using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using BattleMech.PCL.ViewModels;

namespace BattleMech.UWP {
    public sealed partial class MainMenuPage : Page {
        public MainMenuPage() {
            this.InitializeComponent();

            DataContext = new MainMenuModel();
        }

        private void btnNewGame_OnClick(object sender, RoutedEventArgs e) { Frame.Navigate(typeof (MainGame)); }

        private void btnCustomGame_OnCLick(object sender, RoutedEventArgs e) { Frame.Navigate(typeof (CustomGamePage)); }

        private void btnCharacterProfile_OnClick(object sender, RoutedEventArgs e) { Frame.Navigate(typeof (CharacterProfilePage)); }

        private void btnOptions_OnClick(object sender, RoutedEventArgs e) { Frame.Navigate(typeof (OptionsPage)); }

        private void btnCredits_OnClick(object sender, RoutedEventArgs e) { Frame.Navigate(typeof (CreditsPage)); }

        private void btnLeaderboard_OnClick(object sender, RoutedEventArgs e) { Frame.Navigate(typeof (LeaderboardPage)); }
    }
}