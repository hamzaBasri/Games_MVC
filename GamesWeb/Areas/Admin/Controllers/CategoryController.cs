using Games.DataAccess.Data;
using Games.DataAccess.Repository.IRepository;
using Games.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamesWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            List<Category> categories = _unitOfWork.Category.GetAll().ToList();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            //if (await _context.Categories.AnyAsync(c => c.Name == category.Name))
            //{
            //    ModelState.AddModelError("name", "Le nom de la catégorie existe déjà dans la base de données");
            //}
            //if (await _categoryRepository.Categories.AnyAsync(c => c.DisplayOrder == category.DisplayOrder))
            //{
            //    ModelState.AddModelError("displayOrder", "L'ordre d'affichage existe déjà dans la base de données");
            //}
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
                TempData["success"] = "La catégorie a été ajoutée avec succès";
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            Category? category = _unitOfWork.Category.Get(u => u.Id == id);
            if (category == null || id == null || id == 0)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            //if (await _context.Categories.Where(c => c.Id != category.Id).AnyAsync(c => c.Name == category.Name))
            //{
            //    ModelState.AddModelError("name", "Le nom de la catégorie existe déjà dans la base de données");
            //}
            //if (await _context.Categories.Where(c => c.Id != category.Id).AnyAsync(c => c.DisplayOrder == category.DisplayOrder))
            //{
            //    ModelState.AddModelError("displayOrder", "L'ordre d'affichage existe déjà dans la base de données");
            //}
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
                TempData["success"] = "La catégorie a été modifiée avec succès";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            Category? category = _unitOfWork.Category.Get(u => u.Id == id);
            if (category == null || id == null || id == 0)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            Category? category = _unitOfWork.Category.Get(u => u.Id == id);
            if (category == null || id == null || id == 0)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            TempData["success"] = "La catégorie a été supprimer avec succès";
            return RedirectToAction("Index");
        }
    }
}
