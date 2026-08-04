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
            var objFromDb = _db.Games.FirstOrDefault(s => s.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.Description = obj.Description;
                objFromDb.Producer = obj.Producer;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.PriceWalmart = obj.PriceWalmart;
                objFromDb.PriceAmazon = obj.PriceAmazon;
                objFromDb.PriceABGames = obj.PriceABGames;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.Platforms = obj.Platforms;

                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
