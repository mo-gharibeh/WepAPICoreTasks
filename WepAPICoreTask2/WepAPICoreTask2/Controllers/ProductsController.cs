using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WepAPICoreTask2.Models;

namespace WepAPICoreTask2.Controllers
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

        [HttpGet("AllProducts")]
        public IActionResult GetAllProducts()
        {

            var categories = _db.Products.ToList();

            return Ok(categories);
        }

        [HttpGet("Product/{id:min(5)}")]

        public IActionResult GetgetAllProductById(int id)
        {

            if (id < 0)
            {
                return BadRequest($"Invalid input: {id}");
            }

            var products = _db.Products.Where(model => model.ProductId == id);

            if (products == null)
            {
                return NotFound($"Product '{id}' not found.");
            }

            return Ok(products);
        }

        [HttpGet("Product/{name}")]

        public IActionResult GetProductByName(string name)
        {

            if (name == null)
            {
                return BadRequest($"Invalid input: {name}");
            }

            var products = _db.Products.Where(model => model.ProductName == name);

            if (products == null)
            {
                return NotFound($"Product '{name}' not found.");
            }

            return Ok(products);
        }

        [HttpDelete("Product/{name}")]
        public IActionResult DeleteCategoryByName(string name)
        {

            var delProduct = _db.Products.FirstOrDefault(e => e.ProductName == name);

            if (delProduct != null)
            {
                _db.Products.Remove(delProduct);
                _db.SaveChanges();
                return Ok($"Category '{name}' deleted successfully.");
            }

            return NotFound($"Category '{name}' not found.");
        }
    }
}
