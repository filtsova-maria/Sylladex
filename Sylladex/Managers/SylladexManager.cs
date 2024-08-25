using Microsoft.Xna.Framework;
using Sylladex.Entities;
using Sylladex.FetchModi;
using Sylladex.UI;
using System.Collections.Generic;
using System.Diagnostics;

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
        public Item?[] Items;
        public SylladexManager(List<SylladexCard> cards)
        {
            Cards = cards;
            Items = new Item?[NumberOfCards];
            for (int i = 0; i < NumberOfCards; i++)
            {
                Items[i] = null;
            }
            Modus = new ArraySylladex(ref Items);
            foreach (var card in Cards)
            {
                card.Tint = Modus.Tint;
            }
        }

        public void InsertItem(Item item)
        {
            Modus.InsertItem(item);
        }

        public void FetchItem(Item item)
        {
            Modus.FetchItem(item);
        }

        public void Update()
        {
            for (int i = 0; i < NumberOfCards; i++)
            {
                if (Cards[i].Item != Items[i])
                {
                    Cards[i].Item = Items[i];
                }
                Cards[i].Update();
            }
        }
    }
}
