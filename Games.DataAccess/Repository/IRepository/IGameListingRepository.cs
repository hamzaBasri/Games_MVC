using Games.Models;

namespace Games.DataAccess.Repository.IRepository
{
    public interface IGameListingRepository : IRepository<GameListing>
    {
        void Update(GameListing obj);
    }
}
