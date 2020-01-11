using BattleMech.WebAPI.PCL.Handlers;
using BattleMech.DataLayer.PCL.Views.Assets;
using BattleMech.PCL.Enums;
using BattleMech.DataLayer.PCL.Models.Levels;

using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BattleMech.PCL.Objects.Game;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BattleMech.PCL.ViewModels {
    public class LevelEditorModel : BaseViewModel {
        public LevelEditorModel(string token) : base(token) {
            _assets = new ObservableCollection<ActiveAssetsVIEW>();

            LevelItems = new ObservableCollection<LevelObject>();

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
        
        private ObservableCollection<LevelObject> _levelItems;

        public ObservableCollection<LevelObject> LevelItems { get { return _levelItems; } set { _levelItems = value; OnPropertyChanged(); } }

        private ActiveAssetsVIEW _selectedAsset;

        public ActiveAssetsVIEW SelectedAsset {  get { return _selectedAsset; } set { _selectedAsset = value; OnPropertyChanged(); } }

        public void AddItem() {
            LevelItems.Add(new LevelObject { AssetID = SelectedAsset.ID, AssetInfos =  new List<ActiveAssetsVIEW>() { SelectedAsset }, PositionX = LevelItems.Count() });
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

        private string LevelItemsToJSON() {
            var cList =
                LevelItems.Select(
                    a => new LevelObjectLite {AssetID = a.AssetID, PositionX = a.PositionX, PositionY = a.PositionY});

            return JsonConvert.SerializeObject(cList);
        }

        public async Task<bool> Save() {
            var levelHandler = new LevelEditorHandler(Handler);

            Level.Data = LevelItemsToJSON();

            var result = await levelHandler.AddUpdateLevel(Level);

            return true;
        }
    }
}