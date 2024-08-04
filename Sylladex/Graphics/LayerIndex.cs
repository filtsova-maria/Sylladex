using Microsoft.Xna.Framework;

namespace Sylladex.Graphics
{
    /// <summary>
    /// Represents the index of a layer in the graphics system for greater convenience and granularity relative to MonoGame float depth.
    /// </summary>
    public class LayerIndex
    {
        public const int NumberOfLayers = 100; // Represents the maximum number of rendered game objects
        private float _depth = 0.5f;

        /// <summary>
        /// Gets the MonoGame depth value of the layer.
        /// </summary>
        public float Depth
        {
            get => _depth;
        }

        /// <summary>
        /// Sets the index of the layer and updates the depth value accordingly.
        /// </summary>
        /// <param name="index">The index of the layer.</param>
        public void SetIndex(int index) => _depth = MathHelper.Clamp((float)index / NumberOfLayers, 0, 1f);

        /// <summary>
        /// Initializes a new instance of the LayerIndex class with the specified index. Objects with a lower index get rendered below those with a higher index.
        /// </summary>
        /// <param name="index">The index of the layer.</param>
        public LayerIndex(int index = NumberOfLayers / 2)
        {
            SetIndex(index);
        }
    }
}
