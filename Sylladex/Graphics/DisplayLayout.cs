using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Sylladex.Graphics
{
    public struct DisplayLayout
    {
        public List<Vector2> Positions;
        public DisplayLayout(Vector2 startPosition, int count, int width, int padding)
        {
            Positions = new List<Vector2>();
            for (int i = 0; i < count; i++)
            {
                int x = (int)startPosition.X + i * (width + padding);
                int y = (int)startPosition.Y;
                Positions.Add(new Vector2(x, y));
            }
        }
    }
}