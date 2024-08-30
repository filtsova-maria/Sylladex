using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Sylladex.Graphics
{
    public struct DisplayLayout
    {
        public List<Vector2> Positions;
        public DisplayLayout(Vector2 startPosition, int count, int elementSize, int padding, bool vertical = false)
        {
            Positions = new List<Vector2>();
            for (int i = 0; i < count; i++)
            {
                if (vertical)
                {
                    int x = (int)startPosition.X;
                    int y = (int)startPosition.Y + i * (elementSize + padding);
                    Positions.Add(new Vector2(x, y));
                } else
                {
                    int x = (int)startPosition.X + i * (elementSize + padding);
                    int y = (int)startPosition.Y;
                    Positions.Add(new Vector2(x, y));
                }
            }
        }
    }
}