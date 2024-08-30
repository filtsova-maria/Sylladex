using Microsoft.Xna.Framework;
using Sylladex.Entities;
using Sylladex.Managers;
using System.Diagnostics;
using System.Linq;

namespace Sylladex.FetchModi
{
    /// <summary>
    /// Base class of a sylladex modus. Modus represents the way player can interact with their inventory, item fetch and insert.
    /// </summary>
    public abstract class SylladexModus
    {
        protected Item?[] _items;
        public Color Tint { get; set; } = Color.White;
        /// <summary>
        /// Represents accessibility of the inventory slots for fetching.
        /// </summary>
        public abstract bool[] SlotEnabledMask { get; }
        /// <summary>
        /// Display name of the sylladex modus.
        /// </summary>
        public abstract string Name { get; }
        public bool IsFull
        {
            get => !_items.Any(item => item is null);
        }
        public SylladexModus(ref Item?[] items)
        {
            _items = items;
        }

        /// <summary>
        /// Move item from inventory at a given index to the game world
        /// </summary>
        protected void EjectFromInventory(Item item, int? index = 0)
        {
            Item itemToBeEjected = new Item(item.Name, item.Texture, GameManager.EntityManager.GetObject("player").Position);
            GameManager.EntityManager.AddObject(itemToBeEjected.Name, itemToBeEjected);
            if (index is not null)
            {
                _items[(int)index] = null;
            }
            Debug.WriteLine($"Ejected item: {itemToBeEjected.Name}");
        }
        /// <summary>
        /// Move item from the game world to an inventory slot at a given index
        /// </summary>
        protected void MoveToInventory(Item item, int index = 0)
        {
            _items[index] = item;
            GameManager.EntityManager.RemoveObject(item.Name);
            Debug.WriteLine($"Added item: {item.Name} to index {index}");
        }
        public virtual void InsertItem(Item item)
        {
            // Try inserting the item into the first available empty card
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] is null)
                {
                    MoveToInventory(item, i);
                    return;
                }
            }
            // If no empty card is available, replace the first card
            Debug.WriteLine($"Inventory full, replacing {_items[0]!.Name} with {item.Name}");
            EjectFromInventory(_items[0]!);
            MoveToInventory(item);
        }
        public virtual void FetchItem(Item item)
        {
            // Default Fetch mode is to go through the inventory and find the item to fetch
            Item? itemToFetch = null;
            int fetchIndex = -1;
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] is not null && _items[i]!.Name == item.Name)
                {
                    itemToFetch = _items[i];
                    fetchIndex = i;
                }
            }
            if (itemToFetch is not null)
            {
                _items[fetchIndex] = null;
                EjectFromInventory(itemToFetch, fetchIndex);
                return;
            }
            else
            {
                Debug.WriteLine($"Failed to fetch item: {item.Name}");
            }
        }
    }
}
