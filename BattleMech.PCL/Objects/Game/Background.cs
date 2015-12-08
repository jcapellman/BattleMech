using BattleMech.DataLayer.PCL.Views.Assets;
using BattleMech.PCL.Enums;
using BattleMech.PCL.Objects.Game.Base;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BattleMech.PCL.Objects.Game {
    public class Background : BaseTexturable {
        public Background(ContentManager content, List<ActiveAssetsVIEW> assetInfos) : base(content, assetInfos, TEXTURABLE_ITEM_TYPES.BACKGROUND) {
            MoveX -= 0.25f;

            IsFullScreen = true;
        }
    }
}