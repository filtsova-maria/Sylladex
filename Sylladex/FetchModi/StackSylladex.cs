using Microsoft.Xna.Framework;
using Sylladex.Entities;

namespace Sylladex.FetchModi
{
    public class StackSylladex : SylladexModus
    {
        public override string Name => "Stack";
        public override bool[] SlotEnabledMask { get; }
        public StackSylladex(ref Item?[] items) : base(ref items)
        {
            Tint = Color.HotPink;
            SlotEnabledMask = new bool[_items.Length];
            SlotEnabledMask[0] = true;
            for (int i = 1; i < SlotEnabledMask.Length; i++)
            {
                SlotEnabledMask[i] = false;
            }
        }
    }
}
