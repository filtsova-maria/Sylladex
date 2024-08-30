using Microsoft.Xna.Framework;
using Sylladex.Entities;
using Sylladex.FetchModi;
using Sylladex.UI;
using System.Collections.Generic;

namespace Sylladex.Managers
{
    /// <summary>
    /// Represents available modi.
    /// </summary>
    public enum SylladexModusType
    {
        Hash,
        Queue,
        Stack,
        Array
    }
    /// <summary>
    /// Represents modus configurations as different modi can be used for fetching and inserting.
    /// </summary>
    public enum SylladexModusAction
    {
        Fetch,
        Insert,
        Dimension
    }
    /// <summary>
    /// Represents the inventory system of the player.
    /// </summary>
    public class SylladexManager
    {
        public const int NumberOfCards = 3; // Number of inventory slots, can potentially made dynamic in the future
        /// <summary>
        /// Modus used for fetching items from the inventory.
        /// </summary>
        public SylladexModus FetchModus { get; set; }
        /// <summary>
        /// Modus used for inserting items into the inventory.
        /// </summary>
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
        private readonly Label _statusLabel;
        public SylladexManager(List<SylladexCard> cards, Label statusLabel)
        {
            Cards = cards;
            // Initialize all slots as empty
            Items = new Item?[NumberOfCards];
            for (int i = 0; i < NumberOfCards; i++)
            {
                Items[i] = null;
            }
            // Set default modi
            FetchModus = new ArraySylladex(ref Items);
            InsertModus = new ArraySylladex(ref Items);
            DimensionModus = new ArraySylladex(ref Items);
            // Set status label
            _statusLabel = statusLabel;
            _statusLabel.SetText($"Sylladex:{FetchModus.Name}::{InsertModus.Name}::{DimensionModus.Name}");
            // Configure sylladex cards
            for (int i = 0; i < Cards.Count; i++)
            {
                Cards[i].Tint = FetchModus.Tint;
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
        /// <summary>
        /// Set modus for fetching, inserting or dimension.
        /// </summary>
        /// <param name="action">action</param>
        /// <param name="type">modus</param>
        public void SetModus(SylladexModusAction action, SylladexModusType type)
        {
            SylladexModus modus = type switch
            {
                SylladexModusType.Hash => new HashSylladex(ref Items),
                SylladexModusType.Queue => new QueueSylladex(ref Items),
                SylladexModusType.Stack => new StackSylladex(ref Items),
                SylladexModusType.Array => new ArraySylladex(ref Items),
                _ => FetchModus
            };
            switch (action)
            {
                case SylladexModusAction.Fetch:
                    FetchModus = modus;
                    // Fetch modus determines card color and availability
                    for (int i = 0; i < Cards.Count; i++)
                    {
                        Cards[i].IsEnabled = FetchModus.SlotEnabledMask[i];
                        Cards[i].Tint = FetchModus.Tint;
                    }
                    break;
                case SylladexModusAction.Insert:
                    InsertModus = modus;
                    break;
                case SylladexModusAction.Dimension:
                    DimensionModus = modus;
                    break;
                default:
                    break;
            }
            _statusLabel.SetText($"Sylladex:{FetchModus.Name}::{InsertModus.Name}::{DimensionModus.Name}");
        }

        public void Update()
        {
            // Keep UI in sync with logical representation
            for (int i = 0; i < NumberOfCards; i++)
            {
                if (Cards[i].Item != Items[i])
                {
                    Cards[i].Item = Items[i];
                }
            }
        }
        /// <summary>
        /// Get color of a given modus
        /// </summary>
        public static Color GetModusColor(SylladexModusType modusType)
        {
            return modusType switch
            {
                SylladexModusType.Array => ArraySylladex.GetColor(),
                SylladexModusType.Queue => QueueSylladex.GetColor(),
                SylladexModusType.Stack => StackSylladex.GetColor(),
                SylladexModusType.Hash => HashSylladex.GetColor(),
                _ => Color.White
            };
        }
        /// <summary>
        /// Get name of a given modus.
        /// </summary>
        public static string GetModusName(SylladexModusType modusType)
        {
            return modusType switch
            {
                SylladexModusType.Array => ArraySylladex.GetName(),
                SylladexModusType.Queue => QueueSylladex.GetName(),
                SylladexModusType.Stack => StackSylladex.GetName(),
                SylladexModusType.Hash => HashSylladex.GetName(),
                _ => "Unknown"
            };
        }
    }
}
