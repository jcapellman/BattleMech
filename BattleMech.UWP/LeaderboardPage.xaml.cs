using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

using BattleMech.PCL.ViewModels;

namespace BattleMech.UWP {
    public sealed partial class LeaderboardPage {
        private LeaderboardModel viewModel => (LeaderboardModel) DataContext;

        public LeaderboardPage() {
            this.InitializeComponent();

            DataContext = new LeaderboardModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e) { await viewModel.LoadData(); }

        private void btnBack_OnClick(object sender, RoutedEventArgs e) { Frame.GoBack(); }
    }
}