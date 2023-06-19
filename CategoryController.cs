using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce_new.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ApplicationDbContext _db;

		public CategoryController(ApplicationDbContext db)
		{
			_db = db;
		}
		[HttpGet]
		public IActionResult Index()
		{
			List<Category> categories1 = _db.Categories.ToList();
			List<Category> categories = categories1;
			return View(categories);
		}
		[HttpGet]
		public IActionResult AddCategory()
		{
			return View();
		}			
		[HttpPost]
		public IActionResult AddCategory(Category category)
		{
			if(ModelState.IsValid)
			{
				category.createdAt =System.DateTime.Now.ToString();
				category.updatedAt = System.DateTime.Now.ToString();
				_db.Categories.Add(category);
				_db.SaveChanges();
				TempData["success"] = "Category Added Successfully";

				return RedirectToAction("Index");
			}
			return View();
		}
		public IActionResult UpdateCategory(int id)
		{
			Category category = _db.Categories.Find(id);
			return View(category);
		}
		[HttpPost]
		public IActionResult UpdateCategory(Category category)
		{
			if (ModelState.IsValid)
			{
				category.updatedAt = System.DateTime.Now.ToString();
				_db.Update(category);
				_db.SaveChanges();
				TempData["success"] = "Category Updated Successfully";

				return RedirectToAction("Index");

			}
			else
			{
				return View();
			}
		}
		public IActionResult DeleteCategory(int id)
		{
			Category category = _db.Categories.Find(id);
			return View(category);
		}
		[HttpPost]
		public IActionResult Delete(int id)
		{
			Category category = _db.Categories.Find(id);
			if (category == null)
			{
				return NotFound();
			}
			_db.Categories.Remove(category);
			_db.SaveChanges();
			TempData["success"] = "Category Deleted Successfully";
			return RedirectToAction("Index");	

		}

	}
}
