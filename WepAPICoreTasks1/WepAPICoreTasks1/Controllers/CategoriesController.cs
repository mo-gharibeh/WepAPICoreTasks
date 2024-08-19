using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WepAPICoreTasks1.Models;

namespace WepAPICoreTasks1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly MyDbContext _db;

        public CategoriesController(MyDbContext db)
        {
            _db = db;   
        }
        [HttpGet]
        public IActionResult getAllCategories()
        {
            var categories = _db.Categories.ToList();
            return Ok(categories);
        }

        [HttpGet("id")]
        public IActionResult GetCategoryById(int id) 
        {
            var categories = _db.Categories.Where(c => c.CategoryId == id).ToList();
            return Ok(categories);
        }
    }
}
