using Games.DataAccess.Data;
using Games.DataAccess.Repository.IRepository;
using Games.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.DataAccess.Repository
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        private readonly ApplicationDbContext _db;
        public GameRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Game obj)
        {
            _db.Games.Update(obj);
        }
    }
}
