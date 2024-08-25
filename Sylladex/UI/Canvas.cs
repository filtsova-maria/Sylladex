using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sylladex.Graphics;
using Sylladex.Managers;
using System.Collections.Generic;

namespace Sylladex.UI
{

    /// <summary>
    /// Represents a canvas panel in the user interface.
    /// </summary>
    public class Canvas : IContainer, IRenderable
    {
        /// <summary>
        /// Gets or sets the element texture to be rendered on the canvas.
        /// </summary>
        public Texture2D Texture { get; init; }

        /// <summary>
        /// Gets or sets the tint color of the canvas, default is <c>Color.White</c> (no tint).
        /// </summary>
        public Color? Tint { get; set; }

        private int _width;
        private int _height;

        public int Width => _width;
        public int Height => _height;

        // Stores the child elements of the canvas and their absolute positions.
        private readonly Dictionary<UIElement, Vector2> _children = new Dictionary<UIElement, Vector2>();

        private bool _visible;

        /// <summary>
        /// Only visible canvas gets rendered
        /// </summary>
        public bool IsVisible
        {
            get => _visible;
            set => _visible = value;
        }

        /// <summary>
        /// Gets or sets the position of the canvas.
        /// </summary>
        public Vector2 Position { get; set; }

        private float _opacity;

        /// <summary>
        /// Gets or sets the opacity of the canvas.
        /// </summary>
        public float? Opacity
        {
            get => _opacity;
            set => _opacity = MathHelper.Clamp(value ?? 1f, 0f, 1f);
        }

        /// <summary>
        /// Gets or sets the layer index of the canvas.
        /// </summary>
        public LayerIndex LayerIndex { get; set; }

        /// <summary>
        /// Creates a canvas with a specified texture, layer index, width, height, position, visibility, opacity, and tint color.
        /// </summary>
        /// <param name="texture">The texture to be rendered on the canvas.</param>
        /// <param name="layerIndex">The layer index of the canvas.</param>
        /// <param name="width">The width of the canvas.</param>
        /// <param name="height">The height of the canvas.</param>
        /// <param name="position">The position of the canvas.</param>
        /// <param name="visible">Whether the canvas is visible or not.</param>
        /// <param name="opacity">The opacity of the canvas.</param>
        /// <param name="color">The tint color of the canvas.</param>
        public Canvas(Texture2D texture, int layerIndex, int width, int height, Vector2 position, Alignment alignment = Alignment.Center, bool visible = false, float opacity = 1, Color? color = null)
        {
            Texture = texture;
            _width = width;
            _height = height;
            Position = alignment switch
            {
                Alignment.TopLeft => position,
                Alignment.TopRight => new Vector2(position.X + GameManager.Graphics!.PreferredBackBufferWidth - width, position.Y),
                Alignment.BottomLeft => new Vector2(position.X, position.Y + GameManager.Graphics!.PreferredBackBufferHeight - height),
                Alignment.BottomRight => new Vector2(position.X + GameManager.Graphics!.PreferredBackBufferWidth - width, position.Y + GameManager.Graphics!.PreferredBackBufferHeight - height),
                Alignment.Center => new Vector2(position.X + (GameManager.Graphics!.PreferredBackBufferWidth - width) / 2, position.Y + (GameManager.Graphics!.PreferredBackBufferHeight - height) / 2),
                _ => position
            };
            _visible = visible;
            LayerIndex = new LayerIndex(layerIndex);
            Opacity = opacity;
            Tint = color ?? Color.White;
        }

        /// <summary>
        /// Creates a fullscreen canvas with a specified texture, layer index, visibility, and opacity.
        /// </summary>
        /// <param name="texture">The texture to be rendered on the canvas.</param>
        /// <param name="layerIndex">The layer index of the canvas.</param>
        /// <param name="visible">Whether the canvas is visible or not.</param>
        /// <param name="opacity">The opacity of the canvas.</param>
        public Canvas(Texture2D texture, int layerIndex, bool visible = false, float opacity = 1)
        {
            Texture = texture;
            _width = GameManager.Graphics.PreferredBackBufferWidth;
            _height = GameManager.Graphics.PreferredBackBufferHeight;
            Position = Vector2.Zero;
            LayerIndex = new LayerIndex(layerIndex);
            _visible = visible;
            Opacity = opacity;
        }

        /// <summary>
        /// Adds a child element to the canvas with a specified relative position.
        /// </summary>
        /// <typeparam name="T">The type of the child element.</typeparam>
        /// <param name="child">The child element to add.</param>
        /// <returns>The added child element.</returns>
        public T AddChild<T>(T child) where T : UIElement
        {
            child.Owner = this;
            child.Opacity = child.Opacity ?? Opacity;
            // The child will be placed at the top-left corner of the canvas by default until `At` is called.
            _children.Add(child, Position);
            child.Position = Position;
            // Set relative layer of the child element.
            child.LayerIndex = LayerIndex + _children.Count * 0.001f;
            // Set the absolute position of the child element.
            return child;
        }

        /// <summary>
        /// Sets the relative position of a child element in the canvas.
        /// </summary>
        /// <typeparam name="T">The type of the child element.</typeparam>
        /// <param name="child">The child element to set the position for.</param>
        /// <param name="relativePosition">The relative position of the child element.</param>
        /// <returns>The child element with the updated position.</returns>
        public T SetPosition<T>(T child, Vector2 relativePosition, Alignment alignment) where T : UIElement
        {
            Vector2 position = alignment switch
            {
                Alignment.TopLeft => Position + relativePosition,
                Alignment.TopRight => Position + new Vector2(_width - child.Width - relativePosition.X, relativePosition.Y),
                Alignment.BottomLeft => Position + new Vector2(relativePosition.X, _height - child.Height - relativePosition.Y),
                Alignment.BottomRight => Position + new Vector2(_width - child.Width - relativePosition.X, _height - child.Height - relativePosition.Y),
                Alignment.Center => Position + new Vector2((_width - child.Width) / 2 + relativePosition.X, (_height - child.Height) / 2 + relativePosition.Y),
                _ => Position + relativePosition
            };
            _children[child] = position;
            child.Position = position;
            // We need to set the UIElement bounding box to respect the absolute position of the element on the screen.
            // It can only be properly calculated here from the given relative position.
            // This ensures correct texture rendering in the child's `Draw` method.
            child.Bounds = new Rectangle((int)child.Position.X, (int)child.Position.Y, child.Width, child.Height);
            child.OriginalBounds = new Rectangle(0, 0, child.Texture.Width, child.Texture.Height);
            return child;
        }

        public void Show() => _visible = true;
        public void Hide() => _visible = false;

        /// <summary>
        /// Updates the canvas and its child elements.
        /// </summary>
        public void Update()
        {
            foreach (var child in _children.Keys)
            {
                child.Update();
            }
        }

        /// <summary>
        /// Draws the canvas and its child elements on the screen.
        /// </summary>
        public void Draw()
        {
            GameManager.SpriteBatch.Draw(
                Texture,
                new Rectangle((int)Position.X, (int)Position.Y, _width, _height),
                null,
                (Tint ?? Color.White) * (Opacity ?? 1f),
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                LayerIndex.Depth
            );
            foreach (var child in _children.Keys)
            {
                child.Draw();
            }
        }
        public override string ToString()
        {
            return $"Canvas: {Texture} ({_width}x{_height}) at {Position}, visible: {_visible}, opacity: {_opacity}, tint: {Tint}, layer index: {LayerIndex}, children: {_children.Count}";
        }
    }
}
