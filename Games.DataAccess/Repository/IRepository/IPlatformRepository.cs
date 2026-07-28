using Games.Models;

namespace Games.DataAccess.Repository.IRepository
{
    public interface IPlatformRepository : IRepository<Platform>
    {
        void Update(Platform obj);
    }
}
