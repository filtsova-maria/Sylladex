using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sylladex.Managers;
using System;

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
        private SpriteFont _font;
        private string? _text;
        private Color _textColor;

        /// <summary>
        /// Only enabled button can be clicked or hovered.
        /// </summary>
        public bool IsEnabled { get; set; }

        public Button(Texture2D texture, int width, int height, Action onClick, string? text = null, Color? color = null, Color? textColor = null, SpriteFont? font = null, float? opacity = 1f, bool enabled = true, Color? hoverColor = null, Color? disabledColor = null)
        {
            Texture = texture;
            Width = width;
            Height = height;
            Tint = color ?? Color.White;
            Opacity = opacity;
            _hoverColor = hoverColor ?? Color.Gray;
            _disabledColor = disabledColor ?? Color.DarkGray;
            IsEnabled = enabled;
            _font = font ?? GameManager.FontManager!.GetObject("main");
            _text = text;
            _textColor = textColor ?? Color.Black;
            _onClick = onClick;
        }

        /// <summary>
        /// Reacts to button events.
        /// </summary>
        public override void Update()
        {
            if (IsEnabled && IsPressed())
            {
                _onClick?.Invoke();
            }
        }

        /// <summary>
        /// Draws the button in the main loop.
        /// </summary>
        public override void Draw()
        {
            Color buttonColor = IsEnabled ? (IsPressed() || IsHovered() ? _hoverColor : Tint ?? Color.White) : _disabledColor;
            GameManager.SpriteBatch!.Draw(
                Texture,
                Bounds,
                OriginalBounds,
                buttonColor * (Opacity ?? 1),
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                LayerIndex.Depth
            );

            if (_font != null && !string.IsNullOrEmpty(_text))
            {
                Vector2 textSize = _font.MeasureString(_text);
                Vector2 textPosition = new Vector2((int)(Bounds.Center.X - textSize.X / 2), (int)(Bounds.Center.Y - textSize.Y / 2));
                GameManager.SpriteBatch.DrawString(_font, _text, textPosition, _textColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, (LayerIndex + 1).Depth);
            }
        }
    }
    // TODO: settings window
    // TODO: Sylladex state management
}
