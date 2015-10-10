using BattleMech.WebAPI.PCL.Handlers;
using BattleMech.DataLayer.PCL.Views.Assets;

using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BattleMech.PCL.Enums;

namespace BattleMech.PCL.ViewModels {
    public class LevelEditorModel : BaseViewModel {
        public LevelEditorModel(string token) : base(token) { _assets = new ObservableCollection<ActiveAssetsVIEW>(); }

        private ActiveAssetTypes _selectedAssetType;

        public ActiveAssetTypes SelectedAssetType {  get { return _selectedAssetType; } set { _selectedAssetType = value; OnPropertyChanged(); OnPropertyChanged("Assets"); } }

        private ObservableCollection<ActiveAssetTypes> _assetTypes;

        public ObservableCollection<ActiveAssetTypes> AssetTypes {
            get { return _assetTypes; }

            set { _assetTypes = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ActiveAssetsVIEW> _assets;

        public ObservableCollection<ActiveAssetsVIEW> Assets {
            get { return new ObservableCollection<ActiveAssetsVIEW>(_assets.Where(a => a.AssetTypeID == SelectedAssetType.ID)); }
            set { _assets = value; OnPropertyChanged(); }
        }

        public async Task<bool> LoadData() {
            var levelHandler = new LevelEditorHandler(Handler);

            var response = await levelHandler.GetLevelEditorData();

            if (response.HasError) {
                return false;
            }

            AssetTypes = new ObservableCollection<ActiveAssetTypes>(response.Value.AssetTypes);

            SelectedAssetType = AssetTypes.FirstOrDefault();

            var tmp = new ObservableCollection<ActiveAssetsVIEW>(response.Value.Assets);

            foreach (var item in tmp) {
                item.Filename = $"/Content/{((ASSET_TYPES)item.AssetTypeID).ToString()}/{item.Filename}";
            }

            Assets = tmp;

            return true;
        }
    }
}