﻿using System;
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
using BattleMech.DataLayer.PCL.Models.GameMetrics;
using BattleMech.PCL.Interfaces;
using BattleMech.DataLayer.PCL.Views.Assets;

namespace BattleMech.PCL.Managers.Game {
    public class TexturableManager : BaseGameManager<BaseTexturable> {
        private readonly float _windowHeight;
        private readonly float _windowWidth;

        public IClientStage stage;

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

        public PlayerCharacterGames Metrics;

        public TexturableManager(ContentManager content) : base(content) { }

        public TexturableManager(IClientStage stage, ContentManager content, int windowWidth, int windowHeight) : this(content) {
            _windowWidth = windowWidth;
            _windowHeight = windowHeight;
            this.stage = stage;

            Metrics = new PlayerCharacterGames {
                LevelID = 6,
                EnemiesDestroyed = 0,
                ExperienceGarnered = 0,
                ShotsFired = 0,
                TimesDied = 0,
                TotalEnemiesFought = 0
            };
        }

        public BaseTexturable AddTextItem<T>(string fontName, string renderText, Color fontColor, Vector2 position) where T : Text {
            var item = new Text(null, TEXTURABLE_ITEM_TYPES.TEXT);

            var font = _content.Load<SpriteFont>(fontName);

            item.Init(font, 14.0, renderText, fontColor, position);

            AddItem(item);

            return item;
        }

        /// <summary>
        /// Creates a new texture item that is interactable within the game world. Calls the BaseManager's GEt Renderable that recycles unused objects.
        /// </summary>
        /// <typeparam name="T">The object type to create</typeparam>
        /// <param name="textureName">The texture graphic to be used for the interable object</param>
        /// <returns></returns>
        //public BaseTexturable AddTextureItem<T>(string textureName, float? positionX = null) where T : BaseTexturable {
        //    //get an available renderable of the type T
        //    var item = GetRenderable<T>().FirstOrDefault() as BaseTexturable;

        //    //create a new instance if there was not one available
        //    if (item == null || typeof(T) == typeof(Background)){
        //        item = (T)Activator.CreateInstance(typeof(T), _content.Load<Texture2D>(textureName));

        //        if (item.IsFullScreen) {
        //            item.PositionX = positionX ?? _windowWidth;
        //            item.PositionY = (float)_windowHeight;
        //        }
                
        //        item.CheckOnScreen = IsOnScreen;
        //        AddItem(item);
        //    }else{
        //        item.Init(_content.Load<Texture2D>(textureName), Vector2.Zero);
        //    }

        //    Debug.WriteLine($"Total Items: {_items.Count}");
        //    return item;
        //}

        /// <summary>
        /// Creates a new texture item that is interactable within the game world. Calls the BaseManager's GEt Renderable that recycles unused objects.
        /// </summary>
        /// <typeparam name="T">The object type to create</typeparam>
        /// <param name="textureName">The texture graphic to be used for the interable object</param>
        /// <returns></returns>
        public BaseTexturable AddTextureItem<T>(List<ActiveAssetsVIEW> assetInfos, float? positionX = null, float? positionY = null) where T : BaseTexturable
        {
            //get an available renderable of the type T
            var item = GetRenderable<T>().FirstOrDefault() as BaseTexturable;

            //create a new instance if there was not one available
            if (item == null || typeof(T) == typeof(Background))
            {
                item = (T)Activator.CreateInstance(typeof(T), _content, assetInfos);

                if (item.IsFullScreen)
                {
                    item.Width = _windowWidth;
                    item.Height = _windowHeight;
                    item.PositionX = positionX ?? 0;
                    item.PositionY = positionY ?? 0;
                }

                item.CheckOnScreen = IsOnScreen;
                AddItem(item);
            }
            else
            {
                item.Init(assetInfos, Vector2.Zero);
            }

            //Debug.WriteLine($"Total Items: {_items.Count}");
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
        public override bool Update(GameTime gameTime)
        {
            var keys = controller.GetInput();
            Player player = GetItemByEnum(TEXTURABLE_ITEM_TYPES.PLAYER) as Player;
            var i = _items;
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
                        BaseTexturable bullet = AddTextureItem<GenericBullet>(stage.GetAssetInfo(10));
                        bullet.PositionX = player.PositionX + (player.Texture.Width / 2.0f);
                        bullet.PositionY = player.PositionY + (player.Texture.Height / 2.0f);

                        Metrics.ShotsFired++;
                        break;
                    case Keys.Escape:
                        return false;
                }
            }
            
            waveTime += gameTime.ElapsedGameTime.Milliseconds;
            if (waveTime >= nextWave)
            {
                //create and position new enemy
                var rand = new Random();
                var enemy = AddTextureItem<Enemy>(stage.GetAssetInfo(4));
                enemy.PositionX = _windowWidth;
                enemy.PositionY = (((float)rand.NextDouble() * (_windowHeight - enemy.Texture.Height * 2) + enemy.Texture.Height));

                waveTime = 0;
                nextWave = WAVE_INTERVAL;

                //Debug.WriteLine($"Created new enemy: id:{enemy.ID} | x: |{enemy.PositionX} | y:{enemy.PositionY}");

                Metrics.TotalEnemiesFought++;
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

                        var visEff =  (VisualEffect)AddTextureItem<VisualEffect>(stage.GetAssetInfo(25)); //TODO: Pass object type to client from bullet to determine asset needed
                        visEff.PositionX = enemy.PositionX;
                        visEff.PositionY = enemy.PositionY;
                        visEff.Start();

                        Metrics.EnemiesDestroyed++;
                        Metrics.ExperienceGarnered += 100;
                    }
                }
            }

            var text = GetItemsByEnum(TEXTURABLE_ITEM_TYPES.TEXT);

            foreach (var item in text) {
                var tItem = (Text) item;

                if (tItem.RenderText.StartsWith("ENEMIES")) {
                    tItem.UpdateText($"ENEMIES DESTROYED: {Metrics.EnemiesDestroyed}");
                } else if (tItem.RenderText.StartsWith("SCORE")) {
                    tItem.UpdateText($"SCORE: {Metrics.EnemiesDestroyed * 100}");
                }
            }

            //check to reposition the background
            var backgrounds = GetItemsByEnum(TEXTURABLE_ITEM_TYPES.BACKGROUND);

            foreach (var bkgd in backgrounds) {
                if (bkgd.Position.X <= -_windowWidth) {
                    bkgd.PositionX = _windowWidth + (bkgd.PositionX + _windowWidth);
                }
            }

            base.Update(gameTime);

            return true;
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