using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sylladex.Entities;
using Sylladex.Managers;
using System.Collections.Generic;
using System.Diagnostics;

namespace Sylladex.UI
{
    public class SylladexCard : UIElement
    {
        private IItem? _item;
        private readonly SpriteFont _font;
        private readonly Texture2D _texture;
        private Color _cardColor;
        private readonly float _itemTextureScale = 0.5f;
        public bool IsEnabled { get; set; } = true;

        public SylladexCard(IItem? item, Color cardColor)
        {
            _item = item;
            _font = GameManager.FontManager!.GetObject("main");
            _texture = GameManager.TextureManager!.GetObject("itemCard");
            _cardColor = cardColor;
            Width = _texture.Width;
            Height = _texture.Height;
        }

        private Vector2 ItemPosition =>
            Position
            + TextureManager.GetTextureCenter(_texture)
            - TextureManager.GetTextureCenter(_item!.Texture) * _itemTextureScale;
        private Vector2 TextPosition => new Vector2(
            (int)(ItemPosition.X + _item!.Texture.Width * _itemTextureScale / 2 - _font.MeasureString(_item.Name).X / 2),
            (int)(ItemPosition.Y + _item!.Texture.Height * _itemTextureScale)
            );

        public void SetItem(IItem item)
        {
            _item = item;
        }

        public override void Update()
        {
        }

        public override void Draw()
        {
            Color cardColor = IsEnabled ? (IsPressed() || IsHovered() ? Color.DarkGray : _cardColor) : Color.DarkGray;
            GameManager.SpriteBatch!.Draw(
                _texture,
                new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height),
                null,
                cardColor,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                LayerIndex.Depth
            );

            if (_item is not null)
            {
                GameManager.SpriteBatch!.Draw(
                    _item.Texture,
                    ItemPosition,
                    null,
                    IsEnabled ? Color.White : Color.DarkGray,
                    0f,
                    Vector2.Zero,
                    0.5f,
                    SpriteEffects.None,
                    (LayerIndex + 1).Depth
                );
                GameManager.SpriteBatch.DrawString(
                    _font,
                    _item.Name,
                    TextPosition,
                    Color.Black,
                    0f,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    (LayerIndex + 2).Depth
                 );
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
    public abstract class SylladexBase
    {
        private List<IItem> _items;
        public Color Tint { get; set; } = Color.White;
        public SylladexBase(ref List<IItem> items)
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

    public class QueueSylladex : SylladexBase
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

    public class StackSylladex : SylladexBase
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
//- Captcha function (default: first available emptiest space, otherwise first card, or append to end)
//- Fetch function (default: all cards)
