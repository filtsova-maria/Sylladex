using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sylladex
{
    // TODO: implement interactive UI elements, inventory gui hud, settings
    // Find a way to render a plain rectangle from scratch with given color, size and opacity
    public class Window
    {
        private readonly Texture2D _texture;
        private int _width;
        private int _height;
        private bool _visible;
        public Vector2 Position;
        protected float Opacity = 1f;
        protected LayerIndex LayerIndex;

        /// <summary>
        /// Creates a window with a specified width, height, and position.
        /// </summary>
        /// <param name="texture">The texture to be rendered on the window.</param>
        /// <param name="layerIndex">The layer index of the window.</param>
        /// <param name="width">The width of the window.</param>
        /// <param name="height">The height of the window.</param>
        /// <param name="position">The position of the window.</param>
        /// <param name="visible">Whether the window is visible or not.</param>
        public Window(Texture2D texture, int layerIndex, int width, int height, Vector2 position, bool visible = false, float opacity=1)
        {
            _texture = texture;
            _width = width;
            _height = height;
            Position = position;
            _visible = visible;
            LayerIndex = new LayerIndex(layerIndex);
            SetOpacity(opacity);
        }

        /// <summary>
        /// Creates a fullscreen window such as background.
        /// </summary>
        /// <param name="texture">The texture to be rendered on the window.</param>
        /// <param name="layerIndex">The layer index of the window.</param>
        /// <param name="visible">Whether the window is visible or not.</param>
        public Window(Texture2D texture, int layerIndex, bool visible = false, float opacity=1)
        {
            _texture = texture;
            _width = GameManager.Graphics.PreferredBackBufferWidth;
            _height = GameManager.Graphics.PreferredBackBufferHeight;
            Position = Vector2.Zero;
            LayerIndex = new LayerIndex(layerIndex);
            _visible = visible;
            SetOpacity(opacity);
        }
        public void SetVisibility(bool visible)
        {
            _visible = visible;
        }
        public bool IsVisible() => _visible;
        public void SetOpacity(float opacity)
        {
            if (0 <= opacity && opacity <= 1)
            {
                Opacity = opacity;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(opacity), "Opacity must be between 0 and 1");
            }
        }
        public void Draw()
        {
            GameManager.SpriteBatch.Draw(
                _texture,
                new Rectangle((int)Position.X, (int)Position.Y, _width, _height),
                null,
                Color.White*Opacity,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                LayerIndex.Depth
            );
        }

    }
}
