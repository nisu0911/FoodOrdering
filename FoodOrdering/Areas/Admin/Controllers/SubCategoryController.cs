using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodOrdering.Data;
using FoodOrdering.Models;
using FoodOrdering.Models.ViewModel;
using FoodOrdering.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.ManagerUser)]
    public class SubCategoryController : Controller
    {
        private readonly ApplicationDbContext db;
        [TempData]
        public string StatusMessage { get; set; }

        public SubCategoryController(ApplicationDbContext _db)
        {
            db = _db;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.subCategory.Include(sc=>sc.Category).ToListAsync());
        }
        public async Task<IActionResult> Create()
        {
            SubCategoryAndCategoryViewModel model = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = await db.category.ToListAsync(),
                SubCategory = new Models.SubCategory(),
                SubCategoryList = await db.subCategory.OrderBy(sc => sc.Name).Select(sc => sc.Name).Distinct().ToListAsync()
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubCategoryAndCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var categoryCheck = db.subCategory.Include(sc => sc.Category).Where(sc => sc.Name == model.SubCategory.Name && sc.CategoryId == model.SubCategory.CategoryId);
                if (categoryCheck.Count() > 0)
                {
                    StatusMessage = "Error: Sub Category exists under " + categoryCheck.First().Category.Name + " category";
                }
                else
                {
                    db.subCategory.Add(model.SubCategory);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            SubCategoryAndCategoryViewModel vm = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = await db.category.ToListAsync(),
                SubCategory = new Models.SubCategory(),
                SubCategoryList = await db.subCategory.OrderBy(sc => sc.Name).Select(sc => sc.Name).Distinct().ToListAsync(),
                StatusMessage = StatusMessage
            };
            return View(vm);
        }
        
        public async Task<IActionResult> GetSubCategory(int id)
        {
            List<SubCategory> subCategories = new List<SubCategory>();

            subCategories = await db.subCategory.Where(sc => sc.CategoryId == id).ToListAsync();

            return Json(new SelectList(subCategories, "Id", "Name"));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var subCategory = await db.subCategory.SingleOrDefaultAsync(sc=>sc.Id==id);
            if (subCategory == null)
            {
                return NotFound();
            }
            SubCategoryAndCategoryViewModel model = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = await db.category.ToListAsync(),
                SubCategory = subCategory,
                SubCategoryList = await db.subCategory.OrderBy(sc => sc.Name).Select(sc => sc.Name).Distinct().ToListAsync()
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SubCategoryAndCategoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var categoryCheck = db.subCategory.Include(sc => sc.Category).Where(sc => sc.Name == vm.SubCategory.Name && sc.CategoryId == vm.SubCategory.CategoryId);
                if (categoryCheck.Count() > 0)
                {
                    StatusMessage = "Error: Sub Category exists under " + categoryCheck.First().Category.Name + " category";
                }
                else
                {
                    var subCategoryDb = await db.subCategory.FindAsync(vm.SubCategory.Id);
                    subCategoryDb.Name = vm.SubCategory.Name;
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            SubCategoryAndCategoryViewModel model = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = await db.category.ToListAsync(),
                SubCategory = vm.SubCategory,
                SubCategoryList = await db.subCategory.OrderBy(sc => sc.Name).Select(sc => sc.Name).Distinct().ToListAsync(),
                StatusMessage = StatusMessage
            };
            return View(model);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var subCategory = await db.subCategory.Include(sc => sc.Category).FirstOrDefaultAsync(sc => sc.Id == id);
            if (subCategory == null){
                return NotFound();
            }
            return View(subCategory);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var subCategoryDb = await db.subCategory.FindAsync(id);
            db.subCategory.Remove(subCategoryDb);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var subCategory = await db.subCategory.Include(sc => sc.Category).FirstOrDefaultAsync(sc => sc.Id == id);
            if (subCategory == null)
            {
                return NotFound();
            }
            return View(subCategory);
        }
    }
}
