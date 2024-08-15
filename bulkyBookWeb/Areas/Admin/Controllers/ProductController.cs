using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using bulkyBookWeb.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAccess.Repository;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bulky.Models.ViewModels;

namespace bulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public ProductController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> ProductList = _UnitOfWork.Product.GetAll().ToList();
            return View(ProductList);
        }
        public IActionResult Create()
        {
            //Below mentioned code is commited because we are using ViewModel to pass the data to view insted of viewbag or viewdata and temdata.
            //IEnumerable<SelectListItem> CategoryList = _UnitOfWork.Category.GetAll()
            //    .Select(i => new SelectListItem
            //    {
            //        Text = i.Name,
            //        Value = i.Id.ToString()
            //    });
            //ViewBag.CategoryList = CategoryList;
            ProductVM productVM = new ProductVM()
            {
                CategoryList = _UnitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                Product = new Product()
            };
            return View(productVM);
        }
        [HttpPost]
        public IActionResult Create(ProductVM product)
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.Product.Add(product.Product);
                _UnitOfWork.Save();
                return RedirectToAction("Index", "Product");
            }
            //else
            //{

            //    product.CategoryList = _UnitOfWork.Category.GetAll().Select(i => new SelectListItem
            //        {
            //            Text = i.Name,
            //            Value = i.Id.ToString()

            //    });
            //    return View(product);
            //}
            
                return View();
            

        }
        public IActionResult Edit(int id)
        {
            Product? product = _UnitOfWork.Product.Get(u => u.Id == id);
            //Below mentioned are two other ways to find the product from database in order to edit or delete.
            //Product product = _DbContext.Categories.FirstOrDefault(u => u.Id == id);
            //Caegory product2 = _DbContext.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (product.Id == null || product.Id == 0)
            {
                return NotFound();
            }
            if (product == null)
            {
                return NotFound();
            }
            return View(product);

        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.Product.update(product);
                _UnitOfWork.Save();
                return RedirectToAction("Index", "Product");
            }
            return View(product);
        }
        public IActionResult Delete(int id)
        {
            return View();
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteProduct(int id)
        {
            Product? product = _UnitOfWork.Product.Get(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _UnitOfWork.Product.Remove(product);
            _UnitOfWork.Save();
            return RedirectToAction("Index", "Product");

        }

    }

}
