using Newtonsoft.Json.Linq;

namespace GC_LAB_CARDS_API.Models
{
    public class CardsVM
    {
        public string code { get; set; }
        public string image { get; set; }
        public string value { get; set; }
        public string suit { get; set; }
        public bool IsChecked { get; set; } = false;

        public static IEnumerable<CardsVM> GetCardsVMFromDrawCardAPI(DrawCardAPI cards)
        {
            return cards.cards
                .Select(c => new CardsVM()
                {
                    code = c.code,
                    image = c.image,
                    value = c.value,
                    suit = c.suit,
                });
        }
        public static IEnumerable<CardsVM> GetCardsVMFromCollection(IFormCollection collection)
        {
            var keys = collection.Keys.ToList();
            int amountOfCards = collection[keys[0]]
                .ToString()
                .Split(',')
                .Count();
            CardsVM[] cards = new CardsVM[amountOfCards]
                .Select(c=>new CardsVM())
                .ToArray();
            foreach(var key in collection.Keys)
            {
                List<string> values = collection[key]
                    .ToString()
                    .Split(',')
                    .ToList();
                for (int i = 0; i < values.Count(); i++)
                {
                    if (i == 5)
                    {
                        break;
                    }
                    switch (key.ToString())
                    {
                        case "item.code":
                            cards[i].code = values[i];
                            break;
                        case "item.image":
                            cards[i].image = values[i];
                            break;
                        case "item.value":
                            cards[i].value = values[i];
                            break;
                        case "item.suit":
                            cards[i].suit = values[i];
                            break;
                        case "item.IsChecked":
                            Console.WriteLine(values.Count() + "   wtf" );
                            cards[i].IsChecked = values[i]
                                .ToString() == "true";
                            break;
                        default:
                            break;
                    }
                }
            }
            return cards
                .Where(c=>c.IsChecked == true)
                .OrderBy(c=>c.suit);
        }
    }
}
