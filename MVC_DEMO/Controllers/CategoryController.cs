using Microsoft.AspNetCore.Mvc;
using MVC_DEMO.Data;
using MVC_DEMO.Models;

namespace MVC_DEMO.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> catList = _db.Categories.ToList();
            return View(catList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString()) {
                ModelState.AddModelError("Name", "The DisplayOrdder and Name cannot be excatly same");
            }
            if (obj.Name == "test")
            {
                ModelState.AddModelError("", "The Name cannot be test");
            }
            if (ModelState.IsValid) {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Record Added To Database Successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
            
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) { 
                return NotFound();
            }
            Category? categoryFromDB = _db.Categories.Find(id);
            //Category? categoryFromDB1 = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //Category? categoryFromDB2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (categoryFromDB == null) {
                return NotFound();
            }

            return View(categoryFromDB);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrdder and Name cannot be excatly same");
            }
            if (obj.Name == "test")
            {
                ModelState.AddModelError("", "The Name cannot be test");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Record Update In the Database Successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDB = _db.Categories.Find(id);
            //Category? categoryFromDB1 = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //Category? categoryFromDB2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (categoryFromDB == null)
            {
                return NotFound();
            }

            return View(categoryFromDB);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Record Removed From The Database Successfully";
            return RedirectToAction("Index", "Category");
        }
    }
}
