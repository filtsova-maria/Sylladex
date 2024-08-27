using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Sylladex.Entities;
using Sylladex.Managers;
using Sylladex.UI;
using Sylladex.Graphics;
using System.Collections.Generic;

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
            GameManager.CollisionManager = new CollisionManager();
        }

        protected override void Initialize()
        {
            // Set the game window size
            GameManager.Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2;
            GameManager.Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2;
            GameManager.Graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Load the textures
            GameManager.SpriteBatch = new SpriteBatch(GraphicsDevice);
            string[] textures = { "player", "playerMove", "floor", "controlDeck", "itemCard", "Sword", "Shield", "Helmet", "Potion", "settingsIcon", "pixelBase", "cross", "eButton" };
            foreach (var texture in textures)
            {
                GameManager.TextureManager.AddObject(texture, Content.Load<Texture2D>(texture));
            }
            string[] fonts = { "main" };
            foreach (var font in fonts)
            {
                GameManager.FontManager.AddObject(font, Content.Load<SpriteFont>(font));
            }
            // Load the animations
            GameManager.AnimationManager.AddObject("playerMove", new Animation(GameManager.TextureManager!.GetObject("playerMove"), 3, 1, 0.1f));
            // Load the soundtracks
            string[] soundTracks = { "game" };
            foreach (var soundTrack in soundTracks)
            {
                GameManager.SoundtrackManager.AddObject(soundTrack, Content.Load<Song>(soundTrack));
            }
            // Load the sound effects
            string[] soundEffects = { "footsteps" };
            foreach (var soundEffect in soundEffects)
            {
                GameManager.SoundEffectManager.AddObject(soundEffect, Content.Load<SoundEffect>(soundEffect));
            }
            // Load the UI windows and elements
            Canvas Background = new Canvas(GameManager.TextureManager.GetObject("floor"), 0, true);
            GameManager.CanvasManager.AddObject("background", Background);
            Canvas HUD = new Canvas(GameManager.TextureManager.GetObject("pixelBase"), 51, GameManager.Graphics!.PreferredBackBufferWidth, 100, Vector2.Zero, alignment: Alignment.BottomLeft, visible: true, color: Color.Black, opacity: 0.5f);
            GameManager.CanvasManager.AddObject("inventoryHUD", HUD);
            new Button(GameManager.TextureManager.GetObject("settingsIcon"), 50, 50, () => GameManager.CanvasManager.ShowCanvas("settingsMenu"))
                .In(Background)
                .At(Vector2.Zero, Alignment.TopRight);
            Canvas SettingsMenu = new Canvas(GameManager.TextureManager.GetObject("pixelBase"), 52, 500, 500, Vector2.Zero, alignment: Alignment.Center);
            GameManager.CanvasManager.AddObject("settingsMenu", SettingsMenu);
            new Button(GameManager.TextureManager.GetObject("cross"), 50, 50, () => GameManager.CanvasManager.HideCanvas("settingsMenu"), hoverColor: Color.Red)
                .In(SettingsMenu)
                .At(Vector2.Zero, Alignment.TopRight);
            int CardWidth = GameManager.TextureManager.GetObject("itemCard").Width;
            int CardPadding = 10;
            DisplayLayout layout = new DisplayLayout(new Vector2(-(SylladexManager.NumberOfCards * (CardWidth + CardPadding)) / 2, 0), SylladexManager.NumberOfCards, CardWidth, CardPadding);
            List<SylladexCard> cards = new List<SylladexCard>();
            foreach (var position in layout.Positions)
            {
                cards.Add(new SylladexCard(null, Color.White)
                    .In(HUD)
                    .At(position, Alignment.Center));
            }
            GameManager.SylladexManager = new SylladexManager(cards);
            new Label(GameManager.FontManager.GetObject("main"), $"Sylladex:{GameManager.SylladexManager.FetchModus.Name}::{GameManager.SylladexManager.InsertModus.Name}::{GameManager.SylladexManager.DimensionModus.Name}", textColor: Color.White, backgroundOpacity: 0f)
                .In(HUD)
                .At(Vector2.Zero);
            // TODO: add buttons to select sylladex mode and customize it
            // TODO: create some label reference to be updated by the sylladex manager on change
            // Load the entities
            Vector2 initPlayerPosition = new Vector2(GameManager.Graphics.PreferredBackBufferWidth / 2, GameManager.Graphics.PreferredBackBufferHeight / 2);
            GameManager.EntityManager.AddObject("player", new Player(initPlayerPosition));
            GameManager.EntityManager.AddObject("Sword", new Item("Sword", GameManager.TextureManager.GetObject("Sword"), new Vector2(100, 250)));
            GameManager.EntityManager.AddObject("Shield", new Item("Shield", GameManager.TextureManager.GetObject("Shield"), new Vector2(300, 250)));
            GameManager.EntityManager.AddObject("Potion", new Item("Potion", GameManager.TextureManager.GetObject("Potion"), new Vector2(500, 250)));
            GameManager.EntityManager.AddObject("Helmet", new Item("Helmet", GameManager.TextureManager.GetObject("Helmet"), new Vector2(700, 250)));
            // Load the input controls
            Player player = (Player)GameManager.EntityManager.GetObject("player");
            GameManager.InputManager.AddAction(Keys.W, () => player.Move(HorizontalDirection.None, VerticalDirection.Up));
            GameManager.InputManager.AddAction(Keys.S, () => player.Move(HorizontalDirection.None, VerticalDirection.Down));
            GameManager.InputManager.AddAction(Keys.A, () => player.Move(HorizontalDirection.Left, VerticalDirection.None));
            GameManager.InputManager.AddAction(Keys.D, () => player.Move(HorizontalDirection.Right, VerticalDirection.None));
            // Play the game soundtrack
            GameManager.SoundtrackManager.Play("game", true);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            GameManager.Update(gameTime);
            GameManager.EntityManager.Update();
            GameManager.CanvasManager.Update();
            GameManager.InputManager.Update();
            GameManager.SylladexManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            GameManager.SpriteBatch.Begin(SpriteSortMode.FrontToBack);
            GameManager.EntityManager.Draw();
            GameManager.CanvasManager.Draw();
            GameManager.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
