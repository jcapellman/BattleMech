using System;
using System.Collections.Generic;
using System.Linq;

using BattleMech.PCL.Enums;
using BattleMech.PCL.Objects.Game.Base;

namespace BattleMech.PCL.Managers.Game {
    public class TexturableManager : BaseGameManager {
        private Dictionary<Guid, BaseTexturable> _items { get; }

        public List<BaseTexturable> Items => new List<BaseTexturable>(_items.Values);

        public TexturableManager() {
            _items = new Dictionary<Guid, BaseTexturable>();
        }

        public void AddItem(BaseTexturable item) {
            item.ID = Guid.NewGuid();

            _items.Add(item.ID, item);
        }

        public BaseTexturable GetItemByEnum(TEXTURABLE_ITEM_TYPES itemType) {
            return _items.Values.FirstOrDefault(a => a.ItemType == itemType);
        }

        public void UpdateItem(BaseTexturable item) {
            _items[item.ID] = item;
        }
    }
}