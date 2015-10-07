using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using BattleMech.WebAPI.PCL.Handlers;
using BattleMech.WebAPI.PCL.Transports.Auth;
using BattleMech.WebAPI.PCL.Transports.Internal;

namespace BattleMech.PCL.ViewModels {
    public class BaseViewModel : INotifyPropertyChanged {
        protected string _token;

        public BaseViewModel(string token) { _token = token; }

        public HandlerItem Handler => new HandlerItem { Token = _token, WEBAPIUrl = "http://battlemech.azurewebsites.net/api/" };

        public async Task<AuthResponseItem> AttemptLogin(string username, string password) {
            return await new AuthHandler(Handler).GenerateToken(new AuthRequestItem { Username = username, Password = password });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}