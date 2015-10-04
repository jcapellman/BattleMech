using System.Collections.ObjectModel;
using System.Threading.Tasks;

using BattleMech.DataLayer.PCL.Views.GameMetrics;
using BattleMech.WebAPI.PCL.Handlers;

namespace BattleMech.PCL.ViewModels {
    public class CharacterProfileModel : BaseViewModel {
        private ObservableCollection<PlayerGameListingView> _gameHistory; 

        public ObservableCollection<PlayerGameListingView> GameHistory {
            get { return _gameHistory; }
            set { _gameHistory = value; OnPropertyChanged(); }
        } 

        public async Task<bool> LoadData() {
            var login = await AttemptLogin("test", "test");

            var character = await new GameMetricHandler(Handler).GetGameMetrics();

            if (!character.HasError) {
                GameHistory = new ObservableCollection<PlayerGameListingView>(character.Value);

                return true;
            }

            return false;
        }
    }
}