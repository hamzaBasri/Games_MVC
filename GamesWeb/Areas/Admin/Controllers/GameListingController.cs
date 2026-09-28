using Games.DataAccess.Repository.IRepository;
using Games.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Games.Models;
using Games.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GamesWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class GameListingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public GameListingController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(int gameId)
        {
            var game = _unitOfWork.Game.Get(g => g.Id == gameId);
            if(game == null)
            {
                return NotFound();
            }          
            ViewBag.GameTitle = game.Title;           
            ViewBag.GameId = gameId;
            ViewBag.GameImageUrl = game.ImageUrl;
            return View();
        }

        [HttpGet]             
        public IActionResult Upsert(int gameId, int? gameListingId)
        {
            var game = _unitOfWork.Game.Get(g => g.Id == gameId);
            if (game == null)
            {
                return NotFound();
            }
            ViewBag.GameTitle = game.Title;
            ViewBag.GameImageUrl = game.ImageUrl;
            GameListingVM gameListingVM = new()
            {
                GameListing = new GameListing() { GameId = gameId },
                PlatformList = _unitOfWork.Platform.GetAll().Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Id.ToString()
                })

            };
            if (gameListingId == null || gameListingId == 0)
            {
                return View(gameListingVM);
            }
            else
            {
                gameListingVM.GameListing = _unitOfWork.GameListing.Get(gl => gl.Id == gameListingId);
                return View(gameListingVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(GameListing gameListing, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                var listingExists = _unitOfWork.GameListing.Get(gl => gl.GameId == gameListing.GameId && gl.PlatformId == gameListing.PlatformId && gl.Id != gameListing.Id);
                if (listingExists != null)
                {
                    ModelState.AddModelError("GameListing.PlatformId", "un listing pour ce jeu et cette plateforme existe déjà.");
                    GameListingVM gameListingVM = new()
                    {
                        GameListing = gameListing,
                        PlatformList = _unitOfWork.Platform.GetAll().Select(p => new SelectListItem
                        {
                            Text = p.Name,
                            Value = p.Id.ToString()
                        })
                    };
                    return View(gameListingVM);
                }
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string gameListingPath = Path.Combine(wwwRootPath, @"images\gamelisting");

                    if (!string.IsNullOrEmpty(gameListing.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, gameListing.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(gameListingPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    gameListing.ImageUrl = @"\images\gamelisting\" + fileName;
                }

                if (gameListing.Id == 0)
                {
                    _unitOfWork.GameListing.Add(gameListing);
                }
                else
                {
                    _unitOfWork.GameListing.Update(gameListing);
                }
                _unitOfWork.Save();
                TempData["success"] = "GameListing enregistrer avec succès";
                return RedirectToAction("Index", new { gameId = gameListing.GameId });
            }
            else 
            {
                GameListingVM gameListingVM = new()
                {
                    GameListing = gameListing,
                    PlatformList = _unitOfWork.Platform.GetAll().Select(p => new SelectListItem
                    {
                        Text = p.Name,
                        Value = p.Id.ToString()
                    })
                };
                return View(gameListingVM);
            }
        }

        [HttpGet]
        public IActionResult GetAll(int gameId)
        { 
            List<GameListing> objgameListings = _unitOfWork.GameListing.GetAll(includeProperties:"Platform").Where(gl => gl.GameId == gameId).ToList();
            return Json(new {data = objgameListings });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var gameListingToBeDeleted =_unitOfWork.GameListing.Get(gl => gl.Id == id);
            if (gameListingToBeDeleted == null)
            {
                return Json(new { success = false, message = "Erreur lors de la suppression" });
            }
            if (!string.IsNullOrEmpty(gameListingToBeDeleted.ImageUrl))
            { 
                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, gameListingToBeDeleted.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
                
            }
            _unitOfWork.GameListing.Remove(gameListingToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "GameListing supprimé avec succès" });
        }
    }
}
