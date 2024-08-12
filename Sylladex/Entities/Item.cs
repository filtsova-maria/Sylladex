using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sylladex.Graphics;
using Sylladex.Managers;

namespace Sylladex.Entities
{
    public interface IItem
    {
        string Name { get; }
        Texture2D Texture { get; }
    }
    public class Item: Entity, IItem
    {
        public string Name { get; }
        public Texture2D Texture { get; }
        private const float _pickupRadius = 100;

        public Item(string name, Texture2D texture, Vector2 position = default)
        {
            Sprite = new Sprite(this, texture);
            Name = name;
            Texture = texture;
            DrawPosition = position;
        }
        public override void Update()
        {
            if (CollisionManager.IsInRadius(this, GameManager.EntityManager!.GetObject("player"), _pickupRadius))
            {
                Sprite!.Tint = Color.GreenYellow; // Indicates that the item can be picked up
            }
            else
            {
                Sprite!.Tint = Color.White;
            }
        }
    }
}
