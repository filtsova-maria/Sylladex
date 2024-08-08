using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sylladex.Managers;
using Sylladex.Graphics;

namespace Sylladex.UI
{

    /// <summary>
    /// Represents a base UI element, needs to be placed via <see cref="UIElementExtensions.In"/>.
    /// </summary>
    public abstract class UIElement : IRenderable
    {
        public IContainer Owner { get; set; }
        public Vector2 Position { get; set; }
        public LayerIndex LayerIndex { get; set; }
        public Texture2D Texture { get; init; }
        public float? Opacity { get; set; }
        public Color? Tint { get; set; }
        public virtual bool IsPressed() => InputManager.IsHovered(Bounds) && InputManager.Clicked;
        public virtual bool IsHovered() => InputManager.IsHovered(Bounds);
        public Rectangle Bounds;
        public Rectangle OriginalBounds;
        public abstract void Update();
        public abstract void Draw();
        public int Width;
        public int Height;
    }

    /// <summary>
    /// Provides extension methods for UIElement.
    /// </summary>
    public static class UIElementExtensions
    {
        /// <summary>
        /// Places the UI element in the specified container.
        /// </summary>
        /// <typeparam name="T">UI element type, e.g. <see cref="Button"/>.</typeparam>
        /// <param name="element">The UI element to be placed.</param>
        /// <param name="container">The container to place the UI element in.</param>
        /// <returns>The placed UI element.</returns>
        public static PlacedUIElement<T> In<T>(this T element, IContainer container) where T : UIElement
        {
            container.AddChild(element, null);
            return new PlacedUIElement<T>(element, container, element.Texture, element.Width, element.Height, element.Tint, element.Opacity);
        }
    }

    /// <summary>
    /// Represents a positioned UI element with an owner container.
    /// </summary>
    public class PlacedUIElement<T> : UIElement where T : UIElement
    {
        T Element { get; set; }

        /// <summary>
        /// Initializes a new instance of the PlacedUIElement class.
        /// </summary>
        /// <param name="element">The UI element to be placed.</param>
        /// <param name="owner">The owner container of the UI element.</param>
        /// <param name="texture">The texture of the UI element.</param>
        /// <param name="width">The width of the UI element.</param>
        /// <param name="height">The height of the UI element.</param>
        /// <param name="color">The color tint of the UI element. Defaults to <c>Color.White</c> (original texture color).</param>
        /// <param name="opacity">The opacity of the UI element. Defaults to the opacity of the owner container.</param>
        public PlacedUIElement(T element, IContainer owner, Texture2D texture, int width, int height, Color? color = null, float? opacity = null)
        {
            Element = element;
            // Ensures the bounds are passed from the original element to its placed chainable.
            element.OriginalBounds = new Rectangle(0, 0, element.Texture.Width, element.Texture.Height);
            element.Bounds = new Rectangle((int)element.Position.X, (int)element.Position.Y, element.Width, element.Height);
            Owner = owner;
            Texture = texture;
            Width = width;
            Height = height;
            Tint = color ?? Color.White;
            Opacity = opacity;
        }

        /// <summary>
        /// Sets the position of the UI element relative to the owner container.
        /// </summary>
        /// <param name="position">The position relative to the owner container.</param>
        /// <returns>The UI element with the updated position.</returns>
        public T At(Vector2 position)
        {
            return Owner.SetPosition(Element, position);
        }

        /// <summary>
        /// Updates the UI element.
        /// </summary>
        public override void Update() { }

        /// <summary>
        /// Draws the UI element.
        /// </summary>
        public override void Draw()
        {
            GameManager.SpriteBatch.Draw(
                Texture,
                Bounds,
                OriginalBounds,
                (Color)Tint * (float)Opacity,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                LayerIndex.Depth
            );
        }
    }
}
