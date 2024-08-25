using Microsoft.Xna.Framework;
using Sylladex.Entities;

namespace Sylladex.FetchModi
{
    public class HashSylladex : SylladexModus
    {
        public HashSylladex(ref Item?[] items) : base(ref items)
        {
            Tint = Color.Yellow;
        }
    }
}
