using Microsoft.Data.Entity;

namespace BattleMech.DataLayer.Contexts {
    public class BaseContext : DbContext {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=BattleMech;Trusted_Connection=True;");
        } 
    }
}