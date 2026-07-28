using Games.Models;

namespace Games.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category obj);
        
    }
}
