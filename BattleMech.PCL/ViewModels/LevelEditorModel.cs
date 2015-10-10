using BattleMech.WebAPI.PCL.Handlers;
using BattleMech.DataLayer.PCL.Views.Assets;
using BattleMech.PCL.Enums;
using BattleMech.DataLayer.PCL.Models.Levels;

using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BattleMech.PCL.ViewModels {
    public class LevelEditorModel : BaseViewModel {
        public LevelEditorModel(string token) : base(token) {
            _assets = new ObservableCollection<ActiveAssetsVIEW>();

            LevelItems = new ObservableCollection<LEVELITEM>();

            Level = new Levels();
        }

        private Levels _level;

        public Levels Level {  get { return _level; } set { _level = value; OnPropertyChanged(); } }

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

        public struct LEVELITEM {
            public int AssetID { get; set; }

            public string Filename { get; set; }
        }

        private ObservableCollection<LEVELITEM> _levelItems;

        public ObservableCollection<LEVELITEM> LevelItems { get { return _levelItems; } set { _levelItems = value; OnPropertyChanged(); } }

        private ActiveAssetsVIEW _selectedAsset;

        public ActiveAssetsVIEW SelectedAsset {  get { return _selectedAsset; } set { _selectedAsset = value; OnPropertyChanged(); } }

        public void AddItem() {
            LevelItems.Add(new LEVELITEM { AssetID = SelectedAsset.ID, Filename = SelectedAsset.Filename });
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

        public async Task<bool> Save() {
            var levelHandler = new LevelEditorHandler(Handler);

            Level.Data = string.Join("|", LevelItems.Select(a => a.AssetID));

            var result = await levelHandler.AddUpdateLevel(Level);

            return true;
        }
    }
}