using BattleMech.PCL.Enums;
using BattleMech.PCL.Objects.Game.Base;
using Microsoft.Xna.Framework.Graphics;

namespace BattleMech.PCL.Objects.Game {
    public class Background : BaseTexturable {
        public Background(Texture2D texture) : base(texture, TEXTURABLE_ITEM_TYPES.BACKGROUND) {
            MoveX -= 0.25f;

            IsFullScreen = true;
        }
    }
}