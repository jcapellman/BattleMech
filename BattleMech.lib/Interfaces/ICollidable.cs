using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace BattleMech.PCL.Interfaces
{
    public interface ICollidable
    {
        bool IsColliding { get; set; }

        bool GetCollision(Rectangle other);
    }
}
