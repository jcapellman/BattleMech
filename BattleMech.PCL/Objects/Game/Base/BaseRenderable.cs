using System;
using Microsoft.Xna.Framework;

namespace BattleMech.PCL.Objects.Game.Base {
    public abstract class BaseRenderable {
        public float PositionX;

        public float PositionY;

        public float OriginX;

        public float OriginY;

        public float MoveX;

        public float MoveY;

        public float Width;

        public float Height;

        public Guid ID;

        public bool BoundWithinScreen;

        public bool IsActive;

        public BaseRenderable() {
            OriginX = 0.0f;
            OriginY = 0.0f;

            PositionX = 0.0f;
            PositionY = 0.0f;

            BoundWithinScreen = false;
            IsActive = true;
        }

        public virtual void Update(GameTime gameTime) {
            PositionX += MoveX;
            PositionY += MoveY;
        }

        public Vector2 Origin => new Vector2(OriginX, OriginY);

        public Vector2 Position => new Vector2(PositionX, PositionY);

        public abstract Rectangle GetRectange();
    }
}