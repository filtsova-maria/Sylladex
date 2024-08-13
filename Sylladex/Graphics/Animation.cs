using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sylladex.Managers;
using System.Collections.Generic;

namespace Sylladex.Graphics
{
    /// <summary>
    /// Represents an animation that can be played and displayed on the screen.
    /// </summary>
    public class Animation
    {
        private readonly Texture2D _texture;
        private readonly List<Rectangle> _sourceRectangles = new();
        private readonly int _frames;
        private int _frame;
        private readonly float _frameTime;
        private float _frameTimeLeft;
        private bool _active = true;

        /// <summary>
        /// Initializes a new instance of the Animation class.
        /// </summary>
        /// <param name="texture">The spritesheet texture containing the animation frames.</param>
        /// <param name="framesX">The number of frames in the X direction.</param>
        /// <param name="framesY">The number of frames in the Y direction.</param>
        /// <param name="frameTime">The time duration for each frame.</param>
        /// <param name="row">The row of frames to use (default is 1).</param>
        public Animation(Texture2D texture, int framesX, int framesY, float frameTime, int row = 1)
        {
            _texture = texture;
            _frameTime = frameTime;
            _frameTimeLeft = _frameTime;
            _frames = framesX;
            var frameWidth = _texture.Width / framesX;
            var frameHeight = _texture.Height / framesY;

            for (int i = 0; i < _frames; i++)
            {
                _sourceRectangles.Add(new(i * frameWidth, (row - 1) * frameHeight, frameWidth, frameHeight));
            }
        }

        /// <summary>
        /// Stops the animation from playing.
        /// </summary>
        public void Stop()
        {
            _active = false;
        }

        /// <summary>
        /// Starts or resumes playing the animation.
        /// </summary>
        public void Start()
        {
            _active = true;
        }

        /// <summary>
        /// Resets the animation to the first frame.
        /// </summary>
        public void Reset()
        {
            _frame = 0;
            _frameTimeLeft = _frameTime;
        }

        /// <summary>
        /// Updates the animation frame based on the elapsed time.
        /// </summary>
        public void Update()
        {
            if (!_active) return;

            _frameTimeLeft -= GameManager.DeltaTime;

            if (_frameTimeLeft <= 0)
            {
                _frameTimeLeft += _frameTime;
                _frame = (_frame + 1) % _frames;
            }
        }

        /// <summary>
        /// Draws the current frame of the animation at the specified position.
        /// </summary>
        /// <param name="position">The position to draw the animation.</param>
        /// <param name="depth">The depth of the animation sprite.</param>
        /// <param name="flip">Whether to flip the animation horizontally, used for changing sprite direction.</param>
        public void Draw(Vector2 position, float depth, bool flip = false)
        {
            GameManager.SpriteBatch.Draw(_texture, position, _sourceRectangles[_frame], Color.White, 0, Vector2.Zero, Vector2.One, flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, depth);
        }
    }

}
