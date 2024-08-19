using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WepAPICoreTasks1.Models;

namespace WepAPICoreTasks1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _db;

        public ProductsController(MyDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult getAllProducts()
        {
            var products = _db.Products.ToList();
            return Ok(products);
        }

        [HttpGet("id")]
        public IActionResult GetProductById(int id)
        {
            var products = _db.Products.Where(c => c.ProductId == id).ToList();
            return Ok(products);
        }
    }
}
