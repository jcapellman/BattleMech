using BattleMech.PCL.Enums;

namespace BattleMech.PCL.Objects.Game {
    public class LevelObject {
        public string TextureName { get; set; }

        public int PositionX { get; set; }

        public int PositionY { get; set; }

        public TEXTURABLE_ITEM_TYPES Type { get; set; }
    }
}