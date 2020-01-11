using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleMech.PCL.Interfaces
{
    public interface IAnimable
    {
        List<string> animations { get; set; }
        string currentAnimation { get; set; }
        int currentFrame { get; set; }
        int totalFrames { get; set; }
        bool loops { get; set; }
        double? duration { get; set; }

        void NextFrame();
        void PrevFrame();
    }
}
