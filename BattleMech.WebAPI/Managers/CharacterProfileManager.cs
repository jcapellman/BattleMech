using System;
using System.Linq;

using BattleMech.DataLayer.Contexts;
using BattleMech.WebAPI.PCL.Transports.CharacterProfile;
using BattleMech.WebAPI.PCL.Transports.Common;
using BattleMech.WebAPI.Transports.Internal;

namespace BattleMech.WebAPI.Managers {
    public class CharacterProfileManager : BaseManager {
        public CharacterProfileManager(AuthorizedUser authorizedUser) : base(authorizedUser) { }

        public CTI<CharacterProfileResponseItem> GetProfile(int? userID) {
            try {
                var selectedUserID = userID ?? _authorizedUser.ID;

                using (var db = new GameMetricContext()) {
                    var result = new CharacterProfileResponseItem {
                        GameHistory = db.GameListingViews.Where(a => a.UserID == selectedUserID).ToList(),
                        Character = db.CharacterViews.FirstOrDefault(a => a.UserID == selectedUserID)
                    };
                    
                    return new CTI<CharacterProfileResponseItem>(result);
                }
            } catch (Exception ex) {
                return new CTI<CharacterProfileResponseItem>(null, ex.ToString());
            }
        }
    }
}