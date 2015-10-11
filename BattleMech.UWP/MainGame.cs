using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml.Controls;
using BattleMech.DataLayer.PCL.Models.GameMetrics;
using BattleMech.PCL.Enums;
using BattleMech.PCL.Managers.Game;
using BattleMech.PCL.Objects.Game;
using BattleMech.WebAPI.PCL.Handlers;
using BattleMech.WebAPI.PCL.Transports.Internal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BattleMech.UWP {
    public class MainGame : Game {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        private TexturableManager _tm;

        public MainGame() {
            _graphics = new GraphicsDeviceManager(this);
           
            Content.RootDirectory = "Content";
            
        }

        private void AddHUD()
        {
            _tm.AddTextItem<Text>("GFX_FONTS/MainFont", "ENEMIES DESTROYED: 0", Color.White, new Vector2(20, 20));
            _tm.AddTextItem<Text>("GFX_FONTS/MainFont", "SCORE: 0", Color.White, new Vector2(500, 20));

        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _tm = new TexturableManager(Content, Window.ClientBounds.Width, Window.ClientBounds.Height);
            _tm.controller = new PCInputController();

            AddHUD();

            foreach (var levelObjects in App.CurrentLevel.Objects) {
                switch (levelObjects.AssetType) {
                    case ASSET_TYPES.GFX_BACKGROUND:
                        _tm.AddTextureItem<Background>(levelObjects.Filename, levelObjects.PositionX);
                        break;
                }
            }

            //add the player's selected character to the game
            _tm.AddTextureItem<Player>(App.PlayerAsset.Filename);
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