using System;
using System.Linq;

using BattleMech.PCL.Enums;
using BattleMech.PCL.Objects.Game.Base;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BattleMech.PCL.Managers.Game {
    public class TexturableManager : BaseGameManager<BaseTexturable> {
        public TexturableManager(ContentManager content) : base(content) { }

        public void AddTextureItem<T>(string textureName) where T : BaseTexturable {
            AddItem((T)Activator.CreateInstance(typeof(T), _content.Load<Texture2D>(textureName)));
        }

        public BaseTexturable GetItemByEnum(TEXTURABLE_ITEM_TYPES itemType) {
            return _items.Values.FirstOrDefault(a => a.ItemType == itemType);
        }
    }
}