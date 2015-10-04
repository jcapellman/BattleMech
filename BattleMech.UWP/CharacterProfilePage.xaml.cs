using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BattleMech.UWP {
    public sealed partial class CharacterProfilePage : Page {
        public CharacterProfilePage() {
            this.InitializeComponent();
        }

        private void btnBack_OnClick(object sender, RoutedEventArgs e) { Frame.GoBack(); }

        private void btnSave_OnClick(object sender, RoutedEventArgs e) { Frame.GoBack(); }
    }
}
