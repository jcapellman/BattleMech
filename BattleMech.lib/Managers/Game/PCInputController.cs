using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleMech.PCL.Managers.Game
{
    public class PCInputController : BaseInputController
    {
        protected KeyboardState _currentKeyboardState;

        public List<Keys> keys;

        public PCInputController()
        {

        }

        public override List<Keys> GetInput()
        {
            _currentKeyboardState = Keyboard.GetState();

            return _currentKeyboardState.GetPressedKeys().ToList();
        }
    }
}
