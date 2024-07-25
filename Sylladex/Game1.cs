using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sylladex
{
    public class Game1 : Game
    {
        Texture2D ballTexture;
        Texture2D floorTexture;
        float ballSpeed;
        Vector2 ballPosition;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            // Set the window size
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            ballSpeed = 150F;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ballTexture = Content.Load<Texture2D>("ball");
            floorTexture = Content.Load<Texture2D>("floor");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            HandleInput(gameTime);

            base.Update(gameTime);
        }

        private void HandleInput(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();

            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Up))
            {
                MoveBall(0, -1, elapsedTime);
            }

            if (kstate.IsKeyDown(Keys.Down))
            {
                MoveBall(0, 1, elapsedTime);
            }

            if (kstate.IsKeyDown(Keys.Left))
            {
                MoveBall(-1, 0, elapsedTime);
            }

            if (kstate.IsKeyDown(Keys.Right))
            {
                MoveBall(1, 0, elapsedTime);
            }
        }

        private void MoveBall(int xDirection, int yDirection, float elapsedTime)
        {
            float newX = ballPosition.X + ballSpeed * xDirection * elapsedTime;
            float newY = ballPosition.Y + ballSpeed * yDirection * elapsedTime;

            if (newX >= 0 && newX <= _graphics.PreferredBackBufferWidth - ballTexture.Width)
            {
                ballPosition.X = newX;
            }

            if (newY >= 0 && newY <= _graphics.PreferredBackBufferHeight - ballTexture.Height)
            {
                ballPosition.Y = newY;
            }
        }
       

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _spriteBatch.Draw(floorTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
            _spriteBatch.Draw(ballTexture, ballPosition, null, Color.White, 0f, new Vector2(ballTexture.Width / 2, ballTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
