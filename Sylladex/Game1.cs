﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

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
            GameManager.WindowManager = new WindowManager();
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
            // Load the textures
            GameManager.SpriteBatch = new SpriteBatch(GraphicsDevice);
            string[] textures = { "player", "playerMove", "floor", "controlDeck", "itemCard", "settingsIcon" };
            foreach (var texture in textures)
            {
                GameManager.TextureManager.AddObject(texture, Content.Load<Texture2D>(texture));
            }
            // Load the animations
            GameManager.AnimationManager.AddObject("playerMove", new Animation(GameManager.TextureManager.GetObject("playerMove"), 3, 1, 0.1f));
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
            // Load the UI elements
            GameManager.WindowManager.AddObject("background", new Window(Content.Load<Texture2D>("floor"), 0, true));
            // Load the entities
            Vector2 initPlayerPosition = new Vector2(GameManager.Graphics.PreferredBackBufferWidth / 2, GameManager.Graphics.PreferredBackBufferHeight / 2);
            GameManager.EntityManager.AddObject("player", new Player(initPlayerPosition));
            // Load the input controls
            Player player = (Player)GameManager.EntityManager.GetObject("player");
            GameManager.InputManager.AddObject(Keys.Up, () => player.Move(0, Direction.Up));
            GameManager.InputManager.AddObject(Keys.Down, () => player.Move(0, Direction.Down));
            GameManager.InputManager.AddObject(Keys.Left, () => player.Move(Direction.Left, 0));
            GameManager.InputManager.AddObject(Keys.Right, () => player.Move(Direction.Right, 0));
            // Play the game soundtrack
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
            GameManager.WindowManager.Draw();
            GameManager.EntityManager.Draw();
            GameManager.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
