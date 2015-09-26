using System;
using System.Collections.Generic;
using BattleMech.PCL.Objects.Game.Base;
using Microsoft.Xna.Framework.Content;

namespace BattleMech.PCL.Managers.Game {
    public class BaseGameManager<T> where T : BaseRenderable {
        internal ContentManager _content;

        protected Dictionary<Guid, T> _items { get; }

        public List<T> Items => new List<T>(_items.Values);

        public BaseGameManager(ContentManager content) {
            _items = new Dictionary<Guid, T>();

            _content = content;
        }

        public void UpdateItem(T item) {
            _items[item.ID] = item;
        }

        internal void AddItem(T item) {
            item.ID = Guid.NewGuid();

            _items.Add(item.ID, item);
        }
    }
}