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
    }
}