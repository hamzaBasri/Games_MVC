using Games.DataAccess.Repository.IRepository;
using Games.Models;
using Games.DataAccess.Data;

namespace Games.DataAccess.Repository
{
    public class GameListingRepository : Repository<GameListing>, IGameListingRepository
    {
        private readonly ApplicationDbContext _db;

        public GameListingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(GameListing obj) 
        { 
            _db.GameListings.Update(obj);
        }
    }
}
