using BulkyBookRazor.Data;
using BulkyBookRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyBookRazor.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDBContext _db;
        public List<Category> categoryList { get; set; }
        public IndexModel(ApplicationDBContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            categoryList =_db.Categories.ToList();
        }
    }
}
