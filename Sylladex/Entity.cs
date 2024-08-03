using Microsoft.Xna.Framework;

namespace Sylladex
{
    public abstract class Entity
    {
        public Vector2 DrawPosition { get; set; }
        protected Sprite Sprite { get; init; }
        public Direction Direction { get; set; } = Direction.Right;
        protected float Opacity { get; set; } = 1.0f;
        public LayerIndex LayerIndex { get; init; } = new LayerIndex();

        public abstract void Update();
        public abstract void Draw();
    }
}
