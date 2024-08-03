using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Sylladex
{
    public class LayerIndex
    {
        public const int NumberOfLayers = 100;
        private float _depth = 0.5f;
        public float Depth
        {
            get => _depth;
        }
        public void SetIndex(int index) => _depth = MathHelper.Clamp(((float)index) / NumberOfLayers, 0, 1f);
        public LayerIndex(int index = NumberOfLayers / 2)
        {
            SetIndex(index);
        }
    }
}
