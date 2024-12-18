using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using bulkyBookWeb.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAccess.Repository;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bulky.Models.ViewModels;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Hosting;

namespace bulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IWebHostEnvironment _IWebhostEnvironment;
        public ProductController(IUnitOfWork UnitOfWork, IWebHostEnvironment iWebhostEnvironment)
        {
            _UnitOfWork = UnitOfWork;
            _IWebhostEnvironment = iWebhostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> ProductList = _UnitOfWork.Product.GetAll().ToList();
            return View(ProductList);
        }
        public IActionResult Upsert(int? id)
        {
            //Below mentioned code is commented because we are using ViewModel to pass the data to view insted of viewbag or viewdata and temdata.
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
            if(id ==null || id == 0)
            {
                //This will be true for Insert or Create    
                return View(productVM);
            }
            else
            {
                //This will be true for update
                productVM.Product = _UnitOfWork.Product.Get(u => u.Id == id);
                return View (productVM);
            }
            
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
              String wwwRootPath = _IWebhostEnvironment.WebRootPath;
                if (file!=null) { 
                string fileName = Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"Images/Product");
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageURl =@"\Images\Product" + fileName;
                        }
                _UnitOfWork.Product.Add(productVM.Product);
                _UnitOfWork.Save();
                TempData["Success"] = "Product Added Successfully"; 
                return RedirectToAction("Index", "Product");
            }
            else
            {
                productVM.CategoryList = _UnitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
            }
            
                return View(productVM);
            

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
