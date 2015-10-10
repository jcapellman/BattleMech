using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

using BattleMech.PCL.ViewModels;

namespace BattleMech.UWP {
    public sealed partial class CustomGamePage : BasePage {
        private LevelModel viewModel => (LevelModel) DataContext;

        public CustomGamePage() {
            this.InitializeComponent();

            DataContext = new LevelModel(App.Token);            
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e) { await viewModel.LoadData(); }

        private void btnBack_OnClick(object sender, RoutedEventArgs e) { Frame.GoBack(); }

        private async void btnLaunch_OnClick(object sender, RoutedEventArgs e) {
            var result = await viewModel.LoadLevel(App.Assets);

            if (result == null) {
                ShowDialog("Could not load level");
                return;
            }

            App.CurrentLevel = result;

            App.Game = new GamePage();

            Window.Current.Content = App.Game;
        }
    }
}