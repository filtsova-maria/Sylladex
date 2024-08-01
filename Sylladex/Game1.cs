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

    public class Player : Entity
    {
        public enum Direction
        {
            Left = -1,
            Right = 1,
            Up = -1,
            Down = 1
        }
        private float _speed = 150F;
        private Direction _directionX = Direction.Right;
        public Player(Vector2 pos)
        {
            Sprite = new Sprite(this, GameManager.TextureManager.GetObject("player"));
            DrawPosition = pos;
        }

        public override void Update()
        {

        }

        public void Move(Direction xDirection, Direction yDirection)
        {
            GameManager.SoundEffectManager.Play("footsteps");
            float elapsedTime = GameManager.TotalSeconds;
            float newX = DrawPosition.X + _speed * (int)xDirection * elapsedTime;
            float newY = DrawPosition.Y + _speed * (int)yDirection * elapsedTime;
            if (xDirection != 0)
            {
                _directionX = xDirection;
            }

            if (newX >= 0 && newX <= GameManager.Graphics.PreferredBackBufferWidth)
            {
                DrawPosition = new Vector2(newX, DrawPosition.Y);
            }

            if (newY >= 0 && newY <= GameManager.Graphics.PreferredBackBufferHeight)
            {
                DrawPosition = new Vector2(DrawPosition.X, newY);
            }
        }

        public override void Draw()
        {
            Sprite.Draw(_directionX == Direction.Left);
        }
    }

    public class Sprite
    {
        private readonly Texture2D _texture;
        private readonly Entity _owner;

        public Sprite(Entity owner, Texture2D texture)
        {
            _texture = texture;
            _owner = owner;
        }

        public void Draw(bool flipX=false)
        {
            GameManager.SpriteBatch.Draw(_texture, _owner.DrawPosition, null, Color.White, 0f, Vector2.Zero, Vector2.One, flipX ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0.5f);
        }
    }

    public abstract class Entity
    {
        public Vector2 DrawPosition { get; set; }
        public Sprite Sprite { get; init; }
        public abstract void Update();
        public abstract void Draw();
    }
    // TODO: animation manager
    // TODO: window manager, background, UI rendering
}
