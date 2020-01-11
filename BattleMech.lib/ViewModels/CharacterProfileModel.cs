using System.Collections.ObjectModel;
using System.Threading.Tasks;

using BattleMech.DataLayer.PCL.Views.GameMetrics;
using BattleMech.WebAPI.PCL.Handlers;

namespace BattleMech.PCL.ViewModels {
    public class CharacterProfileModel : BaseViewModel {
        public CharacterProfileModel(string token) : base(token) { }

        private ObservableCollection<PlayerGameListingView> _gameHistory; 

        public ObservableCollection<PlayerGameListingView> GameHistory {
            get { return _gameHistory; }
            set { _gameHistory = value; OnPropertyChanged(); }
        }

        private PlayerCharacterView _playerCharacterView;

        public PlayerCharacterView PlayerCharacter {
            get {  return _playerCharacterView; }

            set { _playerCharacterView = value; OnPropertyChanged(); }
        }

        public async Task<bool> LoadData() {
            var character = await new CharacterProfileHandler(Handler).GET();

            if (!character.HasError) {
                GameHistory = new ObservableCollection<PlayerGameListingView>(character.Value.GameHistory);
                PlayerCharacter = character.Value.Character;

                return true;
            }

            return false;
        }
    }
}