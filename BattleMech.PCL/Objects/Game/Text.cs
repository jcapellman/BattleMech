using BattleMech.PCL.Enums;
using BattleMech.PCL.Objects.Game.Base;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BattleMech.PCL.Objects.Game {
    public class Text : BaseTexturable {
        public SpriteFont sfMain;
        public string RenderText;
        public double FontSize;
        public Color FontColor;
        public Vector2 Position;

        public void Init(SpriteFont font, double fontSize, string text, Color fontColor, Vector2 posiition) {
            sfMain = font;
            FontSize = fontSize;
            RenderText = text;
            FontColor = fontColor;
            Position = posiition;
        }

        public void UpdateText(string renderText) {
            RenderText = renderText;
        }

        public override Rectangle GetRectange() {
            return new Rectangle();
        }

        public Text(Texture2D texture, TEXTURABLE_ITEM_TYPES itemType) : base(texture, itemType) {
            IsActive = true;
        }
    }
}