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
        
        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _tm = new TexturableManager(Content, Window.ClientBounds.Width, Window.ClientBounds.Height);
            _tm.controller = new PCInputController();
            
            foreach (var levelObjects in App.CurrentLevel.Objects) {
                switch (levelObjects.AssetType) {
                    case ASSET_TYPES.GFX_BACKGROUND:
                        _tm.AddTextureItem<Background>(levelObjects.Filename);
                        break;
                }
            }

            //add the player's selected character to the game
            _tm.AddTextureItem<Player>(App.PlayerAsset.Filename);
        }
        
        private async void recordGameMetric() {
            var gmHandler = new GameMetricHandler(new HandlerItem { Token = App.Token, WEBAPIUrl = "http://battlemech.azurewebsites.net/api/" });

            
            
            var result = await gmHandler.AddGameMetric(_tm.Metrics);

            if (result.HasError)
            {
                Debug.WriteLine(result.Exception);
            }
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
            
            var frame = new Frame();
            frame.Navigate(typeof(MainMenuPage));

            Windows.UI.Xaml.Window.Current.Content = frame;
            Windows.UI.Xaml.Window.Current.Activate();
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            foreach (var texturable in _tm.Items) {

                if(texturable.IsActive) _spriteBatch.Draw(texturable.Texture, texturable.GetRectange(), Color.White);
            }

            _spriteBatch.End();
           
            base.Draw(gameTime);
        }
    }
}