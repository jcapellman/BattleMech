using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml.Controls;
using BattleMech.DataLayer.PCL.Models.GameMetrics;
using BattleMech.PCL.Enums;
using BattleMech.PCL.Interfaces;
using BattleMech.PCL.Managers.Game;
using BattleMech.PCL.Objects.Game;
using BattleMech.WebAPI.PCL.Handlers;
using BattleMech.WebAPI.PCL.Transports.Internal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Windows.Storage;
using System;
using Windows.Storage.Streams;
using BattleMech.DataLayer.PCL.Views.Assets;
using System.Collections.Generic;

namespace BattleMech.UWP {
    public class MainGame : Game, IClientStage {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        private TexturableManager _tm;

        public MainGame() {
            _graphics = new GraphicsDeviceManager(this);
           
            Content.RootDirectory = "Content";
            
        }

        private void AddHUD()
        {
            string font = "GFX_FONTS/MainFont";
            _tm.AddTextItem<Text>(font, "ENEMIES DESTROYED: 0", Color.White, new Vector2(20, 20));
            _tm.AddTextItem<Text>(font, "SCORE: 0", Color.White, new Vector2(500, 20));

        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _tm = new TexturableManager(this, Content, Window.ClientBounds.Width, Window.ClientBounds.Height);
            _tm.controller = new PCInputController();

            AddHUD();

            foreach (var levelObjects in App.CurrentLevel.Objects) {
                switch ((ASSET_TYPES)levelObjects.AssetInfos.FirstOrDefault().AssetTypeID) {
                    case ASSET_TYPES.GFX_BACKGROUND:
                        _tm.AddTextureItem<Background>(levelObjects.AssetInfos, levelObjects.PositionX);
                        break;
                }
            }

            //string fileContent;
            //StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Content/GFX_EFFECTS/green_explosion.json"));

            //var uri = new Uri("ms-appx:///Content/GFX_EFFECTS/green_explosion.json");
            //var btyes = File.ReadAllBytes(uri.ToString());

            //using (StreamReader reader = new StreamReader(await file.OpenStreamForReadAsync()))
            //{
            //    fileContent = await reader.ReadToEndAsync();
            //}


                //IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);
                //DataReader reader = new DataReader(fileStream);

                //var bytes = new byte[fileStream.Size];
                ////reader.ReadBytes(bytes);
                //var blah = reader.ReadString((uint)fileStream.Size-1);

                //AtlasHandler.Instance.CacheAtlas(bytes, bytes);
                //add the player's selected character to the game
                _tm.AddTextureItem<Player>(new List<ActiveAssetsVIEW>() { App.PlayerAsset } );
        }
        
        private async void recordGameMetric() {
            var gmHandler = new GameMetricHandler(new HandlerItem { Token = App.Token, WEBAPIUrl = "http://battlemech.azurewebsites.net/api/" });
            
            await gmHandler.AddGameMetric(_tm.Metrics);
        }

        private bool _exiting = false;

        protected override void UnloadContent()
        {
            this.Dispose();
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime) {
            if (_exiting)
            {
                SuppressDraw();
                return;
            }

            var isNotDone =_tm.Update(gameTime);

            if (isNotDone) {
                base.Update(gameTime);

                return;
            }

            _exiting = true;
            SuppressDraw();

            recordGameMetric();

            IsMouseVisible = true;

            var frame = new Frame();
            frame.Navigate(typeof(MainMenuPage));

            Windows.UI.Xaml.Window.Current.Content = frame;
            Windows.UI.Xaml.Window.Current.Activate();
        }

        /// <summary>
        /// Gets all asset views associated with the ID provided. This can return a single image or a sequence of images.
        /// </summary>
        /// <param name="assestID"></param>
        /// <returns></returns>
        public List<ActiveAssetsVIEW> GetAssetInfo(int assestID)
        {
            return App.Assets.Where(a => a.ID == assestID).ToList();
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            foreach (var texturable in _tm.Items.Where(a => a.IsActive).OrderBy(b => b.ItemType)) {
                switch (texturable.ItemType) {
                    case TEXTURABLE_ITEM_TYPES.TEXT:
                        var item = (Text) texturable;

                        _spriteBatch.DrawString(item.sfMain, item.RenderText, item.Position, item.FontColor);
                        break;
                    default:
                        _spriteBatch.Draw(texturable.Texture, texturable.GetRectange(), Color.White);
                        break;
                }
            }

            _spriteBatch.End();
           
            base.Draw(gameTime);
        }
    }
}