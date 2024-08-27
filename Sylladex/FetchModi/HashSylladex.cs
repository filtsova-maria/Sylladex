using Microsoft.Xna.Framework;
using Sylladex.Entities;

namespace Sylladex.FetchModi
{
    public class HashSylladex : SylladexModus
    {
        public override string Name => "Hash";
        public override bool[] SlotEnabledMask { get; }
        public HashSylladex(ref Item?[] items) : base(ref items)
        {
            Tint = Color.Yellow;
            SlotEnabledMask = new bool[_items.Length];
            for (int i = 0; i < SlotEnabledMask.Length; i++)
            {
                SlotEnabledMask[i] = true;
            }
        }
    }
}
