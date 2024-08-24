using Microsoft.Xna.Framework;
using Sylladex.Entities;
using Sylladex.Managers;
using System.Collections.Generic;
using System.Diagnostics;

namespace Sylladex.FetchModi
{
    public abstract class FetchModus
    {
        protected List<Item> _items;
        public Color Tint { get; set; } = Color.White;
        public FetchModus(ref List<Item> items)
        {
            _items = items;
        }
        public virtual void InsertItem(Item item)
        {
            bool inserted = false;
            foreach (var card in GameManager.SylladexManager.Cards)
            {
                if (card.Item is null)
                {
                    card.Item = item;
                    _items.Add(item);
                    inserted = true;
                    Debug.WriteLine($"Added item: {item.Name}");
                    break;
                    // FIXME: still goes through the entire card list for some reason
                }
            }
            if (!inserted)
            {
                Debug.WriteLine("No empty card available");
            }
        }
        public virtual Item FetchItem(Item item)
        {
            Item itemToFetch = _items.Find((match) => match.Name == item.Name) ?? _items[0];
            Debug.WriteLine($"Fetched item: {itemToFetch.Name}");
            return itemToFetch;
        }
    }

    public class ArraySylladex : FetchModus
    {
        public ArraySylladex(ref List<Item> items) : base(ref items)
        {
            Tint = Color.Blue;
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
