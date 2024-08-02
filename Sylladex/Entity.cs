using Microsoft.Xna.Framework;

namespace Sylladex
{
    public abstract class Entity
    {
        public Vector2 DrawPosition { get; set; }
        protected Sprite Sprite { get; init; }
        protected Direction Direction { get; set; } = Direction.Right;

        public abstract void Update();
        public abstract void Draw();
    }
}
