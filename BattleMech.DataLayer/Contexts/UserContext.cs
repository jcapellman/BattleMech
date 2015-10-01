using BattleMech.DataLayer.PCL.Models.Users;
using Microsoft.Data.Entity;

namespace BattleMech.DataLayer.Contexts {
    public class UserContext : BaseContext {
        public DbSet<Users> UsersDS { get; set; }
    }
}