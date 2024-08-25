using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sylladex.Entities;
using Sylladex.Managers;

namespace Sylladex.UI
{
    public class SylladexCard : UIElement
    {
        public Item? Item { get; set; } = null;
        private readonly SpriteFont _font;
        private readonly Texture2D _texture;
        private readonly float _itemTextureScale = 0.5f;
        public bool IsEnabled { get; set; } = true;

        public SylladexCard(Item? item, Color cardColor)
        {
            Item = item;
            _font = GameManager.FontManager.GetObject("main");
            _texture = GameManager.TextureManager.GetObject("itemCard");
            Tint = cardColor;
            Width = _texture.Width;
            Height = _texture.Height;
        }

        private Vector2 ItemPosition
        {
            get
            {
                if (Item is null)
                {
                    return Position;
                }
                return Position
                   + TextureManager.GetTextureCenter(_texture)
                   - TextureManager.GetTextureCenter(Item.Texture) * _itemTextureScale;
            }
        }

        private Vector2 TextPosition
        {
            get
            {
                if (Item is null)
                {
                    return Position;
                }
                return new Vector2(
                    (int)(ItemPosition.X + Item.Texture.Width * _itemTextureScale / 2 - _font.MeasureString(Item.Name).X / 2),
                    (int)(ItemPosition.Y + Item.Texture.Height * _itemTextureScale)
                );
            }
        }

        public override void Update()
        {
            if (IsEnabled && IsPressed() && Item is not null)
            {
                GameManager.SylladexManager.FetchItem(Item);
            }
        }

        public override void Draw()
        {
            Color cardColor = IsEnabled ? (IsPressed() || IsHovered() ? Color.DarkGray : (Color)Tint!) : Color.DarkGray;
            GameManager.SpriteBatch.Draw(
                _texture,
                new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height),
                null,
                cardColor,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                LayerIndex.Depth
            );

            if (Item is not null)
            {
                GameManager.SpriteBatch.Draw(
                    Item.Texture,
                    ItemPosition,
                    null,
                    IsEnabled ? Color.White : Color.DarkGray,
                    0f,
                    Vector2.Zero,
                    0.5f,
                    SpriteEffects.None,
                    LayerIndex.Depth + 0.01f
                );
                GameManager.SpriteBatch.DrawString(
                    _font,
                    Item.Name,
                    TextPosition,
                    Color.Black,
                    0f,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    LayerIndex.Depth + 0.02f
                 );
            }
        }
    }
}