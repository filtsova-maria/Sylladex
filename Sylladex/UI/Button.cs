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
        private Color _hoverColor;
        private Color _disabledColor;
        private Action _onClick;

        /// <summary>
        /// Only enabled button can be clicked or hovered.
        /// </summary>
        public bool IsEnabled { get; set; }

        public Button(Texture2D texture, int width, int height, Color? color = null, float? opacity = null, bool enabled = true, Color? hoverColor = null, Color? disabledColor = null)
        {
            Texture = texture;
            Width = width;
            Height = height;
            Tint = color ?? Color.White;
            Opacity = opacity ?? 1f;
            _hoverColor = hoverColor ?? Color.Gray;
            _disabledColor = disabledColor ?? Color.DarkGray;
            IsEnabled = enabled;
        }

        /// <summary>
        /// Updates the button.
        /// </summary>
        public override void Update()
        {
            if (IsEnabled && IsPressed())
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
            Color buttonColor = IsEnabled ? (IsPressed() || IsHovered() ? _hoverColor : (Color)Tint) : _disabledColor;
            GameManager.SpriteBatch.Draw(
                Texture,
                Bounds,
                OriginalBounds,
                buttonColor * (float)Opacity,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                LayerIndex.Depth
            );
        }
    }
    // TODO: add button text, labels
    // TODO: implement sylladex UI, layout calculations
}
