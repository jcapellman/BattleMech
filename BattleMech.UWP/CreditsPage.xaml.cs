using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BattleMech.UWP {
    public sealed partial class CreditsPage : Page {
        public CreditsPage() {
            this.InitializeComponent();
        }

        private void btnBack_OnClick(object sender, RoutedEventArgs e) { Frame.GoBack(); }
    }
}