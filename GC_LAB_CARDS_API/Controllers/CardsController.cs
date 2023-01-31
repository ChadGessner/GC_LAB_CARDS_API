using GC_LAB_CARDS_API.Models;
using Microsoft.AspNetCore.Mvc;
using Flurl.Http;
using System.Linq;

namespace GC_LAB_CARDS_API.Controllers
{
    public class CardsController : Controller
    {
        public string Uri { get; set; } = "https://deckofcardsapi.com/api/deck/";
        public IActionResult Index()
        {
            string apiUri = Uri + "new/shuffle/?deck_count=1";
            var apiTask = apiUri.GetJsonAsync<DeckAPI>();
            apiTask.Wait();
            DeckAPI deck = apiTask.Result;
             
            return View(deck);
        }
        [HttpGet]
        public IActionResult Draw(string id)
        {
            string apiUri = $"{Uri}/{id}/draw/?count=5";
            var apiTask = apiUri.GetJsonAsync<DrawCardAPI>();
            apiTask.Wait();
            DrawCardAPI fiveCards = apiTask.Result;
            return View(CardsVM.GetCardsVMFromDrawCardAPI(fiveCards));
        }
        [HttpPost()]
        public IActionResult Draw(IFormCollection collection, string id)
        {
            IEnumerable<CardsVM> kept = CardsVM.GetCardsVMFromCollection(collection);
            string apiUri = $"{Uri}/{id}/draw/?count={Math.Abs((kept.Count()) - 5)}";
            var apiTask = apiUri.GetJsonAsync<DrawCardAPI>();
            apiTask.Wait();
            DrawCardAPI fiveCards = apiTask.Result;
            IEnumerable<CardsVM> cards = CardsVM.GetCardsVMFromDrawCardAPI(fiveCards);
            return View(cards.Concat(kept));
        }
    }
}
