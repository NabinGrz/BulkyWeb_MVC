
using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAcess.Data;
using Bulky.Models.DomainModel;
using Bulky.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        /**
         *Here, we are using application DBContext Directly*
        private readonly AppDBContext _appDBContext;
        public CategoryController(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }
        **/

        /**
         Now, we are using Repository
         **/
        private readonly ICategoryRepository _categoryRepo;
        List<Category> _categoriesList;

        public CategoryController(ICategoryRepository appDBContext)
        {
            this._categoryRepo = appDBContext;
        }

        //------------------------ALL THE ACTIONS---------------------------------------
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //**Using DB Context Directly  this._categoriesList = await _categoryRepo.Categories.ToListAsync();
            this._categoriesList = _categoryRepo.GetAll().ToList();
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
                Category? category = new Category()
                {
                    CategoryName = addCategory.CategoryName,
                    CategoryOrder = addCategory.CategoryOrder
                };

                //**Using DB Context Directly  _categoryRepo.Categories.Add(category);
                //**Using DB Context Directly  await _categoryRepo.SaveChangesAsync();

                /**Using the Category Repository**/
                _categoryRepo.Add(category);
                _categoryRepo.Save();
                TempData["success"] = "Category Created Successfully!!";
                return RedirectToAction("Index");
            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(Guid id)
        {
            //**Using DB Context Directly  Category? category = await _categoryRepo.Categories.FindAsync(id); //It works on Primary Key Only
            //Category? category2 = _appDBContext.Categories.FirstOrDefault(u => u.CategoryName == "CategoryName");//It works on any value
            //Category? category3 = _appDBContext.Categories.Where(u => u.CategoryName == "CategoryName").FirstOrDefault();

            /**Using the Category Repository**/
            Category? category = _categoryRepo.GetT(u => u.ID == id);
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
                /**Using the Category Repository**/
                _categoryRepo.Update(updateCategoryRequest);
                 _categoryRepo.Save();
                TempData["success"] = "Category Updtaed Successfully!!";
                return RedirectToAction("Index");
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
            //**Using DB Context Directly -> Category? category = await _categoryRepo.Categories.FindAsync(id);

            /**Using the Category Repository**/
            Category? category = _categoryRepo.GetT(u => u.ID == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("DeleteCategory")] //Giving ActionName Explicity
        public async Task<IActionResult> DeleteTest(Guid id)
        {
            //**Using DB Context Directly -> Category? category = await _categoryRepo.Categories.FindAsync(id);
            
            /**Using the Category Repository**/
            Category? category = _categoryRepo.GetT(u => u.ID == id);
            if (category == null)
            {
                return NotFound();
            }
            //**Using DB Context Directly ->  _categoryRepo.Categories.Remove(category);
            //**Using DB Context Directly -> await _categoryRepo.SaveChangesAsync();

            /**Using the Category Repository**/
            _categoryRepo.Delete(category);
            _categoryRepo.Save();
            TempData["success"] = "Category Deleted Successfully!!";
            return RedirectToAction("Index");

        }
    }
}
