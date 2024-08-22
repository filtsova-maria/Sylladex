using Microsoft.Xna.Framework;
using Sylladex.UI;

namespace Sylladex.Managers
{
    public enum FetchModus
    {
        Array,
        Stack,
        Queue,
        Hashmap
    }
    /// <summary>
    /// Represents the inventory system of the player.
    /// </summary>
    public class SylladexManager
    {
        public const int NumberOfCards = 3; // Number of inventory slots, can potentially made dynamic in the future
        public FetchModus Modus { get; set; } = FetchModus.Array;
        public SylladexCard[] Cards { get; set; }
        public SylladexManager(SylladexCard[] cards)
        {
            Cards = cards;
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
