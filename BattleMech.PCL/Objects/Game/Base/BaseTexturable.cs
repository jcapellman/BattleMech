using BattleMech.PCL.Enums;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BattleMech.PCL.Objects.Game.Base {
    public class BaseTexturable : BaseRenderable {
        public Texture2D Texture;

        public TEXTURABLE_ITEM_TYPES ItemType;

        public bool IsFullScreen;

        public BaseTexturable(Texture2D texture, TEXTURABLE_ITEM_TYPES itemType) {
            Texture = texture;

            ItemType = itemType;
        }

        public override Rectangle GetRectange() {
            if (IsFullScreen) {
                return new Rectangle(0, 0, (int) PositionX, (int) PositionY);
            }

            return new Rectangle((int) PositionX, (int) PositionY, Texture.Width, Texture.Height);
        }
    }
}