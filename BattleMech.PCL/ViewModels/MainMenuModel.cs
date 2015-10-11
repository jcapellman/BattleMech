using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BattleMech.DataLayer.PCL.Views.Assets;
using BattleMech.PCL.Enums;
using BattleMech.PCL.Objects.Game;
using BattleMech.PCL.PSI;
using BattleMech.WebAPI.PCL.Handlers;
using BattleMech.WebAPI.PCL.Transports.Auth;
using BattleMech.WebAPI.PCL.Transports.CharacterProfile;
using BattleMech.WebAPI.PCL.Transports.Common;

namespace BattleMech.PCL.ViewModels {
    public class MainMenuModel : BaseViewModel {
        private readonly BaseSettingsPSI _settingPSI;
        private BaseLFSPSI _lfsPSI;

        public MainMenuModel(string token, BaseSettingsPSI setting, BaseLFSPSI lfsPSI) : base(token) {
            _settingPSI = setting;
            _lfsPSI = lfsPSI;
        }

        private bool _createAccountEnabled;

        public bool CreateAccountEnabled { get { return _createAccountEnabled; } set { _createAccountEnabled = value; OnPropertyChanged(); } }

        private bool _isLoggedIn;

        public bool IsLoggedIn { get { return _isLoggedIn; } set { _isLoggedIn = value; OnPropertyChanged(); } }

        private string _loginButtonText;

        public string LoginButtonText {  get { return _loginButtonText; } set { _loginButtonText = value; OnPropertyChanged(); } }

        private string _username;

        public string Username {  get { return _username; } set { _username = value; OnPropertyChanged(); LoginEnabled = checkForm(); } }

        private string _password;

        public string Password {  get { return _password; } set { _password = value; OnPropertyChanged(); LoginEnabled = checkForm(); } }

        private bool _loginEnabled;

        public bool LoginEnabled {  get { return _loginEnabled; } set { _loginEnabled = value; OnPropertyChanged(); } }

        private bool _rememberLogin;

        public bool RememberLogin { get { return _rememberLogin; } set { _rememberLogin = value; OnPropertyChanged(); } }

        private bool _createEnabled;

        public bool CreateEnabled { get { return _createEnabled; } set { _createEnabled = value; OnPropertyChanged(); } }
        
        private string _caUsername;

        public string caUsername {  get { return _caUsername; } set { _caUsername = value; OnPropertyChanged(); CreateEnabled = checkCreateForm(); } }

        private string _caPassword;

        public string caPassword { get { return _caPassword; } set { _caPassword = value; OnPropertyChanged(); CreateEnabled = checkCreateForm(); } }

        private string _caFirstName;

        public string caFirstName { get { return _caFirstName; } set { _caFirstName = value; OnPropertyChanged(); CreateEnabled = checkCreateForm(); } }

        private string _caLastName;

        public string caLastName { get { return _caLastName; } set { _caLastName = value; OnPropertyChanged(); CreateEnabled = checkCreateForm(); } }

        public async Task<AuthResponseItem> LoadData() {
            IsLoggedIn = !string.IsNullOrEmpty(_token);
            LoginButtonText = (IsLoggedIn ? "LOGOUT" : "LOGIN");
            CreateAccountEnabled = !IsLoggedIn;
            
            if (_settingPSI.Get<bool>(Enums.SETTING_OPTIONS.REMEMBER_LOGIN)) {
                Username = _settingPSI.Get<string>(Enums.SETTING_OPTIONS.USERNAME);
                Password = _settingPSI.Get<string>(Enums.SETTING_OPTIONS.PASSWORD);

                return await AttemptLogin();
            }

            return null;
        }

        public async Task<List<ActiveAssetsVIEW>> LoadAssetData() {
            var assetHandler = new AssetHandler(Handler);

            var result = await assetHandler.GetAssets(_settingPSI.Get<int>(SETTING_OPTIONS.ASSET_VERSION));

            if (result.HasError) {
                return null;
            }

            if (result.Value.ClientHasLatest) {
                return null;
            }
            
            _settingPSI.Write(SETTING_OPTIONS.ASSET_VERSION, result.Value.AssetVerion);
            
            var fileWrite = await _lfsPSI.WriteFile(result.Value);

            for (var x = 0; x < result.Value.Assets.Count; x++) {
                var item = result.Value.Assets[x];

                result.Value.Assets[x].Filename = $"/Content/{((ASSET_TYPES)item.AssetTypeID).ToString()}/{item.Filename}";
            }

            return result.Value.Assets;
        }

        private string Token { get { return _token; } set { _token = value; OnPropertyChanged(); } }

        private bool checkForm() {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        private bool checkCreateForm() {
            return !string.IsNullOrEmpty(caUsername) && !string.IsNullOrEmpty(caPassword) && !string.IsNullOrEmpty(caFirstName) && !string.IsNullOrEmpty(caLastName);
        }

        public async Task<AuthResponseItem> AttemptLogin() {
            var result = await AttemptLogin(Username, Password);

            if (!string.IsNullOrEmpty(result?.Token)) {
                IsLoggedIn = true;
                LoginButtonText = "LOGOUT";
                Token = result.Token;

                if (RememberLogin) {
                    _settingPSI.Write(Enums.SETTING_OPTIONS.USERNAME, Username);
                    _settingPSI.Write(Enums.SETTING_OPTIONS.PASSWORD, Password);
                    _settingPSI.Write(Enums.SETTING_OPTIONS.REMEMBER_LOGIN, RememberLogin);
                }
            } else {
                LoginButtonText = "LOGIN";

                _settingPSI.Delete(Enums.SETTING_OPTIONS.USERNAME);
                _settingPSI.Delete(Enums.SETTING_OPTIONS.PASSWORD);
                _settingPSI.Delete(Enums.SETTING_OPTIONS.REMEMBER_LOGIN);
            }

            CreateAccountEnabled = !IsLoggedIn;

            return result;
        }

        public async Task<CTI<CharacterProfileResponseItem>> GetCharacterInfo()
        {
            var charHandler = new CharacterProfileHandler(Handler);
            var result = await charHandler.GET();

            return result;
        }

        public void Logout() {
            IsLoggedIn = false;

            LoginButtonText = "LOGIN";

            CreateAccountEnabled = !IsLoggedIn;
        }

        public void ClearCreateAccountForm() {
            caUsername = string.Empty;
            caPassword = string.Empty;
            caFirstName = string.Empty;
            caLastName = string.Empty;
        }

        public async Task<bool> AttemptCreateAccount() {
            var userHandler = new UserHandler(Handler);

            var user = new DataLayer.PCL.Models.Users.Users {
                EmailAddress = caUsername,
                Password = caPassword,
                FirstName = caFirstName,
                LastName = caLastName,
                Activated = true
            };

            var result = await userHandler.AddUser(user);

            return result.Value;
        }

        public async Task<Level> GenerateLevel(List<ActiveAssetsVIEW> assets)
        {
            var level = new Level
            {
                LevelName = "RandomGeneratedLevel",
                Objects = new List<LevelObject>()
            };

            LevelObject lvlObj = new LevelObject();

            return level;
        }
    }
}