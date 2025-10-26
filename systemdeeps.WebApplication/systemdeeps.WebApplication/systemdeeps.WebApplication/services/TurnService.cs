using systemdeeps.WebApplication.Interfaces;
using systemdeeps.WebApplication.Models;

namespace systemdeeps.WebApplication.Services
{
    // Business layer: turn queue logic + full CRUD
    public class TurnService
    {
        private readonly IGenericRepository<Turn> _repo;

        public TurnService(IGenericRepository<Turn> repo)
        {
            _repo = repo;
        }

        // Read all turns ordered by date
        public IEnumerable<Turn> GetAll() =>
            _repo.GetAll().OrderBy(t => t.DateTime).ToList();

        public Turn? GetById(int id) => _repo.GetById(id);

        // Create a new turn with next sequential number
        public Turn CreateNewTurn(int affiliateId, string? description = null)
        {
            int lastNumber = _repo.GetAll().Any()
                ? _repo.GetAll().Max(t => t.Number)
                : 0;

            var newTurn = new Turn
            {
                Number = lastNumber + 1,
                Status = "Pending",
                Description = description,
                DateTime = DateTime.Now,
                AffiliateId = affiliateId
            };

            _repo.Add(newTurn);
            _repo.Save();
            return newTurn;
        }

        // Move to the next pending turn
        public Turn? NextTurn()
        {
            var current = _repo.GetAll().FirstOrDefault(t => t.Status == "Attending");
            if (current != null)
            {
                current.Status = "Completed";
                _repo.Update(current);
            }

            var next = _repo.GetAll()
                .Where(t => t.Status == "Pending")
                .OrderBy(t => t.DateTime)
                .FirstOrDefault();

            if (next != null)
            {
                next.Status = "Attending";
                _repo.Update(next);
            }

            _repo.Save();
            return next;
        }

        // Delete one turn
        public void Delete(int id)
        {
            _repo.Delete(id);
            _repo.Save();
        }

        // Reset queue (delete all turns)
        public void ResetQueue()
        {
            var all = _repo.GetAll().ToList();
            foreach (var t in all)
                _repo.Delete(t.Id);

            _repo.Save();
        }

        // Helpers
        public IEnumerable<Turn> GetPending() =>
            _repo.GetAll().Where(t => t.Status == "Pending").OrderBy(t => t.DateTime).ToList();

        public Turn? GetCurrentTurn() =>
            _repo.GetAll().FirstOrDefault(t => t.Status == "Attending");
    }
}
