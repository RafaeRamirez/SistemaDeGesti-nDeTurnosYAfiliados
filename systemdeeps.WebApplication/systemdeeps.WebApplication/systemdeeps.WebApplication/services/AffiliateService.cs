using systemdeeps.WebApplication.Interfaces;
using systemdeeps.WebApplication.Models;

namespace systemdeeps.WebApplication.Services
{
    // Business layer for affiliates (CRUD)
    public class AffiliateService
    {
        private readonly IGenericRepository<Affiliate> _repo;

        public AffiliateService(IGenericRepository<Affiliate> repo)
        {
            _repo = repo;
        }

        public IEnumerable<Affiliate> GetAll() =>
            _repo.GetAll().OrderBy(a => a.FullName).ToList();

        public Affiliate? GetById(int id) => _repo.GetById(id);

        public void Create(Affiliate affiliate)
        {
            affiliate.DateRegistered = DateTime.Now;
            _repo.Add(affiliate);
            _repo.Save();
        }

        public void Update(Affiliate affiliate)
        {
            _repo.Update(affiliate);
            _repo.Save();
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
            _repo.Save();
        }
    }
}