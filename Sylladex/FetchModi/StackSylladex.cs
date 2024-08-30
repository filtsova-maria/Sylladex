using Microsoft.Xna.Framework;
using Sylladex.Entities;
using System.Diagnostics;

namespace Sylladex.FetchModi
{
    public class StackSylladex : SylladexModus
    {
        public static string GetName() => "Stack";
        public static Color GetColor() => Color.HotPink;
        public override string Name => GetName();
        public override bool[] SlotEnabledMask { get; }
        public StackSylladex(ref Item?[] items) : base(ref items)
        {
            Tint = GetColor();
            SlotEnabledMask = new bool[_items.Length];
            SlotEnabledMask[0] = true;
            for (int i = 1; i < SlotEnabledMask.Length; i++)
            {
                SlotEnabledMask[i] = false;
            }
        }
        public override void InsertItem(Item item)
        {
            if (IsFull)
            {
                // If no empty card is available, replace the top card
                Debug.WriteLine($"Inventory full, replacing {_items[0]!.Name} with {item.Name}");
                EjectFromInventory(_items[0]!);
                MoveToInventory(item);
                return;
            }
            Item? propagatedItem = null;
            for (int i = 0; i < _items.Length - 1; i++)
            {
                Item? cur = _items[i];
                Item? next = _items[i + 1];
                // If the current slot is empty, fill it
                if (cur is null)
                {
                    MoveToInventory(item, i);
                    Debug.WriteLine($"Inserted item: {item.Name} to index {i}");
                    return;
                }
                // If the current slot is full and the next slot is empty, push the current item to the right and store the new item in its place
                else if (next is null)
                {
                    if (propagatedItem is null)
                    {
                        _items[i + 1] = cur; // push the current item to the right
                        MoveToInventory(item, i);
                        Debug.WriteLine($"Inserted item: {item.Name} to index {i}, pushed {cur.Name} to the right");
                        return;
                    }
                    _items[i + 1] = propagatedItem; // store pushed item to the next available slot
                    return;
                }
                // If the current slot is full and the next slot is full, store the current item and push the rest to the right
                else
                {
                    if (propagatedItem is null)
                    {
                        MoveToInventory(item, i);
                        _items[i + 1] = cur;
                        Debug.WriteLine($"Inserted item: {item.Name} to index {i}");
                    }
                    else
                    {
                        _items[i + 1] = propagatedItem;
                    }
                    propagatedItem = next;
                    Debug.WriteLine($"Propagating {propagatedItem.Name} to the right");
                }
            }
        }

        public override void FetchItem(Item item)
        {
            Item? itemToFetch = _items[0];
            if (itemToFetch is null)
            {
                Debug.WriteLine($"Failed to fetch item: {item.Name}");
                return;
            }
            EjectFromInventory(itemToFetch, 0);

            // Bubble items to the top
            for (int i = 0; i < _items.Length - 1; i++)
            {
                Item? cur = _items[i];
                Item? next = _items[i + 1];
                if (cur is null && next is not null)
                {
                    _items[i] = next;
                    _items[i + 1] = null;
                }
            }
        }
    }
}
