using BulkyWeb.Data;
using BulkyWeb.Models.DomainModel;
using BulkyWeb.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDBContext _appDBContext;
        List<Category> _categoriesList;

        public CategoryController(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            this._categoriesList = await _appDBContext.Categories.ToListAsync();
            return View(this._categoriesList);
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryRequest addCategory)
        {
            /**All this is server side validation**/
            if (addCategory.CategoryName == addCategory.CategoryOrder.ToString())
            {
                //**I think in Dot Net 8 or higher, we do not need "nameof", instead we can use string.
                ModelState.AddModelError(nameof(addCategory.CategoryName), "Display Order cannot be same as Category Name");
                return View();
            }
            if (ModelState.IsValid)
            {
                Category category = new Category()
                {
                    CategoryName = addCategory.CategoryName,
                    CategoryOrder = addCategory.CategoryOrder
                };
                _appDBContext.Categories.Add(category);
                await _appDBContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(Guid id)
        {
            Category? category = await _appDBContext.Categories.FindAsync(id);//It works on Primary Key Only
            //Category? category2 = _appDBContext.Categories.FirstOrDefault(u => u.CategoryName == "CategoryName");//It works on any value
            //Category? category3 = _appDBContext.Categories.Where(u => u.CategoryName == "CategoryName").FirstOrDefault();
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> EditCategory(Category updateCategoryRequest)
        {
            //**We donot need to do this...the action will automatically finds the selected data**// 
            //Category? category = await _appDBContext.Categories.FindAsync(id);
            //if (category == null)
            //{
            //    return NotFound();
            //}
            //category.CategoryName = updateCategoryRequest.CategoryName;
            //category.CategoryOrder = updateCategoryRequest.CategoryOrder;
            if (updateCategoryRequest.CategoryName == updateCategoryRequest.CategoryOrder.ToString())
            {
                //**I think in Dot Net 8 or higher, we do not need "nameof", instead we can use string.
                ModelState.AddModelError(nameof(updateCategoryRequest.CategoryName), "Display Order cannot be same as Category Name");
                return View();
            }
            if (ModelState.IsValid)
            {
                _appDBContext.Update(updateCategoryRequest);
                await _appDBContext.SaveChangesAsync(); return RedirectToAction("Index");
            }
            return View();

        }
        /**
        Here, We cannot have same action method with same parenthesis.
        So, we cannot make [HttpGet]DeleteCategory & [HttpPost]DeleteCategory.
        Now, what we need to do is, We need to Give an ActionName Explicity(even though it has its own name) to our post method.
        Because the View will look for [HttpGet]DeleteCategory
        **/
        [HttpGet]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            Category? category = await _appDBContext.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("DeleteCategory")]
        public async Task<IActionResult> DeleteTest(Guid id)
        {
            Category? category = await _appDBContext.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            _appDBContext.Categories.Remove(category);
            await _appDBContext.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}
