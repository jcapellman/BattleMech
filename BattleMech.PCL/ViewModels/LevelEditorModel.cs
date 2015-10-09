using BattleMech.DataLayer.PCL.Views.Levels;
using BattleMech.WebAPI.PCL.Handlers;

using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BattleMech.PCL.ViewModels {
    public class LevelEditorModel : BaseViewModel {
        public LevelEditorModel(string token) : base(token) { }

        private ObservableCollection<ActiveAssetTypes> _assetTypes;

        public ObservableCollection<ActiveAssetTypes> AssetTypes {
            get { return _assetTypes; }

            set { _assetTypes = value; OnPropertyChanged(); }
        }

        public async Task<bool> LoadData() {
            var levelHandler = new LevelHandler(Handler);

            var response = await levelHandler.GetLevelEditorData();

            if (response.HasError) {
                return false;
            }

            AssetTypes = new ObservableCollection<ActiveAssetTypes>(response.Value.AssetTypes);

            return true;
        }
    }
}