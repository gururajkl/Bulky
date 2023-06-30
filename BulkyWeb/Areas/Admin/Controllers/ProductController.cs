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

        public ProductController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = unitOfWork.Product.GetAll();
            return View(products);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = unitOfWork.Category.GetAll().Select(u =>
                new SelectListItem { Text = u.Name, Value = u.Id.ToString() }
            );
            ProductViewModel productViewModel = new()
            {
                CategoryList = CategoryList,
                Product = new Product()
            };
            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Product.Add(productViewModel.Product);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var productFromDb = unitOfWork.Product.Get(u => u.Id == id);
            if (productFromDb == null) return NotFound();
            return View(productFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Product.Update(product);
                unitOfWork.Save();
                TempData["success"] = $"Product {product.Title} updated successfully";
                return RedirectToAction("Index");
            }
            return View(product);
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
    }
}
