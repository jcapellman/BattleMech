using BattleMech.PCL.Enums;
using BattleMech.PCL.Objects.Game.Base;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using BattleMech.DataLayer.PCL.Views.Assets;
using Microsoft.Xna.Framework.Content;

namespace BattleMech.PCL.Objects.Game.Actors
{
    public class GenericBullet : BaseTexturable
    {
        public GenericBullet(ContentManager content, List<ActiveAssetsVIEW> assetInfos) : base(content, assetInfos, TEXTURABLE_ITEM_TYPES.PROJECTILE)
        {
            MoveX = 10.0f;
            PositionX = 400.0f;
            PositionY = 400.0f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (CheckOnScreen != null && !CheckOnScreen(GetRectange())) IsActive = false;
        }
    }
}
