using Games.DataAccess.Data;
using Games.Models;
//using GamesWeb.Data;
//using GamesWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamesWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _context.Categories.ToListAsync();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (await _context.Categories.AnyAsync(c => c.Name == category.Name))
            {
                ModelState.AddModelError("name", "Le nom de la catégorie existe déjà dans la base de données");
            }
            if (await _context.Categories.AnyAsync(c => c.DisplayOrder == category.DisplayOrder))
            {
                ModelState.AddModelError("displayOrder", "L'ordre d'affichage existe déjà dans la base de données");
            }
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                TempData["success"] = "La catégorie a été ajoutée avec succès";
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            Category? category = await _context.Categories.Where(c =>c.Id == id).FirstOrDefaultAsync();
            if (category == null || id == null || id == 0)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (await _context.Categories.Where(c => c.Id != category.Id).AnyAsync(c => c.Name == category.Name))
            {
                ModelState.AddModelError("name", "Le nom de la catégorie existe déjà dans la base de données");
            }
            if (await _context.Categories.Where(c => c.Id != category.Id).AnyAsync(c => c.DisplayOrder == category.DisplayOrder))
            {
                ModelState.AddModelError("displayOrder", "L'ordre d'affichage existe déjà dans la base de données");
            }
            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
                TempData["success"] = "La catégorie a été modifiée avec succès";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            Category? category = await _context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (category == null || id == null || id==0)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            Category? category = await _context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (category == null || id == null || id == 0) 
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            TempData["success"] = "La catégorie a été supprimer avec succès";
            return RedirectToAction("Index");
        }
    }
}
