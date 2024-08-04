using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sylladex.Graphics;
using Sylladex.Managers;
using System.Collections.Generic;

namespace Sylladex.UI
{

    /// <summary>
    /// Represents a window panel in the user interface.
    /// </summary>
    public class Window
    {
        private readonly Texture2D _texture;
        private int _width;
        private int _height;
        private List<UIElement> _children = new List<UIElement>();
        private bool _visible;
        public bool IsVisible
        {
            get => _visible;
            set => _visible = value;
        }
        public Vector2 Position;
        private float _opacity;
        public float Opacity
        {
            get => _opacity;
            set => _opacity = MathHelper.Clamp(value, 0f, 1f);
        }
        public LayerIndex LayerIndex;

        /// <summary>
        /// Creates a window with a specified width, height, and position.
        /// </summary>
        /// <param name="texture">The texture to be rendered on the window.</param>
        /// <param name="layerIndex">The layer index of the window.</param>
        /// <param name="width">The width of the window.</param>
        /// <param name="height">The height of the window.</param>
        /// <param name="position">The position of the window.</param>
        /// <param name="visible">Whether the window is visible or not.</param>
        /// <param name="opacity">The opacity of the window.</param>
        public Window(Texture2D texture, int layerIndex, int width, int height, Vector2 position, bool visible = false, float opacity = 1)
        {
            _texture = texture;
            _width = width;
            _height = height;
            Position = position;
            _visible = visible;
            LayerIndex = new LayerIndex(layerIndex);
            Opacity = opacity;
        }

        /// <summary>
        /// Creates a fullscreen window such as background.
        /// </summary>
        /// <param name="texture">The texture to be rendered on the window.</param>
        /// <param name="layerIndex">The layer index of the window.</param>
        /// <param name="visible">Whether the window is visible or not.</param>
        /// <param name="opacity">The opacity of the window.</param>
        public Window(Texture2D texture, int layerIndex, bool visible = false, float opacity = 1)
        {
            _texture = texture;
            _width = GameManager.Graphics.PreferredBackBufferWidth;
            _height = GameManager.Graphics.PreferredBackBufferHeight;
            Position = Vector2.Zero;
            LayerIndex = new LayerIndex(layerIndex);
            _visible = visible;
            Opacity = opacity;
        }

        /// <summary>
        /// Adds a child UI element to the window. It is intended for UI elements to call this method when they are created to bind themselves to the window.
        /// </summary>
        /// <param name="child">The UI element to add.</param>
        /// <returns>The index of the added child element.</returns>
        internal int AddChild(UIElement child)
        {
            _children.Add(child);
            return _children.Count;
        }

        /// <summary>
        /// Updates the window and its child elements.
        /// </summary>
        public void Update()
        {
            foreach (var child in _children)
            {
                child.Update();
            }
        }

        /// <summary>
        /// Draws the window and its child elements on the screen.
        /// </summary>
        public void Draw()
        {
            GameManager.SpriteBatch.Draw(
                _texture,
                new Rectangle((int)Position.X, (int)Position.Y, _width, _height),
                null,
                Color.White * Opacity,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                LayerIndex.Depth
            );
            foreach (var child in _children)
            {
                child.Draw();
            }
        }
    }
}
