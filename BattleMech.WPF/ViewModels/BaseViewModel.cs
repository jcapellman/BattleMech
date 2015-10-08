using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using BattleMech.WebAPI.PCL.Handlers;
using BattleMech.WebAPI.PCL.Transports.Auth;
using BattleMech.WebAPI.PCL.Transports.Internal;
using BattleMech.PCL.Enums;

namespace BattleMech.WPF.ViewModels {
    public class BaseViewModel : INotifyPropertyChanged {
        protected string _token;

        public BaseViewModel(string token) { _token = token; }

        public HandlerItem Handler => new HandlerItem { Token = _token, WEBAPIUrl = "http://battlemech.azurewebsites.net/api/" };

        public async Task<AuthResponseItem> AttemptLogin(string username, string password) {
            return await new AuthHandler(Handler).GenerateToken(new AuthRequestItem { Username = username, Password = password });
        }

        internal void writeConfig<T>(SETTING_OPTIONS setting, T value) {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove(setting.ToString());
            config.AppSettings.Settings.Add(setting.ToString(), value.ToString());
            config.Save();

            ConfigurationManager.RefreshSection("appSettings");
        }

        internal T getConfig<T>(SETTING_OPTIONS setting) {
            object result = ConfigurationManager.AppSettings[setting.ToString()];

            if (result == null) {
                return default(T);
            }

            return (T)result;
        }

        internal void deleteConfig(SETTING_OPTIONS setting) {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove(setting.ToString());

            config.Save();

            ConfigurationManager.RefreshSection("appSettings");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}