using Games.DataAccess.Repository.IRepository;
using Games.Models;
using Games.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            GameVM gameVM = new()
            {
                CategoryList = _unitOfWork.Category
                .GetAll()
                .Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                Game = new Game()
                
            };
            return View(gameVM);
        }
        [HttpPost]
        public IActionResult Create(GameVM gameVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Game.Add(gameVM.Game);
                _unitOfWork.Save();
                TempData["success"] = "Le jeu a été ajouté avec succès";
                return RedirectToAction("Index");
            }
            else
            {
                gameVM.CategoryList = _unitOfWork.Category
                    .GetAll()
                    .Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return View(gameVM);
            }           
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
