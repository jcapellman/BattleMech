using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BattleMech.UWP {
    public sealed partial class CustomGamePage : Page {
        public CustomGamePage() {
            this.InitializeComponent();
        }

        private void btnBack_OnClick(object sender, RoutedEventArgs e) { Frame.GoBack(); }

        private void btnLaunch_OnClick(object sender, RoutedEventArgs e) { Frame.GoBack(); }
    }
}