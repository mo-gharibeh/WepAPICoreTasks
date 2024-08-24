using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        //{
        //    var cartItems = db.Carts
        //                      .Where(c => c.UserID == id)
        //                      .SelectMany(c => c.CartItems)
        //                      .Include(ci => ci.Product) // جلب تفاصيل المنتج
        //                      .ToList();
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
