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
            /*TODO 23-07-2026 : BUG DÉTECTÉ : Dans GameRepository.cs, la méthode Update() a un bug de logique. 
             * Le code met à jour Title, Description, Producer, les 3 prix et CategoryId uniquement 
             * à l'intérieur d'un bloc if (obj.ImageUrl != null). Résultat concret : si tu modifies 
             * un jeu sans changer son image (le cas le plus fréquent), obj.ImageUrl reste null, 
             * donc aucun champ n'est sauvegardé, même si le formulaire indique un succès. C'est probablement 
             * la cause du bug de prix repéré sur le site (Mario Bros 3 / Hogwarts Legacy) : 
             * quelqu'un a dû modifier le prix sans re-uploader une image, et le changement a été silencieusement ignoré. 
             * Correction suggérée : sortir les 6 premières lignes d'affectation en dehors du if, pour qu'elles s'exécutent toujours 
             * — ne garder le if que pour la ligne ImageUrl. */
            var objFromDb = _db.Games.FirstOrDefault(s => s.Id == obj.Id);
            if (objFromDb != null) 
            {
                if (obj.ImageUrl != null)
                {
                    objFromDb.Title = obj.Title;
                    objFromDb.Description = obj.Description;
                    objFromDb.Producer = obj.Producer;
                    objFromDb.ListPrice = obj.ListPrice;
                    objFromDb.PriceWalmart = obj.PriceWalmart;
                    objFromDb.PriceAmazon = obj.PriceAmazon;
                    objFromDb.PriceABGames = obj.PriceABGames;
                    objFromDb.CategoryId = obj.CategoryId;
                    if (obj.ImageUrl != null)
                    {
                        objFromDb.ImageUrl = obj.ImageUrl;
                    }
                }
                
            }
        }
    }
}
