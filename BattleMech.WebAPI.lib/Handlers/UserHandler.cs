using System.Threading.Tasks;

using BattleMech.DataLayer.PCL.Models.Users;
using BattleMech.WebAPI.PCL.Transports.Common;
using BattleMech.WebAPI.PCL.Transports.Internal;

namespace BattleMech.WebAPI.PCL.Handlers {
    public class UserHandler : BaseHandler {
        public UserHandler(HandlerItem handlerItem) : base(handlerItem, "Users") { }

        public async Task<CTI<bool>> AddUser(Users user) { return await PUT<Users, CTI<bool>>(user); }
    }
}