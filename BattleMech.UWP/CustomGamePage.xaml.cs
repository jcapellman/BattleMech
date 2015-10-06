using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

using BattleMech.PCL.ViewModels;

namespace BattleMech.UWP {
    public sealed partial class CustomGamePage {
        private LevelModel viewModel => (LevelModel) DataContext;

        public CustomGamePage() {
            this.InitializeComponent();

            DataContext = new LevelModel();            
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e) { await viewModel.LoadData(); }

        private void btnBack_OnClick(object sender, RoutedEventArgs e) { Frame.GoBack(); }

        private void btnLaunch_OnClick(object sender, RoutedEventArgs e) { Frame.GoBack(); }
    }
}