﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleMech.PCL.Interfaces
{
    public interface ICollidable
    {
        bool IsColliding { get; set; }

        bool GetCollision(Rectangle other);
    }
}
