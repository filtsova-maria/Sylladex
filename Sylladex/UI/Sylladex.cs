using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sylladex.Entities;
using Sylladex.Managers;
using System.Collections.Generic;

namespace Sylladex.UI
{
    public class SylladexCard : UIElement
    {
        private IItem? _item;
        private readonly SpriteFont _font;
        private string? _text;
        private Vector2 _position;
        private Vector2 _itemPosition;
        private Vector2 _textPosition;
        private readonly Texture2D _texture;
        private Color _cardColor;
        public bool Enabled { get; set; } = true;

        public SylladexCard(IItem? item, Vector2 position, Color cardColor)
        {
            _item = item;
            _font = GameManager.FontManager!.GetObject("main");
            _text = item?.Name;
            _position = position;
            _texture = GameManager.TextureManager!.GetObject("itemCard");
            _itemPosition = TextureManager.GetTextureCenter(_texture, _position);
            _textPosition = new Vector2(_itemPosition.X, (int)(_itemPosition.Y + _texture.Height / 2 + 10));
            _cardColor = cardColor;
        }

        public void SetItem(IItem item)
        {
            _item = item;
            _text = item.Name;
        }

        public override void Update()
        {
        }

        public override void Draw()
        {
            GameManager.SpriteBatch!.Draw(_texture, _position, Enabled ? _cardColor : Color.DarkGray);
            if (_item is not null)
            {
                GameManager.SpriteBatch.Draw(_item.Texture, _itemPosition, Enabled ? Color.White : Color.DarkGray);
                GameManager.SpriteBatch.DrawString(_font, _text, _textPosition, Color.Black);
            }
        }
    }
    public struct DisplayLayout
    {
        public List<Vector2> Positions;
        public DisplayLayout(Vector2 startPosition, int count, int width, int padding)
        {
            Positions = new List<Vector2>();
            for (int i = 0; i < count; i++)
            {
                int x = (int)startPosition.X + (i * (width + padding));
                int y = (int)startPosition.Y;
                Positions.Add(new Vector2(x, y));
            }
        }
    }
    public abstract class Sylladex
    {
        private List<IItem> _items;
        public Color Tint { get; set; } = Color.White;
        public Sylladex(ref List<IItem> items)
        {
            _items = items;
        }
        public virtual void PutItem(IItem item)
        {
            _items.Add(item);
        }
        public virtual IItem FetchItem()
        {
            IItem item = _items[0];
            _items.RemoveAt(0);
            return item;
        }
    }

    public class QueueSylladex : Sylladex
    {
        private Queue<IItem> _items;

        public QueueSylladex(ref List<IItem> items) : base(ref items)
        {
            _items = new Queue<IItem>(items);
            Tint = Color.Orange;
        }

        public override void PutItem(IItem item)
        {
            _items.Enqueue(item);
        }

        public override IItem FetchItem()
        {
            return _items.Count > 0 ? _items.Dequeue() : null;
        }
    }

    public class StackSylladex : Sylladex
    {
        private Stack<IItem> _items;

        public StackSylladex(ref List<IItem> items) : base(ref items)
        {
            _items = new Stack<IItem>(items);
            Tint = Color.HotPink;
        }

        public override void PutItem(IItem item)
        {
            _items.Push(item);
        }

        public override IItem FetchItem()
        {
            return _items.Count > 0 ? _items.Pop() : null;
        }
    }
}

// Modus name:
//-Display function(default: linear)
//- Rules around using a static number of cards (default: uses cards in-place)
//- Captcha function (default: first available emptiest space, otherwise first card, or append to end)
//- Fetch function (default: all cards)
