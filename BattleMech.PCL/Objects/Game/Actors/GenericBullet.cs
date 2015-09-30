using BattleMech.PCL.Enums;
using BattleMech.PCL.Objects.Game.Base;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleMech.PCL.Objects.Game.Actors
{
    public class GenericBullet : BaseTexturable
    {
        public GenericBullet(Texture2D texture) : base(texture, TEXTURABLE_ITEM_TYPES.ACTOR)
        {
            AccelerationX = 10.0f;
            PositionX = 400.0f;
            PositionY = 400.0f;
        }
    }
}
