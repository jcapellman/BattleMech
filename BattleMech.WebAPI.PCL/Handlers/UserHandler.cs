using System.Threading.Tasks;

using BattleMech.DataLayer.PCL.Models.Users;
using BattleMech.WebAPI.PCL.Transports.Common;

namespace BattleMech.WebAPI.PCL.Handlers {
    public class UserHandler : BaseHandler {
        public UserHandler(string baseURL) : base(baseURL, "Users") { }

        public async Task<CTI<bool>> AddUser(Users user) { return await PUT<Users, bool>(user); }
    }
}