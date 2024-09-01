using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WepAPICoreTasks1.DTOs;
using WepAPICoreTasks1.Models;

namespace WepAPICoreTasks1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _db;
        private readonly ILogger<WeatherForecastController> _logger;

        public ProductsController(MyDbContext db, ILogger<WeatherForecastController> logger)
        {
            _db = db;
            _logger = logger;
        }


        [HttpGet("sortByName")]
        public IActionResult Get()
        {
            var s = _db.Products.OrderByDescending(p => p.ProductName).Take(5);
            var products = _db.Products.OrderBy(s => s.ProductName).Reverse().Take(5).Reverse().ToList();
            var x = _db.Products.OrderBy(x => x.ProductName).ToList().TakeLast(5);


            return Ok(x);
        }


        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult getAllProducts()
        {
            var products = _db.Products.ToList();

            _logger.LogInformation("All Product");
            _logger.LogInformation("you excuted the get product api ");
            _logger.LogInformation($"the cont of this products are {products.Count}");
            _logger.LogInformation("the cont of this products are {}", products.Count);
            return Ok(products);
        }



        ///// this
        [Route("category/{id}")]
        [HttpGet]
        public IActionResult GetProductById(int id)
        {
            

            var products = _db.Products.Where(c => c.CategoryId == id).ToList();
            
            return Ok(products);
        }
        [Route("product/{id}")]
        [HttpGet]
        public IActionResult ProductById(int id)
        {
            var products = _db.Products.Where(c => c.ProductId == id).FirstOrDefault();

            return Ok(products);
        }
        [HttpGet("Product/{id:min(5)}")]
        [ProducesResponseType(404)]

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

        [HttpGet("Math")]
        public IActionResult Calculater(string input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string[] x = input.Split(' ');

            var num1 = Convert.ToDouble(x[0]);
            var op = x[1];
            var num2 = Convert.ToDouble(x[2]);
            if (op == "+")
            {
                var sum = (num1 + num2);
                return Ok(sum);
            }
            else if (op == "-")
            {
                var min = (num1 - num2);
                return Ok(min);
            }
            else
            {
                return Ok();
            }

        }
    }
}
