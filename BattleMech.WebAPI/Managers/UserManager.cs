using System;
using System.Linq;
using Microsoft.Data.Entity;

using BattleMech.DataLayer.Contexts;
using BattleMech.DataLayer.PCL.Models.Users;
using BattleMech.WebAPI.PCL.Transports.Common;

namespace BattleMech.WebAPI.Managers {
    public class UserManager : BaseManager {
        public CTI<bool> AddUser(Users user) {
            try {
                using (var db = new UserContext()) {
                    var existingMatch = db.UsersDS.FromSql("SELECT * FROM Users WHERE Active = 1").Any(c => c.EmailAddress == user.EmailAddress);

                    if (existingMatch) {
                        return new CTI<bool>(false);
                    }

                    db.UsersDS.Add(user);

                    var result = db.SaveChanges();

                    if (result == 0) {
                        throw new Exception("Could not create account");
                    }
                    
                    var playerCharacter = new PlayerCharacters();

                    playerCharacter.AssetID = 7;
                    playerCharacter.ClassTypeID = 1;
                    playerCharacter.Experience = 0;
                    playerCharacter.LevelNumber = 1;
                    playerCharacter.Name = "N3WB";
                    playerCharacter.UserID = user.ID;

                    db.PlayerCharactersDS.Add(playerCharacter);

                    result = db.SaveChanges();

                    return new CTI<bool>(result > 0);
                }
            } catch (Exception ex) {
                return new CTI<bool>(false, ex.ToString());
            }
        }
    }
}