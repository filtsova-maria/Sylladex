using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sylladex.Managers
{
    /// <summary>
    /// Manages user keyboard and mouse input.
    /// </summary>
    public class InputManager : ObjectManager<Keys, Action>
    {
        /// <summary>
        /// Current state of the mouse.
        /// </summary>
        public static MouseState MouseState { get; private set; }

        /// <summary>
        /// Previous state of the mouse.
        /// </summary>
        public static MouseState LastMouseState { get; private set; }

        /// <summary>
        /// Indicates whether the mouse button has been clicked.
        /// </summary>
        public static bool Clicked { get; private set; }

        /// <summary>
        /// Rectangle representing the mouse cursor for collision (hovering) detection.
        /// </summary>
        public static Rectangle MouseCursor { get; private set; }

        /// <summary>
        /// Checks for keyboard and mouse input.
        /// </summary>
        public void Update()
        {
            KeyboardState kstate = Keyboard.GetState();
            foreach (var key in _objects)
            {
                if (kstate.IsKeyDown(key.Key))
                {
                    key.Value();
                }
            }

            LastMouseState = MouseState;
            MouseState = Mouse.GetState();
            Clicked = (MouseState.LeftButton == ButtonState.Pressed) && (LastMouseState.LeftButton == ButtonState.Released);
            MouseCursor = new Rectangle(MouseState.Position, new Point(1, 1));
        }

        /// <summary>
        /// Checks if the mouse cursor is hovering over the specified surface.
        /// </summary>
        /// <param name="surface">The surface to check.</param>
        /// <returns>True if the mouse cursor is hovering over the surface, false otherwise.</returns>
        public static bool IsHovered(Rectangle surface)
        {
            return surface.Contains(MouseCursor.Location);
        }
    }
}
