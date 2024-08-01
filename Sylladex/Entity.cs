using Microsoft.Xna.Framework;

namespace Sylladex
{
    public abstract class Entity
    {
        public Vector2 DrawPosition { get; set; }
        public Sprite Sprite { get; init; }
        public abstract void Update();
        public abstract void Draw();
    }
    // TODO: animation manager, similar to SoundEffectManager manager
    // TODO: window manager, background, UI rendering
}
