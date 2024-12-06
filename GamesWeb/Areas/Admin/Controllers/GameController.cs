using Games.DataAccess.Repository.IRepository;
using Games.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamesWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GameController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public GameController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Game> games = _unitOfWork.Game.GetAll().ToList();
            return View(games);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Game game)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Game.Add(game);
                _unitOfWork.Save();
                TempData["success"] = "Le jeu a été ajouté avec succès";
                return RedirectToAction("Index");
            }
            return View(game);

        }

        public IActionResult Edit(int? id)
        {
           Game? game = _unitOfWork.Game.Get(u => u.Id == id);
            
                
           if (game == null || id == null || id == 0)
           {
                return NotFound();
           }
           return View(game);

        }
        [HttpPost]
        public IActionResult Edit(Game game)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.Game.Update(game);
                _unitOfWork.Save();
                TempData["success"] = "Le jeu a été modifié avec succès";
                return RedirectToAction("Index");
            }
            return View(game);
        }
        public IActionResult Delete(int? id)
        { 
            Game? game = _unitOfWork.Game.Get(u => u.Id == id);
            if (game == null)
            {
                TempData["error"] = "Le jeu n'existe pas";
                return NotFound();
            }
            return View(game);
            
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Game? game = _unitOfWork.Game.Get(u => u.Id == id);
            if (game == null)
            {
                
                return NotFound();
            }
            _unitOfWork.Game.Remove(game);
            _unitOfWork.Save();
            TempData["success"] = "Le jeu a été supprimé avec succès";
            return RedirectToAction("Index");
        }
    }
}
