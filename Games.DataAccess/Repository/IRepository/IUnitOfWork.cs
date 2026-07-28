using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IGameRepository Game { get; }
        IPlatformRepository Platform { get; }
        void Save();
    }
}
