using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BattleMech.DataLayer.PCL.Views.Assets;
using BattleMech.DataLayer.PCL.Views.Levels;
using BattleMech.PCL.Enums;
using BattleMech.PCL.Objects.Game;
using BattleMech.WebAPI.PCL.Handlers;
using BattleMech.WebAPI.PCL.Transports.Common;
using BattleMech.WebAPI.PCL.Transports.Levels;
using Newtonsoft.Json;

namespace BattleMech.PCL.ViewModels {
    public class LevelModel : BaseViewModel {
        public LevelModel(string token) : base(token) { }

        private ObservableCollection<LevelListingView> _levelListing;
         
        public ObservableCollection<LevelListingView> LevelListing {
            get { return _levelListing; }

            set { _levelListing = value; OnPropertyChanged(); }
        }

        private LevelListingView _selectedLevel;

        public LevelListingView SelectedLevel {  get { return _selectedLevel; } set { _selectedLevel = value; OnPropertyChanged(); } }

        public async Task<bool> LoadData() {
            var levelHandler = new LevelHandler(Handler);

            var result = await levelHandler.GetLevelList();

            if (result.HasError) {
                return false;
            }

            LevelListing = new ObservableCollection<LevelListingView>(result.Value);

            SelectedLevel = LevelListing.FirstOrDefault();

            return true;
        }

        public async Task<Level> LoadLevel(List<ActiveAssetsVIEW> assets) {
            var levelHandler = new LevelHandler(Handler);

            var result = await levelHandler.GetLevel(SelectedLevel.ID);

            if (result.HasError) {
                return null;
            }

            var level = new Level {
                LevelName = result.Value.Description,
                Objects = new List<LevelObject>()
            };
            
            var levelObjects = (List<LevelObjectLite>)JsonConvert.DeserializeObject(result.Value.LevelData);

            foreach (var item in levelObjects) {
                var asset = assets.FirstOrDefault(a => a.ID == item.AssetID);

                var nItem = new LevelObject {
                    AssetType = (ASSET_TYPES) asset.AssetTypeID,
                    Filename = asset.Filename,
                    PositionX = item.PositionX,
                    PositionY = item.PositionY,
                    AssetID = asset.ID
                };
                
                level.Objects.Add(nItem);
            }

            return level;
        }
    }
}