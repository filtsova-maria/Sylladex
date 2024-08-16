using Microsoft.Xna.Framework;

namespace Sylladex.UI
{
    public interface IContainer
    {
        // <summary>
        // Adds a child element to the container.
        // </summary>
        public T AddChild<T>(T child) where T : UIElement;
        // <summary>
        // Sets the position of the child element.
        // </summary>
        public T SetPosition<T>(T child, Vector2 position, Alignment alignment) where T : UIElement;
        /// <summary>
        /// Only visible containers get rendered on the screen in the <c>Draw</c> cycle.
        /// </summary>
        public bool IsVisible { get; set; }
    }
}
