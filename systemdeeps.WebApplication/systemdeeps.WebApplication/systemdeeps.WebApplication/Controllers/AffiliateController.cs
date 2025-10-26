using Microsoft.AspNetCore.Mvc;
using systemdeeps.WebApplication.Models;
using systemdeeps.WebApplication.Services;

namespace systemdeeps.WebApplication.Controllers
{
    // Handles web requests for affiliates
    public class AffiliateController : Controller
    {
        private readonly AffiliateService _service;

        public AffiliateController(AffiliateService service)
        {
            _service = service;
        }

        // GET: /Affiliate
        [HttpGet]
        public IActionResult Index()
        {
            var affiliates = _service.GetAll();
            return View(affiliates);
        }

        // GET: /Affiliate/Create
        [HttpGet]
        public IActionResult Create() => View();

        // POST: /Affiliate/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Affiliate affiliate)
        {
            if (!ModelState.IsValid) return View(affiliate);
            _service.Create(affiliate);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Affiliate/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var affiliate = _service.GetById(id);
            if (affiliate == null) return NotFound();
            return View(affiliate);
        }

        // POST: /Affiliate/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Affiliate affiliate)
        {
            if (!ModelState.IsValid) return View(affiliate);
            _service.Update(affiliate);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Affiliate/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var affiliate = _service.GetById(id);
            if (affiliate == null) return NotFound();
            return View(affiliate);
        }

        // POST: /Affiliate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}