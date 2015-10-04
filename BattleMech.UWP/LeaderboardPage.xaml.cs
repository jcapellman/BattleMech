using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BattleMech.UWP {
    public sealed partial class LeaderboardPage : Page {
        public LeaderboardPage() {
            this.InitializeComponent();
        }

        private void btnBack_OnClick(object sender, RoutedEventArgs e) { Frame.GoBack(); }
    }
}