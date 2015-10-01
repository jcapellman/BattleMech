using System;
using System.Linq;

using BattleMech.PCL.Enums;
using BattleMech.PCL.Objects.Game.Base;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using BattleMech.PCL.Objects.Game;
using Microsoft.Xna.Framework.Input;
using BattleMech.PCL.Objects.Game.Actors;

namespace BattleMech.PCL.Managers.Game {
    public class TexturableManager : BaseGameManager<BaseTexturable> {
        private readonly int _windowHeight;
        private readonly int _windowWidth;

        public TexturableManager(ContentManager content) : base(content) { }

        public TexturableManager(ContentManager content, int windowWidth, int windowHeight) : this(content) {
            _windowWidth = windowWidth;
            _windowHeight = windowHeight;
        }

        public BaseTexturable AddTextureItem<T>(string textureName) where T : BaseTexturable {
            var item = (T) Activator.CreateInstance(typeof (T), _content.Load<Texture2D>(textureName));

            if (item.IsFullScreen) {
                item.PositionX = _windowWidth;
                item.PositionY = _windowHeight;
            }

            AddItem(item);
            return item;
        }

        public BaseTexturable GetItemByEnum(TEXTURABLE_ITEM_TYPES itemType) {
            return _items.Values.FirstOrDefault(a => a.ItemType == itemType);
        }

        public List<BaseTexturable> GetItemsByEnum(TEXTURABLE_ITEM_TYPES itemType)
        {
            return _items.Values.Where(a => a.ItemType == itemType).ToList();
        }

        /// <summary>
        /// Prepares game texturables to be updated by game manager
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            var keys = controller.GetInput();
            Player player = GetItemByEnum(TEXTURABLE_ITEM_TYPES.PLAYER) as Player;

            foreach (var key in keys)
            {
                switch (key)
                {
                    case Keys.A:
                        player.Accelerate(MOVEMENT_DIRECTION.LEFT);
                        break;
                    case Keys.D:
                        player.Accelerate(MOVEMENT_DIRECTION.RIGHT);
                        break;
                    case Keys.W:
                        player.Accelerate(MOVEMENT_DIRECTION.UP);
                        break;
                    case Keys.S:
                        player.Accelerate(MOVEMENT_DIRECTION.DOWN);
                        break;
                    case Keys.Space:
                        BaseTexturable bullet = AddTextureItem<GenericBullet>("viper_blaster.png");
                        bullet.PositionX = player.PositionX + (player.Texture.Width / 2.0f);
                        bullet.PositionY = player.PositionY + (player.Texture.Height / 2.0f);
                        break;
                }
            }

            base.Update(gameTime);
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