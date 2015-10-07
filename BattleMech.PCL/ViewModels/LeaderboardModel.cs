using System.Collections.ObjectModel;
using System.Threading.Tasks;

using BattleMech.DataLayer.PCL.Views.Leaderboard;
using BattleMech.WebAPI.PCL.Handlers;

namespace BattleMech.PCL.ViewModels {
    public class LeaderboardModel : BaseViewModel {
        public LeaderboardModel(string token) : base(token) { }

        private ObservableCollection<LeaderboardView> _leaderboardListing;

        public ObservableCollection<LeaderboardView> LeaderboardListing {
            get { return _leaderboardListing; }

            set { _leaderboardListing = value; OnPropertyChanged(); }
        }

        public async Task<bool> LoadData() {
            var leaderboardHandler = new LeaderboardHandler(Handler);

            var result = await leaderboardHandler.GetLeaderboardList();

            if (result.HasError) {
                return false;
            }

            LeaderboardListing = new ObservableCollection<LeaderboardView>(result.Value);

            return true;
        }
    }
}