using BattleMech.PCL.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BattleMech.UWP {
    public sealed partial class LevelEditor : Page {
        private LevelEditorModel viewModel => (LevelEditorModel)DataContext;

        public LevelEditor() {
            InitializeComponent();

            DataContext = new LevelEditorModel(App.Token);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e) {
            await viewModel.LoadData();
        }

        private void btnBack_OnClick(object sender, RoutedEventArgs e) { Frame.GoBack(); }

        private void btnSavePublish_OnClick(object sender, RoutedEventArgs e) { Frame.GoBack(); }

        private void btnAddAsset_OnClick(object sender, RoutedEventArgs e) { viewModel.AddItem(); }
    }
}