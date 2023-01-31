namespace GC_LAB_CARDS_API.Models
{
    public class DrawCardAPI
    {
        public bool success { get; set; }
        public string deck_id { get; set; }
        public List<CardAPI> cards { get; set; }
        public int remaining { get; set; }
    }
}
