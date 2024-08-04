using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sylladex.Managers;
using Sylladex.Graphics;

namespace Sylladex.UI
{
    /// <summary>
    /// Represents a UI element in the Sylladex system.
    /// </summary>
    public class UIElement
    {
        protected readonly Texture2D Texture;
        protected readonly Window OwnerWindow;
        protected Vector2 RelativePosition;
        protected int Width;
        protected int Height;
        protected Color Color;
        protected LayerIndex RelativeLayerIndex;
        protected float Opacity;
        protected Rectangle OriginalBounds;
        protected Rectangle Bounds;

        /// <summary>
        /// Initializes a new instance of the UIElement class.
        /// </summary>
        /// <param name="ownerWindow">The window that owns the UI element.</param>
        /// <param name="texture">The texture of the UI element.</param>
        /// <param name="relativePosition">The position of the UI element relative to the owner window.</param>
        /// <param name="width">The width of the UI element.</param>
        /// <param name="height">The height of the UI element.</param>
        /// <param name="color">The color tint of the UI element. Defaults to white (original texture color).</param>
        /// <param name="opacity">The opacity of the UI element. Defaults to the opacity of the owner window.</param>
        public UIElement(Window ownerWindow, Texture2D texture, Vector2 relativePosition, int width, int height, Color? color = null, float? opacity = null)
        {
            OwnerWindow = ownerWindow;
            RelativeLayerIndex = new LayerIndex(OwnerWindow.AddChild(this));
            Texture = texture;
            RelativePosition = OwnerWindow.Position + relativePosition;
            Width = width;
            Height = height;
            Color = color ?? Color.White;
            Opacity = opacity ?? OwnerWindow.Opacity;
            OriginalBounds = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Bounds = new Rectangle((int)RelativePosition.X, (int)RelativePosition.Y, Width, Height);
        }

        /// <summary>
        /// Gets a value indicating whether the UI element is being hovered over.
        /// </summary>
        public bool IsHovered => InputManager.IsHovered(Bounds);

        /// <summary>
        /// Gets a value indicating whether the UI element is being pressed.
        /// </summary>
        public bool IsPressed => IsHovered && InputManager.Clicked;

        /// <summary>
        /// Updates the UI element.
        /// </summary>
        public virtual void Update() { }

        /// <summary>
        /// Draws the UI element.
        /// </summary>
        public virtual void Draw()
        {
            GameManager.SpriteBatch.Draw(
                Texture,
                Bounds,
                OriginalBounds,
                Color * Opacity,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                OwnerWindow.LayerIndex.Depth + RelativeLayerIndex.Depth
            );
        }
    }
}
