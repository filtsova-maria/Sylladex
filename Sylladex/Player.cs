using Microsoft.Xna.Framework;

namespace Sylladex
{
    public enum Direction
    {
        Left = -1,
        Right = 1,
        Up = -1,
        Down = 1
    }
    public class Player : AnimatedEntity
    {
        private readonly float _speed = 150F;
        private readonly Animation _moveAnimation;

        public Player(Vector2 pos)
        {
            Sprite = new Sprite(this, GameManager.TextureManager.GetObject("player"));
            Direction = Direction.Right;
            DrawPosition = pos;
            _moveAnimation = GameManager.AnimationManager.GetObject("playerMove");
        }
        public override void Update()
        {
            // Call the base class's Update method to handle animation updates
            base.Update();
        }

        public void Move(Direction xDirection, Direction yDirection)
        {
            IsAnimating = true;
            CurrentAnimation = _moveAnimation;
            GameManager.SoundEffectManager.Play("footsteps");
            float elapsedTime = GameManager.TotalSeconds;
            float newX = DrawPosition.X + _speed * (int)xDirection * elapsedTime;
            float newY = DrawPosition.Y + _speed * (int)yDirection * elapsedTime;
            if (xDirection != 0)
            {
                Direction = xDirection;
            }

            if (newX >= 0 && newX <= GameManager.Graphics.PreferredBackBufferWidth-Sprite.GetWidth())
            {
                DrawPosition = new Vector2(newX, DrawPosition.Y);
            }

            if (newY >= 0 && newY <= GameManager.Graphics.PreferredBackBufferHeight-Sprite.GetHeight())
            {
                DrawPosition = new Vector2(DrawPosition.X, newY);
            }
        }
    }
}
