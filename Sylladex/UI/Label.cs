using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sylladex.Managers;

namespace Sylladex.UI
{
    public class Label : UIElement
    {
        private SpriteFont _font;
        private string _text;
        private Color _textColor;
        private Vector2 _position;

        public Label(SpriteFont font, string text, Color textColor, Vector2 position)
        {
            _font = font;
            _text = text;
            _textColor = textColor;
            _position = position;
        }

        public override void Update()
        {
        }

        public override void Draw()
        {
            GameManager.SpriteBatch!.DrawString(_font, _text, _position, _textColor);
        }
    }
}
