using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sylladex.Managers;
using System.Diagnostics;

namespace Sylladex.UI
{

    /// <summary>
    /// Represents a label UI element that displays text.
    /// </summary>
    public class Label : UIElement
    {
        private SpriteFont _font;
        private string _text;
        private Color _textColor;
        private Color? _backgroundColor;
        private float _backgroundOpacity;

        /// <summary>
        /// Initializes a new instance of the <see cref="Label"/> class.
        /// </summary>
        /// <param name="font">The font used for the label text.</param>
        /// <param name="text">The text to be displayed.</param>
        /// <param name="backgroundColor">The background color of the label (optional).</param>
        /// <param name="textColor">The text color of the label (optional).</param>
        /// <param name="backgroundOpacity">The opacity of the background color (optional).</param>
        public Label(SpriteFont font, string text, Color? backgroundColor = null, Color? textColor = null, float backgroundOpacity = 1f)
        {
            _font = font;
            _text = text;
            _textColor = textColor ?? Color.Black;
            _backgroundColor = backgroundColor;
            _backgroundOpacity = backgroundOpacity;
            Width = (int)_font.MeasureString(_text).X;
            Height = (int)_font.MeasureString(_text).Y;
        }

        public override void Update()
        {}

        /// <summary>
        /// Draws the label.
        /// </summary>
        public override void Draw()
        {
            if (_backgroundColor is not null)
            {
                GameManager.SpriteBatch!.Draw(
                    GameManager.TextureManager!.GetObject("pixelBase"),
                    Bounds,
                    OriginalBounds,
                    _backgroundColor!.Value * _backgroundOpacity,
                    0f,
                    Vector2.Zero,
                    SpriteEffects.None,
                    LayerIndex.Depth
                );
            }
            GameManager.SpriteBatch!.DrawString(_font, _text, Position, _textColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, LayerIndex.Depth + 0.1f);
        }
    }
}
