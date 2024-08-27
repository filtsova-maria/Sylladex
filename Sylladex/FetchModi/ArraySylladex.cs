using Microsoft.Xna.Framework;
using Sylladex.Entities;

namespace Sylladex.FetchModi
{
    public class ArraySylladex : SylladexModus
    {
        public override string Name => "Array";
        public override bool[] SlotEnabledMask { get; }
        public ArraySylladex(ref Item?[] items) : base(ref items)
        {
            Tint = Color.SkyBlue;
            SlotEnabledMask = new bool[_items.Length];
            for (int i = 0; i < SlotEnabledMask.Length; i++)
            {
                SlotEnabledMask[i] = true;
            }
        }
    }
}