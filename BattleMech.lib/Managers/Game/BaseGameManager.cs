using System;
using System.Collections.Generic;

using BattleMech.PCL.Objects.Game.Base;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using BattleMech.PCL.Objects.Game;
using System.Linq;

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
        /// Implements pool logic for getting renderable object
        /// </summary>
        /// <typeparam name="T">The object type to get</typeparam>
        /// <returns></returns>
        public List<BaseRenderable> GetRenderable<T>(int? limit = 1) where T : BaseRenderable
        {
            return _items.Values.Where(r => r.GetType() == typeof(T) && r.IsActive == false).Take(limit.Value).Cast<BaseRenderable>().ToList();

            //return _items.Values.FirstOrDefault(r => r.GetType() == typeof(T) && r.IsActive == false);
        }

        public virtual void DisposeItem(BaseRenderable item)
        {
            _items.Remove(item.ID);
        }

        /// <summary>
        /// Handles actually update the items
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual bool Update(GameTime gameTime) {
            foreach (var item in Items) {
                if(item.IsActive) item.Update(gameTime);
            }

            return true;
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