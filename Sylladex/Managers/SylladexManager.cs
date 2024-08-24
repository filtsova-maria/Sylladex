using Microsoft.Xna.Framework;
using Sylladex.Entities;
using Sylladex.FetchModi;
using Sylladex.UI;
using System.Collections.Generic;

namespace Sylladex.Managers
{
    /// <summary>
    /// Represents the inventory system of the player.
    /// </summary>
    public class SylladexManager
    {
        public const int NumberOfCards = 3; // Number of inventory slots, can potentially made dynamic in the future
        public FetchModus Modus { get; set; }
        public List<SylladexCard> Cards { get; set; }
        public List<Item> Items;
        public SylladexManager(List<SylladexCard> cards)
        {
            Cards = cards;
            Items = new List<Item>();
            Modus = new ArraySylladex(ref Items);
        }
        public void InsertItem(Item item)
        {
            Modus.InsertItem(item);
        }
        public Item FetchItem(Item item)
        {
            return Modus.FetchItem(item);
        }
        public void Update()
        {
            foreach (var card in Cards)
            {
                card.Update();
            }
        }
    }
}
