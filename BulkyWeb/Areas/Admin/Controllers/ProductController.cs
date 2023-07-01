using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = unitOfWork.Product.GetAll(includeProperties: "Category");
            return View(products);
        }

        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> CategoryList = unitOfWork.Category.GetAll().Select(u =>
                new SelectListItem { Text = u.Name, Value = u.Id.ToString() }
            );
            ProductViewModel productViewModel = new()
            {
                CategoryList = CategoryList,
                Product = new Product()
            };

            if (id == 0 || id == null)
            {
                return View(productViewModel);
            }
            else
            {
                productViewModel.Product = unitOfWork.Product.Get(p => p.Id == id);
                return View(productViewModel);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductViewModel productViewModel, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productViewModel.Product.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, productViewModel.Product.ImageUrl.Trim('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productViewModel.Product.ImageUrl = @"images\product\" + fileName;
                }

                if (productViewModel.Product.Id == 0)
                {
                    unitOfWork.Product.Add(productViewModel.Product);
                    TempData["success"] = "Product created successfully";
                }
                else
                {
                    unitOfWork.Product.Update(productViewModel.Product);
                    TempData["success"] = "Product updated successfully";
                }

                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                productViewModel.CategoryList = unitOfWork.Category.GetAll().Select(u =>
                    new SelectListItem() { Text = u.Name, Value = u.Id.ToString() }
                );
                return View(productViewModel);
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var productFromDb = unitOfWork.Product.Get(u => u.Id == id);
            if (productFromDb == null) return NotFound();
            return View(productFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Product product)
        {
            unitOfWork.Product.Remove(product);
            unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }

        #region API CALLS
        public IActionResult GetAll()
        {
            List<Product> productList = unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = productList });
        }
        #endregion
    }
}
