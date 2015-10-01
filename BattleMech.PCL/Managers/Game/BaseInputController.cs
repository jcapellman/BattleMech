using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleMech.PCL.Managers.Game
{
    public abstract class BaseInputController
    {
        public abstract List<Keys> GetInput();
    }
}
