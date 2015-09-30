using BattleMech.PCL.Enums;
using BattleMech.PCL.Managers.Game;
using BattleMech.PCL.Objects.Game;
using BattleMech.PCL.Objects.Game.Actors;
using BattleMech.PCL.Objects.Game.Base;
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
            
            _tm.AddTextureItem<Background>("Blue_Stars.jpg");
            _tm.AddTextureItem<Player>("mech.png");
            _tm.AddTextureItem<Enemy>("enemy.png");
        }

        protected override void UnloadContent() {
            Content.Unload();
        }
        
        protected override void Update(GameTime gameTime) {
            _currentKeyboardState = Keyboard.GetState();

            var keys = _currentKeyboardState.GetPressedKeys();

            if (keys.Length == 0) {
                base.Update(gameTime);

                return;
            }
            
            var player = _tm.GetItemByEnum(TEXTURABLE_ITEM_TYPES.PLAYER);

            foreach (var key in keys) {
                switch (key) {
                    case Keys.A:
                        player.PositionX -= 7.0f;
                        break;
                    case Keys.D:
                        player.PositionX += 7.0f;
                        break;
                    case Keys.W:
                        player.PositionY -= 7.0f;
                        break;
                    case Keys.S:
                        player.PositionY += 7.0f;
                        break;
                    case Keys.Space:
                        BaseTexturable bullet = _tm.AddTextureItem<GenericBullet>("viper_blaster.png");
                        bullet.PositionX = player.PositionX + (player.Texture.Width / 2.0f);
                        bullet.PositionY = player.PositionY + (player.Texture.Height / 2.0f);
                        break;
                }
            }
           
            _tm.UpdateItem(player);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            foreach (var texturable in _tm.Items) {
                _spriteBatch.Draw(texturable.Texture, texturable.GetRectange(), Color.White);

                texturable.Update();
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}