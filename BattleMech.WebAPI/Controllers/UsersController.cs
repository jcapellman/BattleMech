using System.Threading.Tasks;

using BattleMech.DataLayer.PCL.Models.Users;
using BattleMech.WebAPI.Managers;
using BattleMech.WebAPI.PCL.Transports.Common;

namespace BattleMech.WebAPI.Controllers {
    public class UsersController : BaseController {
        public async Task<CTI<bool>> PUT(Users user) {
            return await new UserManager().AddUser(user);
        }
    }
}