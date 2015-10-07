using BattleMech.WebAPI.PCL.Handlers;
using System;
using System.Threading.Tasks;

namespace BattleMech.PCL.ViewModels {
    public class MainMenuModel : BaseViewModel {
        public MainMenuModel(string token) : base(token) {  }

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

        public void LoadData() {
            IsLoggedIn = !string.IsNullOrEmpty(_token);
            LoginButtonText = (IsLoggedIn ? "LOGOUT" : "LOGIN");
            CreateAccountEnabled = !IsLoggedIn;
        }

        public string Token { get; set; }

        private bool checkForm() {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        private bool checkCreateForm() {
            return !string.IsNullOrEmpty(caUsername) && !string.IsNullOrEmpty(caPassword) && !string.IsNullOrEmpty(caFirstName) && !string.IsNullOrEmpty(caLastName);
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

            CreateAccountEnabled = !IsLoggedIn;

            return result != null && !string.IsNullOrEmpty(result.Token);
        }

        public void ClearAccountForm() {
            throw new NotImplementedException();
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
                Active = true,
                Created = DateTimeOffset.Now,
                Modified = DateTimeOffset.Now,
                FirstName = caFirstName,
                LastName = caLastName,
                Activated = true
            };

            var result = await userHandler.AddUser(user);

            return result.Value;
        }
    }
}