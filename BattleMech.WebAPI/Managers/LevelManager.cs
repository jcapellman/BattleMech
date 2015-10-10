using System;
using System.Collections.Generic;
using System.Linq;

using BattleMech.DataLayer.Contexts;
using BattleMech.DataLayer.PCL.Models.Levels;
using BattleMech.DataLayer.PCL.Views.Levels;
using BattleMech.WebAPI.PCL.Transports.Common;
using BattleMech.WebAPI.PCL.Transports.Levels;
using BattleMech.WebAPI.Transports.Internal;

namespace BattleMech.WebAPI.Managers {
    public class LevelManager : BaseManager {
        public LevelManager(AuthorizedUser authorizedUser = null) : base(authorizedUser) { }

        internal CTI<bool> AddUpdateLevel(Levels level) {
            using (var levelEditorContext = new LevelEditorContext()) {
                level.UserAuthorID = _authorizedUser.ID;

                if (level.IsNew) {
                    levelEditorContext.LevelsDS.Add(level);
                } else {
                    levelEditorContext.LevelsDS.Update(level);
                }

                levelEditorContext.SaveChanges();

                return new CTI<bool>(true);
            }
        }

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

        internal CTI<LevelEditorResponseItem> GetEditorData() {
            try {
                using (var levelEditorContext = new LevelEditorContext()) {
                    var response = new LevelEditorResponseItem {
                        AssetTypes = levelEditorContext.ActiveAssetTypesVIEWDS.ToList(),
                        Assets = levelEditorContext.ActiveAssetsVIEWDS.ToList()
                    };

                    return new CTI<LevelEditorResponseItem>(response);
                }
            } catch (Exception ex) {
                return new CTI<LevelEditorResponseItem>(null, ex.ToString());
            }
        }
    }
}