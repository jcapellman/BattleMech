using System;
using System.Collections.Generic;
using System.Linq;

using BattleMech.PCL.Enums;
using BattleMech.PCL.Objects.Game.Base;

namespace BattleMech.PCL.Managers.Game {
    public class TexturableManager : BaseGameManager {
        public List<BaseTexturable> Items { get; }

        public TexturableManager() {
            Items = new List<BaseTexturable>();
        }

        public void AddItem(BaseTexturable item) {
            item.ID = Guid.NewGuid();

            Items.Add(item);
        }

        public BaseTexturable GetItemByEnum(TEXTURABLE_ITEM_TYPES itemType) {
            return Items.FirstOrDefault(a => a.ItemType == itemType);
        }

        public void UpdateItem(BaseTexturable item) {
            var index = Items.FindIndex(a => a.ID == item.ID);

            Items[index] = item;
        }
    }
}