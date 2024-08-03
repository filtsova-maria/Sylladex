namespace Sylladex
{
    public abstract class AnimatedEntity : Entity
    {
        #nullable enable
        protected Animation? CurrentAnimation { get; set; }
        protected bool IsAnimating { get; set; }

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

        public override void Draw()
        {
            if (IsAnimating && CurrentAnimation is not null)
            {
                CurrentAnimation.Draw(DrawPosition, LayerIndex.Depth, Direction == Direction.Left);
            }
            else
            {
                Sprite.Draw(null);
            }
        }
    }
}
