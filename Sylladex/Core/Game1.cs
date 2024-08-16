using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Sylladex.Entities;
using Sylladex.Managers;
using Sylladex.UI;
using Sylladex.Graphics;

namespace Sylladex.Core
{
    public class Game1 : Game
    {
        public Game1()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            GameManager.Graphics = new GraphicsDeviceManager(this);
            GameManager.TextureManager = new TextureManager();
            GameManager.FontManager = new FontManager();
            GameManager.AnimationManager = new AnimationManager();
            GameManager.SoundEffectManager = new SoundEffectManager();
            GameManager.SoundtrackManager = new SoundtrackManager();
            GameManager.EntityManager = new EntityManager();
            GameManager.CanvasManager = new CanvasManager();
            GameManager.InputManager = new InputManager();
        }

        protected override void Initialize()
        {
            // Set the game window size
            GameManager.Graphics!.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2;
            GameManager.Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2;
            GameManager.Graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Load the textures
            GameManager.SpriteBatch = new SpriteBatch(GraphicsDevice);
            string[] textures = { "player", "playerMove", "floor", "controlDeck", "itemCard", "sword", "shield", "helmet", "potion", "settingsIcon", "pixelBase" };
            foreach (var texture in textures)
            {
                GameManager.TextureManager!.AddObject(texture, Content.Load<Texture2D>(texture));
            }
            string[] fonts = { "main" };
            foreach (var font in fonts)
            {
                GameManager.FontManager!.AddObject(font, Content.Load<SpriteFont>(font));
            }
            // Load the animations
            GameManager.AnimationManager!.AddObject("playerMove", new Animation(GameManager.TextureManager!.GetObject("playerMove"), 3, 1, 0.1f));
            // Load the soundtracks
            string[] soundTracks = { "game" };
            foreach (var soundTrack in soundTracks)
            {
                GameManager.SoundtrackManager!.AddObject(soundTrack, Content.Load<Song>(soundTrack));
            }
            // Load the sound effects
            string[] soundEffects = { "footsteps" };
            foreach (var soundEffect in soundEffects)
            {
                GameManager.SoundEffectManager!.AddObject(soundEffect, Content.Load<SoundEffect>(soundEffect));
            }
            // Load the UI windows and elements
            Canvas Background = new Canvas(GameManager.TextureManager.GetObject("floor"), 0, true);
            GameManager.CanvasManager!.AddObject("background", Background);
            Canvas HUD = new Canvas(GameManager.TextureManager.GetObject("pixelBase"), 60, GameManager.Graphics!.PreferredBackBufferWidth, 100, new Vector2(0, GameManager.Graphics.PreferredBackBufferHeight - 100), true, color: Color.Black, opacity: 0.5f);
            GameManager.CanvasManager.AddObject("inventoryHUD", HUD);
            new Label(GameManager.FontManager!.GetObject("main"), "Sylladex:", Color.White, backgroundOpacity: 0.5f).In(HUD).At(new Vector2(0, HUD.Height - GameManager.FontManager!.GetObject("main").LineSpacing));
            new Button(GameManager.TextureManager.GetObject("settingsIcon"), 50, 50).In(Background).At(new Vector2(GameManager.Graphics.PreferredBackBufferWidth - 50, 0));
            // Load the entities
            Vector2 initPlayerPosition = new Vector2(GameManager.Graphics.PreferredBackBufferWidth / 2, GameManager.Graphics.PreferredBackBufferHeight / 2);
            GameManager.EntityManager!.AddObject("player", new Player(initPlayerPosition));
            GameManager.EntityManager.AddObject("sword", new Item("Sword", GameManager.TextureManager.GetObject("sword"), new Vector2(100, 250)));
            GameManager.EntityManager.AddObject("shield", new Item("Shield", GameManager.TextureManager.GetObject("shield"), new Vector2(300, 250)));
            GameManager.EntityManager.AddObject("potion", new Item("Potion", GameManager.TextureManager.GetObject("potion"), new Vector2(500, 250)));
            GameManager.EntityManager.AddObject("helmet", new Item("Helmet", GameManager.TextureManager.GetObject("helmet"), new Vector2(700, 250)));
            // Load the input controls
            Player player = (Player)GameManager.EntityManager.GetObject("player");
            GameManager.InputManager!.AddObject(Keys.Up, () => player.Move(HorizontalDirection.None, VerticalDirection.Up));
            GameManager.InputManager.AddObject(Keys.Down, () => player.Move(HorizontalDirection.None, VerticalDirection.Down));
            GameManager.InputManager.AddObject(Keys.Left, () => player.Move(HorizontalDirection.Left, VerticalDirection.None));
            GameManager.InputManager.AddObject(Keys.Right, () => player.Move(HorizontalDirection.Right, VerticalDirection.None));
            // Play the game soundtrack
            GameManager.SoundtrackManager!.Play("game", true);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            GameManager.Update(gameTime);
            GameManager.EntityManager!.Update();
            GameManager.InputManager!.Update();
            GameManager.CanvasManager!.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            GameManager.SpriteBatch!.Begin(SpriteSortMode.FrontToBack);
            GameManager.EntityManager!.Draw();
            GameManager.CanvasManager!.Draw();
            GameManager.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
