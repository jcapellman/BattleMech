using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using BattleMech.PCL.ViewModels;

namespace BattleMech.UWP {
    public sealed partial class CharacterProfilePage : Page {
        private CharacterProfileModel viewModel => (CharacterProfileModel) DataContext;

        public CharacterProfilePage() {
            this.InitializeComponent();

            DataContext = new CharacterProfileModel(App.Token);
        }
        
        protected override async void OnNavigatedTo(NavigationEventArgs e) { await viewModel.LoadData(); }

        private void btnBack_OnClick(object sender, RoutedEventArgs e) { Frame.GoBack(); }
    }
}