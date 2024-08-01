using Microsoft.Xna.Framework;

namespace Sylladex
{
    public class Player : Entity
    {
        public enum Direction
        {
            Left = -1,
            Right = 1,
            Up = -1,
            Down = 1
        }
        private float _speed = 150F;
        private Direction _directionX = Direction.Right;
        public Player(Vector2 pos)
        {
            Sprite = new Sprite(this, GameManager.TextureManager.GetObject("player"));
            DrawPosition = pos;
        }

        public override void Update()
        {

        }

        public void Move(Direction xDirection, Direction yDirection)
        {
            GameManager.SoundEffectManager.Play("footsteps");
            float elapsedTime = GameManager.TotalSeconds;
            float newX = DrawPosition.X + _speed * (int)xDirection * elapsedTime;
            float newY = DrawPosition.Y + _speed * (int)yDirection * elapsedTime;
            if (xDirection != 0)
            {
                _directionX = xDirection;
            }

            if (newX >= 0 && newX <= GameManager.Graphics.PreferredBackBufferWidth)
            {
                DrawPosition = new Vector2(newX, DrawPosition.Y);
            }

            if (newY >= 0 && newY <= GameManager.Graphics.PreferredBackBufferHeight)
            {
                DrawPosition = new Vector2(DrawPosition.X, newY);
            }
        }

        public override void Draw()
        {
            Sprite.Draw(null, _directionX == Direction.Left);
        }
    }
    // TODO: animation manager, similar to SoundEffectManager manager
    // TODO: window manager, background, UI rendering
}
