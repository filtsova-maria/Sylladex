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
        private int _layer;

        /// <summary>
        /// Gets the MonoGame depth value of the layer.
        /// </summary>
        public float Depth
        {
            get => _depth;
        }

        public int Layer
        {
            get => _layer;
        }

        /// <summary>
        /// Sets the index of the layer and updates the depth value accordingly.
        /// </summary>
        /// <param name="index">The index of the layer.</param>
        public void SetIndex(int index)
        {
            _depth = MathHelper.Clamp((float)index / NumberOfLayers, 0, 1f);
            _layer = index;
        }
        public void SetIndex(float depth)
        {
            _depth = MathHelper.Clamp(depth, 0, 1f);
            _layer = (int)(depth * NumberOfLayers);
        }

        /// <summary>
        /// Initializes a new instance of the LayerIndex class with the specified index. Objects with a lower index get rendered below those with a higher index.
        /// </summary>
        /// <param name="index">The index of the layer.</param>
        public LayerIndex(int index = NumberOfLayers / 2) => SetIndex(index);
        public LayerIndex(float depth) => SetIndex(depth);

        public static LayerIndex operator +(LayerIndex layerIndex1, LayerIndex layerIndex2)
        {
            return new LayerIndex(MathHelper.Clamp(layerIndex1.Depth + layerIndex2.Depth, 0f, 1f));
        }

        public static LayerIndex operator -(LayerIndex layerIndex1, LayerIndex layerIndex2)
        {
            return new LayerIndex(MathHelper.Clamp(layerIndex1.Depth - layerIndex2.Depth, 0f, 1f));
        }
        public static LayerIndex operator +(LayerIndex layerIndex1, int value)
        {
            return new LayerIndex(MathHelper.Clamp(layerIndex1.Layer + value, 0, NumberOfLayers));
        }
        public static LayerIndex operator -(LayerIndex layerIndex1, int value)
        {
            return new LayerIndex(MathHelper.Clamp(layerIndex1.Layer - value, 0, NumberOfLayers));
        }
        public static LayerIndex operator +(LayerIndex layerIndex1, float value)
        {
            return new LayerIndex(MathHelper.Clamp(layerIndex1.Depth + value, 0f, 1f));
        }
        public static LayerIndex operator -(LayerIndex layerIndex1, float value)
        {
            return new LayerIndex(MathHelper.Clamp(layerIndex1.Depth - value, 0f, 1f));
        }
        public override string ToString()
        {
            return $"(Layer: {Layer}, Depth: {Depth})";
        }
    }
}
