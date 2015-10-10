using BattleMech.PCL.Managers.Game;
using BattleMech.PCL.Objects.Game;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BattleMech.UWP {
    public class MainGame : Game {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        private TexturableManager _tm;
        
        KeyboardState _currentKeyboardState;

        public MainGame() {
            _graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
        }
        
        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _tm = new TexturableManager(Content, Window.ClientBounds.Width, Window.ClientBounds.Height);
            _tm.controller = new PCInputController();

            _tm.AddTextureItem<Background>("Background/Blue_Stars.jpg");
            _tm.AddTextureItem<Player>("Player/mech.png");
        }

        protected override void UnloadContent() {
            Content.Unload();
        }
        
        protected override void Update(GameTime gameTime) {
            _tm.Update(gameTime);

            base.Update(gameTime);
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