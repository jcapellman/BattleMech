using BattleMech.PCL.Enums;

using System.Threading.Tasks;

namespace BattleMech.WPF.ViewModels {
    public class MainModel : BaseViewModel {
        public MainModel(string token) : base(token) {  }

        private bool _loginEnabled;

        public bool LoginEnabled { get { return _loginEnabled; } set { _loginEnabled = value; OnPropertyChanged(); } }


        private bool _createAccountEnabled;

        public bool CreateAccountEnabled { get { return _createAccountEnabled; } set { _createAccountEnabled = value; OnPropertyChanged(); } }

        private bool _isLoggedIn;

        public bool IsLoggedIn { get { return _isLoggedIn; } set { _isLoggedIn = value; OnPropertyChanged(); } }

        private string _loginButtonText;

        public string LoginButtonText { get { return _loginButtonText; } set { _loginButtonText = value; OnPropertyChanged(); } }

        public string Token { get; set; }

        private string _username;

        public string Username { get { return _username; } set { _username = value; OnPropertyChanged(); LoginEnabled = checkForm(); } }

        private string _password;

        public string Password { get { return _password; } set { _password = value; OnPropertyChanged(); LoginEnabled = checkForm(); } }

        private bool _rememberLogin;

        public bool RememberLogin { get { return _rememberLogin; } set { _rememberLogin = value; OnPropertyChanged(); } }

        private bool checkForm() {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        public async Task<bool> LoadData() {
            IsLoggedIn = !string.IsNullOrEmpty(_token);
            LoginButtonText = (IsLoggedIn ? "LOGOUT" : "LOGIN");
            CreateAccountEnabled = !IsLoggedIn;

            if (getConfig<bool>(SETTING_OPTIONS.REMEMBER_LOGIN)) {
                Username = getConfig<string>(SETTING_OPTIONS.USERNAME);
                Password = getConfig<string>(SETTING_OPTIONS.PASSWORD);

                return await AttemptLogin();
            }

            return false;
        }

        public async Task<bool> AttemptLogin() {
            var result = await AttemptLogin(Username, Password);

            if (result != null && !string.IsNullOrEmpty(result.Token)) {
                IsLoggedIn = true;
                LoginButtonText = "LOGOUT";
                Token = result.Token;

                if (RememberLogin) {
                    writeConfig(SETTING_OPTIONS.USERNAME, Username);
                    writeConfig(SETTING_OPTIONS.PASSWORD, Password);
                    writeConfig(SETTING_OPTIONS.REMEMBER_LOGIN, RememberLogin);
                }
            } else {
                LoginButtonText = "LOGIN";

                deleteConfig(SETTING_OPTIONS.USERNAME);
                deleteConfig(SETTING_OPTIONS.PASSWORD);
                deleteConfig(SETTING_OPTIONS.REMEMBER_LOGIN);
            }

            CreateAccountEnabled = !IsLoggedIn;

            return result != null && !string.IsNullOrEmpty(result.Token);
        }
    }
}