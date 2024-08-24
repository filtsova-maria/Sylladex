using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sylladex.Managers
{
    /// <summary>
    /// Manages user keyboard and mouse input.
    /// </summary>
    public class InputManager : ObjectManager<Keys, List<(Action action, Func<bool>? condition, object? context)>>
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
        /// Add an action binding for a key.
        /// </summary>
        public void AddAction(Keys key, Action action, Func<bool>? condition = null, object? context = null)
        {
            if (_objects.ContainsKey(key))
            {
                _objects[key].Add((action, condition, context));
            }
            else
            {
                _objects[key] = new List<(Action action, Func<bool>? condition, object? context)> { (action, condition, context) };
            }
        }
        /// <summary>
        /// Removes all key bindings for given context. Used for cleaning up when a game object is removed.
        /// </summary>
        public void RemoveActionsFromEntity(object? context)
        {
            if (context is null)
            {
                return;
            }
            foreach (var key in _objects.Keys)
            {
                _objects[key].RemoveAll(x => x.context == context);
            }
        }

        /// <summary>
        /// Checks for keyboard and mouse input.
        /// </summary>
        public void Update()
        {
            KeyboardState kstate = Keyboard.GetState();
            foreach (var key in _objects.Keys)
            {
                if (kstate.IsKeyDown(key))
                {
                    var actions = _objects[key].ToList(); // Create a copy to avoid concurrent modification
                    foreach (var (action, condition, _) in actions)
                    {
                        if (condition is null || condition())
                        {
                            action();
                        }
                    }
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
