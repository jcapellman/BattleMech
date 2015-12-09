using BattleMech.DataLayer.PCL.Views.Assets;
using BattleMech.PCL.Enums;
using BattleMech.PCL.Objects.Game.Base;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BattleMech.PCL.Objects.Game {
    public class Background : BaseTexturable {
        public Background(ContentManager content, List<ActiveAssetsVIEW> assetInfos) : base(content, assetInfos, TEXTURABLE_ITEM_TYPES.BACKGROUND) {
            MoveX -= 2f;

            IsFullScreen = true;
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);


        }
    }
}