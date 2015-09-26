using BattleMech.PCL.Enums;
using BattleMech.PCL.Objects.Game.Base;

using Microsoft.Xna.Framework.Graphics;

namespace BattleMech.PCL.Objects.Game {
    public class Player : BaseTexturable {
        public Player(Texture2D texture) : base(texture, TEXTURABLE_ITEM_TYPES.PLAYER) {
            PositionX = 200.0f;
            PositionY = 200.0f;
        }
    }
}