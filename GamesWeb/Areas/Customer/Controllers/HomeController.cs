using Games.DataAccess.Repository.IRepository;
using Games.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace GamesWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(int? categoryId, int? platformId, string? searchName)
        {
            IEnumerable<Game> gameList = _unitOfWork.Game.GetAll(includeProperties: "Category,Platforms");

            if (categoryId != null && categoryId != 0)
            {
                gameList = gameList.Where(g => g.CategoryId == categoryId);
            }

            if (platformId != null && platformId != 0)
            {
                gameList = gameList.Where(g => g.Platforms.Any(p => p.Id == platformId));
            }

            if (!string.IsNullOrEmpty(searchName))
            {
                gameList = gameList.Where(g => g.Title.Contains(searchName, StringComparison.OrdinalIgnoreCase));
            }

            ViewBag.CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            ViewBag.PlatformList = _unitOfWork.Platform.GetAll().Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString()
            });

            return View(gameList);
        }

        [HttpGet]
        public JsonResult SearchTitles(string term)
        {
            var titles = _unitOfWork.Game.GetAll()
                .Where(g => !string.IsNullOrEmpty(term) && g.Title.Contains(term, StringComparison.OrdinalIgnoreCase))
                .Select(g => g.Title)
                .Distinct()
                .Take(8)
                .ToList();

            return Json(titles);
        }

        public IActionResult Details(int id)
        {
            Game game = _unitOfWork.Game.Get(u => u.Id == id, includeProperties: "Category");
            return View(game);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
