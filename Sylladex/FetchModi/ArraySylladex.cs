using Microsoft.Xna.Framework;
using Sylladex.Entities;

namespace Sylladex.FetchModi
{
    public class ArraySylladex : SylladexModus
    {
        public static string GetName() => "Array";
        public static Color GetColor() => Color.SkyBlue;
        public override string Name => GetName();
        public override bool[] SlotEnabledMask { get; }
        public ArraySylladex(ref Item?[] items) : base(ref items)
        {
            Tint = GetColor();
            SlotEnabledMask = new bool[_items.Length];
            for (int i = 0; i < SlotEnabledMask.Length; i++)
            {
                SlotEnabledMask[i] = true;
            }
        }
    }
}