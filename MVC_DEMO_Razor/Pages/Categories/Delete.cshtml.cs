using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC_DEMO_Razor.Data;
using MVC_DEMO_Razor.Models;

namespace MVC_DEMO_Razor.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category Category { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int? id)
        {
            if (id != null || id != 0)
            {
                Category = _db.Categories.Find(id);
            }
        }
        public IActionResult OnPost()
        {
            _db.Categories.Remove(Category);
            _db.SaveChanges();
            return RedirectToPage("Index");
            
        }
    }
}
