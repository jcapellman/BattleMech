using BattleMech.WebAPI.PCL.Handlers;
using BattleMech.DataLayer.PCL.Views.Assets;

using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;


namespace BattleMech.PCL.ViewModels {
    public class LevelEditorModel : BaseViewModel {
        public LevelEditorModel(string token) : base(token) { }

        private ActiveAssetTypes _selectedAssetType;

        public ActiveAssetTypes SelectedAssetType {  get { return _selectedAssetType; } set { _selectedAssetType = value; OnPropertyChanged(); } }

        private ObservableCollection<ActiveAssetTypes> _assetTypes;

        public ObservableCollection<ActiveAssetTypes> AssetTypes {
            get { return _assetTypes; }

            set { _assetTypes = value; OnPropertyChanged(); }
        }

        public async Task<bool> LoadData() {
            var levelHandler = new LevelEditorHandler(Handler);

            var response = await levelHandler.GetLevelEditorData();

            if (response.HasError) {
                return false;
            }

            AssetTypes = new ObservableCollection<ActiveAssetTypes>(response.Value.AssetTypes);

            SelectedAssetType = AssetTypes.FirstOrDefault();

            return true;
        }
    }
}