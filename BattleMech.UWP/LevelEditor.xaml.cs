using BattleMech.PCL.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BattleMech.UWP {
    public sealed partial class LevelEditor : BasePage {
        private LevelEditorModel viewModel => (LevelEditorModel)DataContext;

        public LevelEditor() {
            InitializeComponent();

            DataContext = new LevelEditorModel(App.Token);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e) {
            await viewModel.LoadData();
        }

        private void btnBack_OnClick(object sender, RoutedEventArgs e) { Frame.GoBack(); }

        private async void btnSavePublish_OnClick(object sender, RoutedEventArgs e) {
            var result = await viewModel.Save();

            if (result) {
                ShowDialog("Level published successfully");
            } else {
                ShowDialog("Level was not saved");
            }
        }

        private void btnAddAsset_OnClick(object sender, RoutedEventArgs e) { viewModel.AddItem(); }

        private void btnLevelInfo_OnClick(object sender, RoutedEventArgs e) {
            pLevelInfo.HorizontalOffset = (Window.Current.Bounds.Width - gLevelInfo.Width) / 2;
            pLevelInfo.VerticalOffset = (Window.Current.Bounds.Height - gLevelInfo.Height) / 2;

            pLevelInfo.IsOpen = true;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e) {
            pLevelInfo.IsOpen = false;
        }
    }
}