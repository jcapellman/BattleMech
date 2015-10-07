using System.Collections.ObjectModel;
using System.Threading.Tasks;

using BattleMech.DataLayer.PCL.Views.Levels;
using BattleMech.WebAPI.PCL.Handlers;

namespace BattleMech.PCL.ViewModels {
    public class LevelModel : BaseViewModel {
        public LevelModel(string token) : base(token) { }

        private ObservableCollection<LevelListingView> _levelListing;
         
        public ObservableCollection<LevelListingView> LevelListing {
            get { return _levelListing; }

            set { _levelListing = value; OnPropertyChanged(); }
        }

        public async Task<bool> LoadData() {
            var levelHandler = new LevelHandler(Handler);

            var result = await levelHandler.GetLevelList();

            if (result.HasError) {
                return false;
            }

            LevelListing = new ObservableCollection<LevelListingView>(result.Value);

            return true;
        }
    }
}