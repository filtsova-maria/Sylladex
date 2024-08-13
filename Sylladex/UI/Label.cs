using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sylladex.Managers;
using System.Diagnostics;

namespace Sylladex.UI
{
    public class Label : UIElement
    {
        private SpriteFont _font;
        private string _text;
        private Color _textColor;
        private Color? _backgroundColor;
        private float _backgroundOpacity;

        public Label(SpriteFont font, string text, Color? backgroundColor=null, Color? textColor = null, float backgroundOpacity=1f)
        {
            _font = font;
            _text = text;
            _textColor = textColor ?? Color.Black;
            _backgroundColor = backgroundColor;
            _backgroundOpacity = backgroundOpacity;
            Width = (int)_font.MeasureString(_text).X;
            Height = (int)_font.MeasureString(_text).Y;
            Debug.WriteLine($"{this}");
        }

        public override void Update()
        {
        }

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
            GameManager.SpriteBatch!.DrawString(_font, _text, Position, _textColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, LayerIndex.Depth+0.1f);
        }
    }
}
