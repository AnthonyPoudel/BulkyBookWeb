using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using bulkyBookWeb.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAccess.Repository;
namespace bulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly UnitOfWork _UnitOfWork;
        public CategoryController(UnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> CategoryList = _UnitOfWork.Category.GetAll().ToList();
            return View(CategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Category Name and Display Order cannot be same");
            }
            if (ModelState.IsValid)
            {
                _UnitOfWork.Category.Add(category);
                _UnitOfWork.Save();
                return RedirectToAction("Index", "Category");
            }
            return View();

        }
        public IActionResult Edit(int id)
        {
            Category? category = _UnitOfWork.Category.Get(u => u.Id == id);
            //Below mentioned are two other ways to find the category from database in order to edit or delete.
            //Category category = _DbContext.Categories.FirstOrDefault(u => u.Id == id);
            //Caegory category2 = _DbContext.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (category.Id == null || category.Id == 0)
            {
                return NotFound();
            }
            if (category == null)
            {
                return NotFound();
            }
            return View(category);

        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.Category.update(category);
                _UnitOfWork.Save();
                return RedirectToAction("Index", "Category");
            }
            return View(category);
        }
        public IActionResult Delete(int id)
        {
            return View();
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteCategory(int id)
        {
            Category? category = _UnitOfWork.Category.Get(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _UnitOfWork.Category.Remove(category);
            _UnitOfWork.Save();
            return RedirectToAction("Index", "Category");

        }

    }

}
