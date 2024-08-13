using Sylladex.Graphics;

namespace Sylladex.Entities
{
    /// <summary>
    /// Represents an abstract class for animated entities.
    /// </summary>
    public abstract class AnimatedEntity : Entity
    {
        protected Animation? CurrentAnimation { get; set; }
        protected bool IsAnimating { get; set; }

        /// <summary>
        /// Manages animated entity state when an animation is playing.
        /// </summary>
        public override void Update()
        {
            if (CurrentAnimation is null) return;
            if (IsAnimating)
            {
                CurrentAnimation.Start();
                CurrentAnimation.Update();
            }
            else
            {
                CurrentAnimation.Stop();
                CurrentAnimation.Reset();
            }
            IsAnimating = false;
        }

        /// <summary>
        /// Draws the animated entity, either the original sprite or the current playing animation.
        /// </summary>
        public override void Draw()
        {
            if (IsAnimating && CurrentAnimation is not null)
            {
                CurrentAnimation.Draw(DrawPosition, LayerIndex.Depth, Direction == HorizontalDirection.Left);
            }
            else
            {
                Sprite!.Draw();
            }
        }
    }
}
