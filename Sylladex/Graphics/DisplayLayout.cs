using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Sylladex.Graphics
{

    /// <summary>
    /// Generates the layout for multiple UI items, storing the positions of elements.
    /// </summary>
    public struct DisplayLayout
    {
        public List<Vector2> Positions;

        /// <summary>
        /// Initializes a new instance of the DisplayLayout struct.
        /// </summary>
        /// <param name="startPosition">The starting position of the layout.</param>
        /// <param name="count">The number of elements in the layout.</param>
        /// <param name="elementSize">The size of each element. Use height for vertical and width for horizontal mode.</param>
        /// <param name="padding">The padding between elements.</param>
        /// <param name="vertical">Specifies whether the layout is vertical or horizontal. Default is horizontal.</param>
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
                }
                else
                {
                    int x = (int)startPosition.X + i * (elementSize + padding);
                    int y = (int)startPosition.Y;
                    Positions.Add(new Vector2(x, y));
                }
            }
        }
    }
}