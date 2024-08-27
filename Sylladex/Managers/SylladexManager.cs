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
        public SylladexModus FetchModus { get; set; }
        public SylladexModus InsertModus { get; set; }
        public SylladexModus DimensionModus { get; set; }
        /// <summary>
        /// UI representation of the inventory slots. Must be kept in sync with <c>SylladexManager.Items</c>.
        /// </summary>
        public List<SylladexCard> Cards { get; set; }
        /// <summary>
        /// Logical representation of inventory slots. Acts as a single source of truth for all modi.
        /// </summary>
        public Item?[] Items;
        public SylladexManager(List<SylladexCard> cards)
        {
            Cards = cards; 
            Items = new Item?[NumberOfCards];
            for (int i = 0; i < NumberOfCards; i++)
            {
                Items[i] = null;
            }
            FetchModus = new QueueSylladex(ref Items);
            InsertModus = new QueueSylladex(ref Items);
            DimensionModus = new QueueSylladex(ref Items);
            for (int i = 0; i < Cards.Count; i++)
            {
                Cards[i].Tint = DimensionModus.Tint;
                Cards[i].IsEnabled = FetchModus.SlotEnabledMask[i];
            }
        }

        public void InsertItem(Item item)
        {
            InsertModus.InsertItem(item);
        }

        public void FetchItem(Item item)
        {
            FetchModus.FetchItem(item);
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

// TODO: disabled mask Disabled bool[NumberOfCards] defined in each modus, all enabled by default
// disable cards in constructor based on the modus Disabled mask
// update it on change of modus in some changeXModus method
// TODO: each sylladex should have Name property, write it as $"Sylladex:{SylladexManager.InsertModus.Name}::{SylladexManager.FetchModus.Name}::{SylladexManager.DisplayModus.Name}"
// fyi stack InsertItem will be very similar to queue except first card being replaced on full