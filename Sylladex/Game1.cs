using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using static Sylladex.Player;

namespace Sylladex
{
    public class Game1 : Game
    {
        public Game1()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            GameManager.Graphics = new GraphicsDeviceManager(this);
            GameManager.TextureManager = new TextureManager();
            GameManager.AnimationManager = new AnimationManager();
            GameManager.SoundEffectManager = new SoundEffectManager();
            GameManager.SoundtrackManager = new SoundtrackManager();
            GameManager.EntityManager = new EntityManager();
            GameManager.InputManager = new InputManager();
        }

        protected override void Initialize()
        {
            // Set the window size
            GameManager.Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2;
            GameManager.Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2;
            GameManager.Graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            GameManager.SpriteBatch = new SpriteBatch(GraphicsDevice);
            string[] textures = { "player", "floor", "controlDeck", "itemCard", "settingsIcon" };
            foreach (var texture in textures)
            {
                GameManager.TextureManager.AddObject(texture, Content.Load<Texture2D>(texture));
            }

            string[] soundTracks = { "game" };
            foreach (var soundTrack in soundTracks)
            {
                GameManager.SoundtrackManager.AddObject(soundTrack, Content.Load<Song>(soundTrack));
            }

            string[] soundEffects = { "footsteps" };
            foreach (var soundEffect in soundEffects)
            {
                GameManager.SoundEffectManager.AddObject(soundEffect, Content.Load<SoundEffect>(soundEffect));
            }

            Vector2 initPlayerPosition = new Vector2(GameManager.Graphics.PreferredBackBufferWidth / 2, GameManager.Graphics.PreferredBackBufferHeight / 2);

            GameManager.EntityManager.AddObject("player", new Player(initPlayerPosition));

            Player player = (Player)GameManager.EntityManager.GetObject("player");
            GameManager.InputManager.AddObject(Keys.Up, () => player.Move(0, Direction.Up));
            GameManager.InputManager.AddObject(Keys.Down, () => player.Move(0, Direction.Down));
            GameManager.InputManager.AddObject(Keys.Left, () => player.Move(Direction.Left, 0));
            GameManager.InputManager.AddObject(Keys.Right, () => player.Move(Direction.Right, 0));

            GameManager.SoundtrackManager.Play("game", true);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            GameManager.Update(gameTime);
            GameManager.EntityManager.Update();
            GameManager.InputManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            GameManager.SpriteBatch.Begin();
            //GameManager.SpriteBatch.Draw(floorTexture, new Rectangle(0, 0, GameManager.Graphics.PreferredBackBufferWidth, GameManager.Graphics.PreferredBackBufferHeight), Color.White);
            GameManager.EntityManager.Draw();
            GameManager.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
    // TODO: animation manager, similar to SoundEffectManager manager
    // TODO: window manager, background, UI rendering
}
