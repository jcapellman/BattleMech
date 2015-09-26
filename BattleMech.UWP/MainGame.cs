using BattleMech.PCL.Enums;
using BattleMech.PCL.Managers.Game;
using BattleMech.PCL.Objects.Game;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BattleMech.UWP {
    public class MainGame : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private TexturableManager _tm;
        
        KeyboardState currentKeyboardState;

        public MainGame() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _tm = new TexturableManager(Content);
            
            _tm.AddTextureItem<Background>("background.jpg");
            _tm.AddTextureItem<Player>("mech.png");
        }

        protected override void UnloadContent() {
            Content.Unload();
        }
        
        protected override void Update(GameTime gameTime) {
            currentKeyboardState = Keyboard.GetState();

            var player = _tm.GetItemByEnum(TEXTURABLE_ITEM_TYPES.PLAYER);

            if (currentKeyboardState.IsKeyDown(Keys.W)) {
                player.OriginY += 7.0f;
            } else if (currentKeyboardState.IsKeyDown(Keys.S)) {
                player.OriginY -= 7.0f;
            } else if (currentKeyboardState.IsKeyDown(Keys.A)) {
                player.OriginX += 7.0f;
            } else if (currentKeyboardState.IsKeyDown(Keys.D)) {
                player.OriginX -= 7.0f;
            }

            _tm.UpdateItem(player);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            foreach (var texturable in _tm.Items) {
                spriteBatch.Draw(texturable.Texture, texturable.Position, origin: texturable.Origin);

                texturable.Update();
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}