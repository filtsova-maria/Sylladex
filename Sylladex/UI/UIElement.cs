using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sylladex.Managers;
using Sylladex.Graphics;

namespace Sylladex.UI
{
    public enum Alignment
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight,
        Center,
    }
    /// <summary>
    /// Represents a base UI element, needs to be placed via <see cref="UIElementExtensions.In"/>.
    /// </summary>
    public abstract class UIElement : IRenderable
    {
        public IContainer? Owner { get; set; }
        public Vector2 Position { get; set; }
        public LayerIndex LayerIndex { get; set; }
        public Texture2D Texture { get; init; }
        public float? Opacity { get; set; }
        public Color? Tint { get; set; }
        public Rectangle Bounds { get; set; }
        public Rectangle OriginalBounds { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public virtual bool IsPressed()
        {
            return InputManager.IsHovered(Bounds) && InputManager.Clicked;
        }

        public virtual bool IsHovered()
        {
            return InputManager.IsHovered(Bounds);
        }

        public UIElement()
        {
            Position = Vector2.Zero;
            LayerIndex = new LayerIndex();
            Opacity = 1f;
            Tint = Color.White;
            Texture = GameManager.TextureManager!.GetObject("pixelBase");
        }

        public abstract void Update();
        public abstract void Draw();

        public override string ToString()
        {
            return $"{GetType().Name}: Position={Position}, LayerIndex={LayerIndex}, Texture={Texture}, Opacity={Opacity}, Tint={Tint}, Bounds={Bounds}, OriginalBounds={OriginalBounds}, Width={Width}, Height={Height},\nOwner=({Owner})";
        }
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
            container.AddChild(element);
            return new PlacedUIElement<T>(element, container);
        }
    }

    /// <summary>
    /// Represents a positioned UI element with an owner container.
    /// </summary>
    public class PlacedUIElement<T> : UIElement where T : UIElement
    {
        private T Element { get; set; }

        /// <summary>
        /// Initializes a new instance of the PlacedUIElement class.
        /// </summary>
        /// <param name="element">The UI element to be placed.</param>
        /// <param name="owner">The owner container of the UI element.</param>
        public PlacedUIElement(T element, IContainer owner)
        {
            Owner = owner;
            Element = element;
            Element.Owner = owner;
        }

        /// <summary>
        /// Sets the position of the UI element relative to the owner container.
        /// </summary>
        /// <param name="relativePosition">The position relative to the owner container.</param>
        /// <returns>The UI element with the updated position.</returns>
        public T At(Vector2 relativePosition, Alignment alignment=Alignment.TopLeft)
        {
            Element.Position = relativePosition;
            return Owner?.SetPosition(Element, relativePosition, alignment) ?? Element;
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
            Element.Draw();
        }
    }
}
