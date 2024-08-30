using Microsoft.Xna.Framework;
using Sylladex.Entities;
using Sylladex.FetchModi;
using Sylladex.UI;
using System.Collections.Generic;

namespace Sylladex.Managers
{
    public enum SylladexModusType
    {
        Hash,
        Queue,
        Stack,
        Array
    }
    public enum SylladexModusParameter
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
        private readonly Label _statusLabel;
        public SylladexManager(List<SylladexCard> cards, Label statusLabel)
        {
            Cards = cards;
            Items = new Item?[NumberOfCards];
            for (int i = 0; i < NumberOfCards; i++)
            {
                Items[i] = null;
            }
            FetchModus = new ArraySylladex(ref Items);
            InsertModus = new ArraySylladex(ref Items);
            DimensionModus = new ArraySylladex(ref Items);
            _statusLabel = statusLabel;
            _statusLabel.SetText($"Sylladex:{FetchModus.Name}::{InsertModus.Name}::{DimensionModus.Name}");
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

        public void SetModus(SylladexModusParameter parameter, SylladexModusType type)
        {
            SylladexModus modus = type switch
            {
                SylladexModusType.Hash => new HashSylladex(ref Items),
                SylladexModusType.Queue => new QueueSylladex(ref Items),
                SylladexModusType.Stack => new StackSylladex(ref Items),
                SylladexModusType.Array => new ArraySylladex(ref Items),
                _ => FetchModus
            };
            switch (parameter)
            {
                case SylladexModusParameter.Fetch:
                    FetchModus = modus;
                    for (int i = 0; i < Cards.Count; i++)
                    {
                        Cards[i].IsEnabled = FetchModus.SlotEnabledMask[i];
                        Cards[i].Tint = FetchModus.Tint;
                    }
                    break;
                case SylladexModusParameter.Insert:
                    InsertModus = modus;
                    break;
                case SylladexModusParameter.Dimension:
                    DimensionModus = modus;
                    break;
                default:
                    break;
            }
            _statusLabel.SetText($"Sylladex:{FetchModus.Name}::{InsertModus.Name}::{DimensionModus.Name}");
        }

        public void Update()
        {
            for (int i = 0; i < NumberOfCards; i++)
            {
                if (Cards[i].Item != Items[i])
                {
                    Cards[i].Item = Items[i];
                }
            }
        }
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

// TODO: disabled mask Disabled bool[NumberOfCards] defined in each modus, all enabled by default
// disable cards in constructor based on the modus Disabled mask
// update it on change of modus in some changeXModus method
// TODO: each sylladex should have Name property, write it as $"Sylladex:{SylladexManager.InsertModus.Name}::{SylladexManager.FetchModus.Name}::{SylladexManager.DisplayModus.Name}"
// fyi stack InsertItem will be very similar to queue except first card being replaced on full