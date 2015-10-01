using BattleMech.PCL.Enums;
using BattleMech.PCL.Objects.Game.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace BattleMech.PCL.Objects.Game {
    public enum MOVEMENT_DIRECTION
    {
        LEFT,
        UP,
        RIGHT,
        DOWN
    }

    public class Player : BaseTexturable {
        private readonly float MAX_SPEED = 10.0f;
        private readonly float STOP_THRESHOLD = 0.01f;  //threshold to actually stop the player

        public float EaseMotion = 0.90f;

        public float Acceleration = 2.0f;

        public Player(Texture2D texture) : base(texture, TEXTURABLE_ITEM_TYPES.PLAYER) {
            PositionX = 200.0f;
            PositionY = 200.0f;

            MoveX = 0.0f;
            MoveY = 0.0f;

            BoundWithinScreen = true;
        }

        /// <summary>
        /// Update the player mechanics
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //apply easing slow down motion
            MoveX *= EaseMotion;
            MoveY *= EaseMotion;

            //determine to stop the player
            if (MoveX > -STOP_THRESHOLD && MoveX < STOP_THRESHOLD) MoveX = 0.0f;
            if (MoveY > -STOP_THRESHOLD && MoveY < STOP_THRESHOLD) MoveY = 0.0f;

            base.Update(gameTime);
        }

        public void Accelerate(MOVEMENT_DIRECTION direction)
        {
            switch(direction)
            {
                case MOVEMENT_DIRECTION.LEFT:
                    MoveX -= Acceleration;
                    break;
                case MOVEMENT_DIRECTION.RIGHT:
                    MoveX += Acceleration;
                    break;
                case MOVEMENT_DIRECTION.UP:
                    MoveY -= Acceleration;
                    break;
                case MOVEMENT_DIRECTION.DOWN:
                    MoveY += Acceleration;
                    break;
            }

            //constriant the player's speed
            if (MoveX > MAX_SPEED) MoveX = MAX_SPEED;
            else if (MoveX < -MAX_SPEED) MoveX = -MAX_SPEED;

            if (MoveY > MAX_SPEED) MoveY = MAX_SPEED;
            else if (MoveY < -MAX_SPEED) MoveY = -MAX_SPEED;
        }
    }
}