using Microsoft.Xna.Framework;
using Sylladex.Entities;

namespace Sylladex.FetchModi
{
    /// <summary>
    /// Represents the Array modus. Items are stored in the first available slot and can be retrieved in any order.
    /// </summary>
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