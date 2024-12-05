using Games.DataAccess.Data;
using Games.DataAccess.Repository.IRepository;
using Games.Models;
//using GamesWeb.Data;
//using GamesWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamesWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> Index()
        {
            List<Category> categories = _categoryRepository.GetAll().ToList();
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
                _categoryRepository.Add(category);
                _categoryRepository.Save();
                TempData["success"] = "La catégorie a été ajoutée avec succès";
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            Category? category = _categoryRepository.Get(u =>u.Id == id);
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
                _categoryRepository.Update(category);
                _categoryRepository.Save();
                TempData["success"] = "La catégorie a été modifiée avec succès";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            Category? category = _categoryRepository.Get(u => u.Id == id);
            if (category == null || id == null || id==0)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            Category? category = _categoryRepository.Get(u => u.Id == id);
            if (category == null || id == null || id == 0) 
            {
                return NotFound();
            }
            _categoryRepository.Remove(category);
            _categoryRepository.Save();
            TempData["success"] = "La catégorie a été supprimer avec succès";
            return RedirectToAction("Index");
        }
    }
}
