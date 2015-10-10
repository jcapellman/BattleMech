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
using System.Diagnostics;

namespace BattleMech.PCL.Managers.Game {
    public class TexturableManager : BaseGameManager<BaseTexturable> {
        private readonly float _windowHeight;
        private readonly float _windowWidth;

        #region Helper Methods

        public bool IsOnScreen(Rectangle objRect)
        {
            if( (objRect.Right > 0 && objRect.Left < _windowWidth) &&
                (objRect.Bottom > 0 && objRect.Top < _windowHeight) )
            {
                return true;
            }

            return false;
        }

        #endregion

        public float WAVE_INTERVAL = 1000;
        public float waveTime;
        public float nextWave;

        public TexturableManager(ContentManager content) : base(content) { }

        public TexturableManager(ContentManager content, int windowWidth, int windowHeight) : this(content) {
            _windowWidth = windowWidth;
            _windowHeight = windowHeight;
        }

        /// <summary>
        /// Creates a new texture item that is interactable within the game world. Calls the BaseManager's GEt Renderable that recycles unused objects.
        /// </summary>
        /// <typeparam name="T">The object type to create</typeparam>
        /// <param name="textureName">The texture graphic to be used for the interable object</param>
        /// <returns></returns>
        public BaseTexturable AddTextureItem<T>(string textureName) where T : BaseTexturable {
            //get an available renderable of the type T
            var item = GetRenderable<T>().FirstOrDefault() as BaseTexturable;

            //create a new instance if there was not one available
            if (item == null){
                item = (T)Activator.CreateInstance(typeof(T), _content.Load<Texture2D>(textureName));

                if (item.IsFullScreen){
                    item.PositionX = (float)_windowWidth;
                    item.PositionY = (float)_windowHeight;
                }
                item.CheckOnScreen = IsOnScreen;
                AddItem(item);
            }else{
                item.Init(_content.Load<Texture2D>(textureName));
            }

            Debug.WriteLine($"Total Items: {_items.Count}");
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
                        BaseTexturable bullet = AddTextureItem<GenericBullet>("GFX_WEAPON/viper_blaster.png");
                        bullet.PositionX = player.PositionX + (player.Texture.Width / 2.0f);
                        bullet.PositionY = player.PositionY + (player.Texture.Height / 2.0f);
                        break;
                }
            }

            waveTime += gameTime.ElapsedGameTime.Milliseconds;
            if (waveTime >= nextWave)
            {
                //create and position new enemy
                var rand = new Random();
                var enemy = AddTextureItem<Enemy>("GFX_ENEMY/Gimp.png");
                enemy.PositionX = _windowWidth;
                enemy.PositionY = (((float)rand.NextDouble() * (_windowHeight - enemy.Texture.Height * 2) + enemy.Texture.Height));

                waveTime = 0;
                nextWave = WAVE_INTERVAL;

                Debug.WriteLine($"Created new enemy: id:{enemy.ID} | x: |{enemy.PositionX} | y:{enemy.PositionY}");
            }

            //prepare for collision detection between active enemies and projectiles

            var enemies = GetItemsByEnum(TEXTURABLE_ITEM_TYPES.ENEMY);
            var projectiles = GetItemsByEnum(TEXTURABLE_ITEM_TYPES.PROJECTILE);

            foreach (Enemy enemy in enemies)
            {
                if (!enemy.IsActive) continue;      //could have been made in-active during the collision detection process, so ignore it

                foreach (GenericBullet projectile in projectiles)
                {
                    if (!projectile.IsActive) continue;         //could have been made in-active during the collision detection process, so ignore it

                    if (enemy.GetCollision(projectile.GetRectange() ))
                    {
                        projectile.IsActive = false;
                        enemy.IsActive = false;
                    }
                }
            }

            base.Update(gameTime);

            //Debug.WriteLine($"DEBUG: Total Enmies: {enemies.Count()} | Total Projectiles: {projectiles.Count()}");
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