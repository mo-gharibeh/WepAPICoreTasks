using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WepAPICoreTask2.DTOs;
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

        // Add

        [HttpPost]
        public IActionResult CreateProduct([FromForm] ProductRequestDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (productDTO.ProductImage != null)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }
                var filePath = Path.Combine(uploadsFolderPath, productDTO.ProductImage.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    productDTO.ProductImage.CopyToAsync(stream);
                }
                var x = new Product
                {
                    CategoryId = productDTO.CategoryId,
                    ProductName = productDTO.ProductName,
                    Description = productDTO.Description,
                    ProductImage = productDTO.ProductImage.FileName,
                    Price = productDTO.Price
                };
                _db.Products.Add(x);
                _db.SaveChanges();
                return Ok();

            }
            return Ok();

        }
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromForm] ProductRequestDTO productDto)
        {


            var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }
            var filePath = Path.Combine(uploadsFolderPath, productDto.ProductImage.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                productDto.ProductImage.CopyToAsync(stream);
            }

            var x = _db.Products.Find(id);
            if (x == null)
            {
                return NotFound();
            }

            x.ProductName = productDto.ProductName ?? x.ProductName;
            x.Description = productDto.Description ?? x.Description;
            if (productDto.ProductImage != null)
            {
                x.ProductImage = productDto.ProductImage.FileName;
            }
            x.Price = productDto.Price;

            _db.Products.Update(x);
            _db.SaveChanges();
            return NoContent();
        }

    }
}
