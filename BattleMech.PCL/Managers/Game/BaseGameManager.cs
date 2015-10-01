using System;
using System.Collections.Generic;

using BattleMech.PCL.Objects.Game.Base;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using BattleMech.PCL.Objects.Game;

namespace BattleMech.PCL.Managers.Game {
    public abstract class BaseGameManager<T> where T : BaseRenderable {
        internal ContentManager _content;

        protected Dictionary<Guid, T> _items { get; }

        public BaseInputController controller;
        public List<T> Items => new List<T>(_items.Values);

        protected BaseGameManager(ContentManager content) {
            _items = new Dictionary<Guid, T>();

            _content = content;
        }

        /// <summary>
        /// Handles actually update the items
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            foreach (var item in Items)
            {
                item.Update(gameTime);
            }
        }

        public void UpdateItem(T item) {
            _items[item.ID] = item;
        }

        public virtual void AddItem(T item) {
            item.ID = Guid.NewGuid();

            _items.Add(item.ID, item);
        }
    }
}