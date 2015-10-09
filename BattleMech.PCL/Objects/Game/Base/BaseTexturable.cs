using BattleMech.PCL.Enums;
using BattleMech.PCL.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BattleMech.PCL.Objects.Game.Base {
    public class BaseTexturable : BaseRenderable, ICollidable
    {
        public Texture2D Texture;

        public TEXTURABLE_ITEM_TYPES ItemType;
        
        public bool IsFullScreen;

        public bool IsColliding { get; set; }

        public BaseTexturable(Texture2D texture, TEXTURABLE_ITEM_TYPES itemType) {
            Texture = texture;

            ItemType = itemType;
        }

        public void Init(Texture2D texture)
        {
            this.Texture = texture;

            IsActive = true;
        }

        public override Rectangle GetRectange() {
            if (IsFullScreen) {
                return new Rectangle(0, 0, (int) PositionX, (int) PositionY);
            }

            return new Rectangle((int) PositionX, (int) PositionY, Texture.Width, Texture.Height);
        }

        public virtual bool GetCollision(Rectangle other)
        {
            //check collision against other rectangle
            Rectangle bounds = GetRectange();

            if ((bounds.Right >= other.Left && bounds.Left < other.Right ||
                bounds.Left <= other.Right && bounds.Right > other.Left) &&
                (bounds.Bottom >= other.Top && bounds.Top < other.Bottom ||
                bounds.Top <= other.Bottom && bounds.Bottom > other.Top))
            {
                return true;
            }

            return false;
        }
    }
}