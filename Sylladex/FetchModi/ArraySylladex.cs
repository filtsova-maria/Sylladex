using Microsoft.Xna.Framework;
using Sylladex.Entities;

namespace Sylladex.FetchModi
{
    public class ArraySylladex : SylladexModus
    {
        public ArraySylladex(ref Item?[] items) : base(ref items)
        {
            Tint = Color.SkyBlue;
        }
    }
}