using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sylladex
{
    public class Window
    {
        private readonly Texture2D _texture;
        private int _width;
        private int _height;
        private bool _visible;
        public Vector2 Position;
        private int _layerIndex;
        private const int _numberOfLayers = 100;


        /// <summary>
        /// Creates a window with a specified width, height, and position.
        /// </summary>
        /// <param name="texture">The texture to be rendered on the window.</param>
        /// <param name="layerIndex">The layer index of the window.</param>
        /// <param name="width">The width of the window.</param>
        /// <param name="height">The height of the window.</param>
        /// <param name="position">The position of the window.</param>
        /// <param name="visible">Whether the window is visible or not.</param>
        public Window(Texture2D texture, int layerIndex, int width, int height, Vector2 position, bool visible = false)
        {
            _texture = texture;
            _width = width;
            _height = height;
            Position = position;
            _visible = visible;
            _layerIndex = layerIndex;
        }

        /// <summary>
        /// Creates a fullscreen window such as background.
        /// </summary>
        /// <param name="texture">The texture to be rendered on the window.</param>
        /// <param name="layerIndex">The layer index of the window.</param>
        /// <param name="visible">Whether the window is visible or not.</param>
        public Window(Texture2D texture, int layerIndex, bool visible = false)
        {
            _texture = texture;
            _width = GameManager.Graphics.PreferredBackBufferWidth;
            _height = GameManager.Graphics.PreferredBackBufferHeight;
            Position = Vector2.Zero;
            _layerIndex = layerIndex;
            _visible = visible;
        }
        private static float ConvertLayerIndexToDepth(int level) => (level / _numberOfLayers);
        public void SetVisibility(bool visible)
        {
            _visible = visible;
        }
        public bool IsVisible() => _visible;
        public void SetLayerIndex(int layerIndex)
        {
            if (0 <= layerIndex && layerIndex <= _numberOfLayers)
            {
                _layerIndex = layerIndex;
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Depth must be between 0 and {_numberOfLayers}");
            }
        }
        public void Draw()
        {
            GameManager.SpriteBatch.Draw(
                _texture,
                new Rectangle((int)Position.X, (int)Position.Y, _width, _height),
                null,
                Color.White,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                ConvertLayerIndexToDepth(_layerIndex)
            );
        }

    }
}
