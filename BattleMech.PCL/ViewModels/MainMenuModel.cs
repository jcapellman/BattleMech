using System.Threading.Tasks;

namespace BattleMech.PCL.ViewModels {
    public class MainMenuModel : BaseViewModel {
        public MainMenuModel(string token) : base(token) {  }

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

        public void LoadData() {
            IsLoggedIn = !string.IsNullOrEmpty(_token);
            LoginButtonText = (IsLoggedIn ? "LOGOUT" : "LOGIN");
        }

        public string Token { get; set; }

        private bool checkForm() {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        public async Task<bool> AttemptLogin() {
            var result = await AttemptLogin(Username, Password);

            if (result != null && !string.IsNullOrEmpty(result.Token)) {
                IsLoggedIn = true;
                LoginButtonText = "LOGOUT";
                Token = result.Token;
            } else {
                LoginButtonText = "LOGIN";
            }

            return result != null && !string.IsNullOrEmpty(result.Token);
        }

        public void Logout() {
            IsLoggedIn = false;

            LoginButtonText = "LOGIN";
        }
    }
}