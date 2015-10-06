using System;
using System.Collections.Generic;
using System.Linq;

using BattleMech.DataLayer.Contexts;
using BattleMech.DataLayer.PCL.Views.Levels;
using BattleMech.WebAPI.PCL.Transports.Common;

namespace BattleMech.WebAPI.Managers {
    public class LevelManager : BaseManager {
        public CTI<List<LevelListingView>> GetLevelListing() {
            try {
                using (var levelContext = new LevelContext()) {
                    var result = levelContext.LevelListingDS.ToList();

                    return new CTI<List<LevelListingView>>(result);
                }
            } catch (Exception ex) {
                return new CTI<List<LevelListingView>>(null, ex.ToString());
            }
        } 
    }
}