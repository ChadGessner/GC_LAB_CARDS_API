namespace GC_LAB_CARDS_API.Models
{
    public class DeckAPI
    {
        public bool success { get; set; }
        public string deck_id { get; set; }
        public bool shuffled { get; set; }
        public int remaining { get; set; }
    }
}
