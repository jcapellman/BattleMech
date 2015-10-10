using BattleMech.PCL.Enums;
using BattleMech.PCL.Objects.Game.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BattleMech.PCL.Objects.Game {
    public class Enemy : BaseTexturable {
        public Enemy(Texture2D texture) : base(texture, TEXTURABLE_ITEM_TYPES.ENEMY) {
            MoveX -= 4.0f;

            PositionX = 1200.0f;
            PositionY = 400.0f;
        }

        

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //if (CheckOnScreen != null && !CheckOnScreen(GetRectange())) IsActive = false;
        }
    }
}