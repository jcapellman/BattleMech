using System.Linq;

using BattleMech.DataLayer.Contexts;
using BattleMech.WebAPI.PCL.Transports.Auth;
using BattleMech.WebAPI.Transports.Internal;

namespace BattleMech.WebAPI.Managers {
    public class AuthManager : BaseManager {
        public AuthResponseItem GenerateToken(AuthRequestItem requestItem) {
            using (var uFactory = new UserContext()) {
                var match = uFactory.UsersDS.FirstOrDefault(a => a.EmailAddress == requestItem.Username && a.Password == requestItem.Password);

                return match == null ? null : new AuthResponseItem {Token = "1234"};
            }
        }

        public AuthorizedUser CheckToken(string token) {
            if (string.IsNullOrEmpty(token)) {
                return null;
            }

            return new AuthorizedUser {ID = 1};
        }
    }
}