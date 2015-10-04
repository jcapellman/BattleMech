using Windows.UI.Xaml.Controls;

using BattleMech.PCL.ViewModels;

namespace BattleMech.UWP {
    public sealed partial class MainMenuPage : Page {
        public MainMenuPage() {
            this.InitializeComponent();

            DataContext = new MainMenuModel();
        }
    }
}