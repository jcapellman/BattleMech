using BattleMech.DataLayer.PCL.Views.Assets;
using BattleMech.PCL.Enums;
using BattleMech.PCL.Managers.Game;
using BattleMech.PCL.Objects.Game.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BattleMech.PCL.Objects.Game {
    public class Enemy : BaseTexturable {
        public readonly int _deathAsset = 7;

        public Enemy(ContentManager content, List<ActiveAssetsVIEW> assetInfos) : base(content, assetInfos, TEXTURABLE_ITEM_TYPES.ENEMY) {
            MoveX -= 4.0f;

            PositionX = 1200.0f;
            PositionY = 400.0f;
        }

        //public void OnDeath(ActiveAssetsVIEW deathAsset, TexturableManager tm)
        //{
        //    var effect = tm.AddTextureItem<VisualEffect>(deathAsset);

        //    effect.PositionX = this.PositionX;
        //    effect.PositionY = this.PositionY;
        //}

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //if (CheckOnScreen != null && !CheckOnScreen(GetRectange())) IsActive = false;
        }
    }
}