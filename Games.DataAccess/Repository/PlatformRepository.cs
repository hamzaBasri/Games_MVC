using Games.DataAccess.Data;
using Games.DataAccess.Repository.IRepository;
using Games.Models;

namespace Games.DataAccess.Repository
{
    public class PlatformRepository : Repository<Platform>, IPlatformRepository
    {
        private readonly ApplicationDbContext _db;

        public PlatformRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Platform obj)
        {
            _db.Platforms.Update(obj);
        }
    }
}