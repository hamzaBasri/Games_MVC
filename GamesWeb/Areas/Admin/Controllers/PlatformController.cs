using Games.DataAccess.Repository.IRepository;
using Games.Models;
using Games.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace GamesWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class PlatformController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PlatformController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Platform platform = new();
            if (id == null || id == 0)
            {
                return View(platform);
            }
            platform = _unitOfWork.Platform.Get(u => u.Id == id);
            return View(platform);
        }

        [HttpPost]
        public IActionResult Upsert(Platform platform, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string platformPath = Path.Combine(wwwRootPath, @"images\platform");

                    if (!string.IsNullOrEmpty(platform.LogoUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, platform.LogoUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(platformPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    platform.LogoUrl = @"\images\platform\" + fileName;
                }

                if (platform.Id == 0)
                {
                    _unitOfWork.Platform.Add(platform);
                }
                else
                {
                    _unitOfWork.Platform.Update(platform);
                }
                _unitOfWork.Save();
                TempData["success"] = "Plateforme enregistrée avec succès";
                return RedirectToAction("Index");
            }
            return View(platform);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Platform> objPlatformList = _unitOfWork.Platform.GetAll().ToList();
            return Json(new { data = objPlatformList });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var platformToBeDeleted = _unitOfWork.Platform.Get(u => u.Id == id);
            if (platformToBeDeleted == null)
            {
                return Json(new { success = false, message = "Erreur lors de la suppression" });
            }

            if (!string.IsNullOrEmpty(platformToBeDeleted.LogoUrl))
            {
                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, platformToBeDeleted.LogoUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _unitOfWork.Platform.Remove(platformToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Suppression réussie" });
        }

        #endregion
    }
}