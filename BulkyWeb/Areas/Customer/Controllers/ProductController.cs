using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.DomainModel;
using Bulky.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        IEnumerable<SelectListItem> categoryList;
        List<Product> productsList;
        Product getT(Guid id)
        {
            return _unitOfWork.Product.GetT(u => u.ID == id);
        }
        public ProductController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            productsList = _unitOfWork.Product.GetAll().ToList();
            categoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.CategoryName,
                Value = u.ID.ToString()
            });

        }
        public IActionResult Index()
        {

            ProductCategoryRequest productCategoryRequest = new ProductCategoryRequest()
            {
                Product = new Product(),
                CategoryList = categoryList
            };
            return View(productsList);
        }
        public IActionResult AddProduct()
        {
            ProductCategoryRequest productCategoryRequest = new ProductCategoryRequest()
            {
                Product = new Product(),
                CategoryList = categoryList
            };
            return View(productCategoryRequest);
        }
        [HttpPost]
        public IActionResult AddProduct(ProductCategoryRequest productCategoryRequest, Guid? id)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productCategoryRequest.Product);
                _unitOfWork.Save();
            }
            else
            {
                productCategoryRequest.CategoryList = categoryList;
            }

            /**
            Here, returning the controllers view will throw an error, because when returning, the category list is empty.
            To Solve:
            *Adding [ValidateNever] in the Models("ProductCategoryRequest")
            *Or, Passing the View with all the necessary data(Product, CategoryList)
            **/
            /**But returning to same form view is not good,,we can return to the list of all T
            return View(productCategoryRequest);
            **/
            return RedirectToAction("Index");
        }

        public IActionResult DeleteProduct(Guid id)
        {
            Product product = getT(id);
            ProductCategoryRequest productCategoryRequest = new()
            {
                Product = product,
                CategoryList = categoryList
            };
            return View(productCategoryRequest);
        }
        [HttpPost, ActionName("DeleteProduct")]
        public IActionResult Delete(Guid id)
        {
            Product product = getT(id);
            _unitOfWork.Product.Delete(product);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

    }
}
