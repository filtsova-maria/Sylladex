using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sylladex.Graphics;
using Sylladex.Managers;

namespace Sylladex.Entities
{
    public class Item : Entity
    {
        public string Name { get; }
        public Texture2D Texture { get; }
        private const float _pickupRadius = 80;
        private readonly Texture2D _tooltipTexture = GameManager.TextureManager.GetObject("eButton");
        private readonly Vector2 _tooltipPosition;
        private readonly Vector2 _namePosition;
        private readonly SpriteFont _font = GameManager.FontManager.GetObject("main");

        public Item(string name, Texture2D texture, Vector2 position = default)
        {
            Sprite = new Sprite(this, texture);
            Name = name;
            Texture = texture;
            DrawPosition = position;
            _tooltipPosition = new Vector2((int)DrawPosition.X + 25, (int)DrawPosition.Y - 25);
            _namePosition = new Vector2((int)(DrawPosition.X + Texture.Width / 2), BottomPosition + 5);
            GameManager.InputManager.AddAction(Keys.E,
                () => GameManager.SylladexManager.InsertItem(this),
                () => CollisionManager.IsInRadius(this, GameManager.EntityManager.GetObject("player"), _pickupRadius),
                context: this,
                singlePress: true
            );
        }
        public override void Update()
        {
            if (CollisionManager.IsInRadius(this, GameManager.EntityManager.GetObject("player"), _pickupRadius))
            {
                Sprite.Tint = Color.GreenYellow; // Indicates that the item can be picked up
            }
            else
            {
                Sprite.Tint = Color.White;
            }
        }
        public override void Draw()
        {
            base.Draw();
            GameManager.SpriteBatch.DrawString(
                _font,
                Name,
                _namePosition,
                Color.White,
                0f,
                _font.MeasureString(Name) / 2,
                1f,
                SpriteEffects.None,
                LayerIndex.Depth
            );
            if (CollisionManager.IsInRadius(this, GameManager.EntityManager.GetObject("player"), _pickupRadius))
            {
                GameManager.SpriteBatch.Draw(
                   _tooltipTexture,
                   _tooltipPosition,
                   null,
                   Color.White,
                   0f,
                   TextureManager.GetTextureCenter(_tooltipTexture),
                   new Vector2(0.5f, 0.5f),
                   SpriteEffects.None,
                   LayerIndex.Depth
                );

            }
        }
    }
}
