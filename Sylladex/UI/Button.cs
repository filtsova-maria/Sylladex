using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sylladex.Managers;
using System;
using System.Diagnostics;

namespace Sylladex.UI
{
    /// <summary>
    /// Represents a button UI element.
    /// </summary>
    public class Button : UIElement
    {
        private bool _isEnabled;
        private Color _hoverColor;
        private Color _disabledColor;
        private Action _onClick;

        /// <summary>
        /// Gets or sets a value indicating whether the button is enabled.
        /// </summary>
        public bool IsEnabled
        {
            get => _isEnabled;
            set => _isEnabled = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        /// <param name="ownerWindow">The window that owns the button.</param>
        /// <param name="texture">The texture of the button.</param>
        /// <param name="position">The position of the button relative to the owner window.</param>
        /// <param name="width">The width of the button.</param>
        /// <param name="height">The height of the button.</param>
        /// <param name="onClick">The action to be performed when the button is clicked.</param>
        /// <param name="tint">The color tint of the button.</param>
        /// <param name="opacity">The opacity of the button.</param>
        public Button(Window ownerWindow, Texture2D texture, Vector2 position, int width, int height, Action onClick, Color? tint = null, float? opacity = null)
            : base(ownerWindow, texture, position, width, height, tint, opacity)
        {
            _isEnabled = true;
            _disabledColor = Color.DarkGray;
            _hoverColor = Color.Gray;
            _onClick = onClick;
        }

        /// <summary>
        /// Updates the button.
        /// </summary>
        public override void Update()
        {
            if (_isEnabled && IsPressed)
            {
                _onClick?.Invoke();
                Debug.WriteLine("Button pressed!");
            }
        }

        /// <summary>
        /// Draws the button.
        /// </summary>
        public override void Draw()
        {
            Color buttonColor = _isEnabled ? (IsPressed || IsHovered ? _hoverColor : Color) : _disabledColor;

            GameManager.SpriteBatch.Draw(
                Texture,
                Bounds,
                OriginalBounds,
                buttonColor * Opacity,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                OwnerWindow.LayerIndex.Depth + RelativeLayerIndex.Depth
            );
        }
    }
    // TODO: add button text, labels
    // TODO: implement sylladex UI, layout calculations
}
