using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using systemdeeps.WebApplication.Hubs;
using systemdeeps.WebApplication.Services;

namespace systemdeeps.WebApplication.Controllers
{
    // Handles web requests related to turns
    public class TurnController : Controller
    {
        private readonly TurnService _service;
        private readonly IHubContext<TurnHub> _hub;

        public TurnController(TurnService service, IHubContext<TurnHub> hub)
        {
            _service = service;
            _hub = hub;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var turns = _service.GetAll();
            return View(turns);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int affiliateId, string? description)
        {
            var newTurn = _service.CreateNewTurn(affiliateId, description);
            await _hub.Clients.All.SendAsync("newTurnCreated", newTurn.Number, newTurn.Status);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Next()
        {
            var next = _service.NextTurn();
            if (next != null)
                await _hub.Clients.All.SendAsync("currentTurnChanged", next.Number, next.Status);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reset()
        {
            _service.ResetQueue();
            await _hub.Clients.All.SendAsync("queueUpdated");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Tv() => View(); // looks for Views/Turn/Tv.cshtml
    }
}