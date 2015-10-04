using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BattleMech.UWP {
    public sealed partial class OptionsPage : Page {
        public OptionsPage() {
            this.InitializeComponent();
        }

        private void btnBack_OnClick(object sender, RoutedEventArgs e) { Frame.GoBack(); }

        private void btnSave_OnClick(object sender, RoutedEventArgs e) { Frame.GoBack(); }
    }
}