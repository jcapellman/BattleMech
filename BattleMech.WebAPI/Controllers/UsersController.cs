using BattleMech.DataLayer.PCL.Models.Users;
using BattleMech.WebAPI.Managers;
using BattleMech.WebAPI.PCL.Transports.Common;

namespace BattleMech.WebAPI.Controllers {
    public class UsersController : BaseController {
        public CTI<bool> PUT(Users user) {
            return new UserManager().AddUser(user);
        }
    }
}