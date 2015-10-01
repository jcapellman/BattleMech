using System;
using System.Threading.Tasks;
using BattleMech.DataLayer.Contexts;

using BattleMech.DataLayer.PCL.Models.Users;
using BattleMech.WebAPI.PCL.Transports.Common;

namespace BattleMech.WebAPI.Managers {
    public class UserManager : BaseManager {
        public async Task<CTI<bool>> AddUser(Users user) {
            try {
                using (var db = new UserContext()) {
                    db.UsersDS.Add(user);

                    var result = await db.SaveChangesAsync();

                    return new CTI<bool>(result > 0);
                }
            } catch (Exception ex) {
                return new CTI<bool>(false, ex.ToString());
            }
        }
    }
}