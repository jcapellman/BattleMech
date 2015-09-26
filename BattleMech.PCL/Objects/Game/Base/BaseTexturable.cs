using BattleMech.PCL.Enums;

using Microsoft.Xna.Framework.Graphics;

namespace BattleMech.PCL.Objects.Game.Base {
    public class BaseTexturable : BaseRenderable {
        public Texture2D Texture;

        public TEXTURABLE_ITEM_TYPES ItemType;

        public BaseTexturable(Texture2D texture, TEXTURABLE_ITEM_TYPES itemType) {
            Texture = texture;

            ItemType = itemType;
        }
    }
}