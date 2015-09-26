using System;
using Microsoft.Xna.Framework;

namespace BattleMech.PCL.Objects.Game.Base {
    public class BaseRenderable {
        public float PositionX;

        public float PositionY;

        public float OriginX;

        public float OriginY;

        public float AccelerationX;

        public float AccelerationY;

        public Guid ID;

        public BaseRenderable() {
            OriginX = 0.0f;
            OriginY = 0.0f;

            PositionX = 0.0f;
            PositionY = 0.0f;
        }

        public void Update() {
            OriginX += AccelerationX;
            OriginY += AccelerationY;
        }

        public Vector2 Origin => new Vector2(OriginX, OriginY);

        public Vector2 Position => new Vector2(PositionX, PositionY);
    }
}