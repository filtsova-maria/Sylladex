using Microsoft.Xna.Framework;
using Sylladex.Entities;
using Sylladex.Managers;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace Sylladex.FetchModi
{
    public abstract class FetchModus
    {
        protected Item?[] _items;
        public Color Tint { get; set; } = Color.White;
        public FetchModus(ref Item?[] items)
        {
            _items = items;
        }
        
        /// <summary>
        /// Move item from inventory to the game world
        /// </summary>
        private void EjectFromInventory(Item item, int index = 0)
        {
            Item itemToBeEjected = new Item(item.Name, item.Texture, GameManager.EntityManager.GetObject("player").Position);
            GameManager.EntityManager.AddObject(itemToBeEjected.Name, itemToBeEjected);
            _items[index] = null;
            Debug.WriteLine($"Ejected item: {itemToBeEjected.Name}");
        }
        /// <summary>
        /// Move item from the game world to an inventory slot
        /// </summary>
        private void MoveToInventory(Item item, int index = 0)
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

    public class ArraySylladex : FetchModus
    {
        public ArraySylladex(ref Item?[] items) : base(ref items)
        {
            Tint = Color.SkyBlue;
        }
    }

    //public class QueueSylladex : FetchModus
    //{
    //    private Queue<IItem> _items;

    //    public QueueSylladex(ref List<IItem> items) : base(ref items)
    //    {
    //        _items = new Queue<IItem>(items);
    //        Tint = Color.Orange;
    //    }

    //    public override void InsertItem(IItem item)
    //    {
    //        _items.Enqueue(item);
    //    }

    //    public override IItem FetchItem()
    //    {
    //        return _items.Count > 0 ? _items.Dequeue() : null;
    //    }
    //}

    //public class StackSylladex : FetchModus
    //{
    //    private Stack<IItem> _items;

    //    public StackSylladex(ref List<IItem> items) : base(ref items)
    //    {
    //        _items = new Stack<IItem>(items);
    //        Tint = Color.HotPink;
    //    }

    //    public override void InsertItem(IItem item)
    //    {
    //        _items.Push(item);
    //    }

    //    public override IItem FetchItem()
    //    {
    //        return _items.Count > 0 ? _items.Pop() : null;
    //    }
    //}
}

// Modus name:
//- Captcha function (default: first available emptiest space, otherwise first card, or append to end)
//- Fetch function (default: all cards)
