using Games.DataAccess.Repository.IRepository;
using Games.Models;
using Games.Models.ViewModels;
using Games.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GamesWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class GameController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public GameController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            List<Game> games = _unitOfWork.Game.GetAll(includeProperties: "Category").ToList();
            
            return View(games);
        }
        public IActionResult Upsert(int? id)
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
            if(id == null || id == 0)
            {
                //Create
                return View(gameVM);
            }
            else
            {
                //Update
                gameVM.Game = _unitOfWork.Game.Get(u => u.Id == id);
                
                return View(gameVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(GameVM gameVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    //File Name
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    //where to register the file
                    string gamePath = Path.Combine(webRootPath, @"images\game");
                    //Save the file

                    if (!string.IsNullOrEmpty(gameVM.Game.ImageUrl))
                    {
                        //Delete the old Image
                        string oldimagePath = Path.Combine(webRootPath, gameVM.Game.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldimagePath))
                        {
                            System.IO.File.Delete(oldimagePath);
                        }


                    }
                    using (var fileStream = new FileStream(Path.Combine(gamePath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    gameVM.Game.ImageUrl = @"\images\game\" + fileName;
                }
                    if (gameVM.Game.Id == 0)
                    {
                        _unitOfWork.Game.Add(gameVM.Game);
                    }
                    else
                    {
                        _unitOfWork.Game.Update(gameVM.Game);
                    }

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

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Game> objGameList = _unitOfWork.Game.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objGameList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var gameToBeDeleted =_unitOfWork.Game.Get(u => u.Id == id);
            if (gameToBeDeleted == null)
            {
                return Json(new { success = false, message = "Erreur lors de la suppression" });
            }
            
            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, gameToBeDeleted.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Game.Remove(gameToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Suppression réussie" });
        }


        #endregion
    }
}
