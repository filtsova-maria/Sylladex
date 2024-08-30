using Microsoft.Xna.Framework;
using Sylladex.Entities;
using System.Diagnostics;

namespace Sylladex.FetchModi
{
    public class HashSylladex : SylladexModus
    {
        public static string GetName() => "Hash";
        public static Color GetColor() => Color.Yellow;
        public override string Name => GetName();
        public override bool[] SlotEnabledMask { get; }
        public HashSylladex(ref Item?[] items) : base(ref items)
        {
            Tint = GetColor();
            SlotEnabledMask = new bool[_items.Length];
            for (int i = 0; i < SlotEnabledMask.Length; i++)
            {
                SlotEnabledMask[i] = true;
            }
        }
        private int HashFunction(Item item)
        {
            int hash = 0;
            foreach (char c in item.Name)
            {
                hash += (int)c;
            }
            int index = hash % _items.Length;
            Debug.WriteLine($"Hashing item: {item.Name} -> {hash} ({index})");
            return index;
        }

        public override void InsertItem(Item item)
        {
            int index = HashFunction(item);
            if (_items[index] is null)
            {
                MoveToInventory(item, index);
            }
            else
            {
                EjectFromInventory(_items[index]!, index);
                MoveToInventory(item, index);
            }
        }
    }
}
