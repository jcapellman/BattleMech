using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using BattleMech.WebAPI.PCL.Handlers;
using BattleMech.WebAPI.PCL.Transports.Auth;
using BattleMech.WebAPI.PCL.Transports.Internal;

namespace BattleMech.PCL.ViewModels {
    public class BaseViewModel : INotifyPropertyChanged {
        private string _token { get; set; }

        public HandlerItem Handler => new HandlerItem { Token = _token, WEBAPIUrl = "http://battlemech.azurewebsites.net/api/" };

        public async Task<bool> AttemptLogin(string username, string password) {
            if (!string.IsNullOrEmpty(_token)) {
                return true;
            }

            var token = await new AuthHandler(Handler).GenerateToken(new AuthRequestItem { Username = username, Password = password });

            if (token != null) {
                _token = token.Token;
            }

            return !string.IsNullOrEmpty(_token);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}