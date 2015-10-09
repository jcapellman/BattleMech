using System.Collections.Generic;

using BattleMech.DataLayer.PCL.Views.Levels;
using BattleMech.WebAPI.Managers;
using BattleMech.WebAPI.PCL.Transports.Common;

namespace BattleMech.WebAPI.Controllers {
    public class LevelController : BaseController {
        public CTI<List<LevelListingView>> GET() { return new LevelManager(AUTH_USER).GetLevelListing(); }        
    }
}