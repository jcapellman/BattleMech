using System;
using System.Linq;

using BattleMech.PCL.Enums;
using BattleMech.PCL.Objects.Game.Base;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BattleMech.PCL.Managers.Game {
    public class TexturableManager : BaseGameManager<BaseTexturable> {
        private readonly int _windowHeight;
        private readonly int _windowWidth;

        public TexturableManager(ContentManager content) : base(content) { }

        public TexturableManager(ContentManager content, int windowWidth, int windowHeight) : this(content) {
            _windowWidth = windowWidth;
            _windowHeight = windowHeight;
        }

        public void AddTextureItem<T>(string textureName) where T : BaseTexturable {
            var item = (T) Activator.CreateInstance(typeof (T), _content.Load<Texture2D>(textureName));

            if (item.IsFullScreen) {
                item.PositionX = _windowWidth;
                item.PositionY = _windowHeight;
            }

            AddItem(item);
        }

        public BaseTexturable GetItemByEnum(TEXTURABLE_ITEM_TYPES itemType) {
            return _items.Values.FirstOrDefault(a => a.ItemType == itemType);
        }

        public new void UpdateItem(BaseTexturable item) {
            if (!item.BoundWithinScreen) {
                base.UpdateItem(item);

                return;
            }
            
            var newXPos = item.PositionX - item.Texture.Width / 2.0f;
            var newYPos = item.PositionY - item.Texture.Height / 2.0f;

            if (newXPos > 5 && newXPos <= _windowWidth - item.Texture.Width && newYPos > 10 && newYPos <= _windowHeight - item.Texture.Height - 200) {
                base.UpdateItem(item);                                
            }            
        }
    }
}