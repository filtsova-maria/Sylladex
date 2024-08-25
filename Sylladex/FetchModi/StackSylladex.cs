using Microsoft.Xna.Framework;
using Sylladex.Entities;

namespace Sylladex.FetchModi
{
    public class StackSylladex : SylladexModus
    {
        public StackSylladex(ref Item?[] items) : base(ref items)
        {
            Tint = Color.HotPink;
        }
    }
}
