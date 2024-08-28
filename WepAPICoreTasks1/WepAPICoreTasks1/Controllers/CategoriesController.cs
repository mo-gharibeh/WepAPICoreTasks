using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WepAPICoreTasks1.DTOs;
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

        [Route("category/AllCategories")]
        [HttpGet]
        public IActionResult AllCategories()
        {
            var categories = _db.Categories.ToList();
            return Ok(categories);
        }

        [Route("category/{id:min(5)}")]
        [HttpGet]

        public IActionResult GetCategoryById1(int id)
        {
            if (id < 0)
            {

                return BadRequest($"Invalid input: {id}");
            }

            var categories = _db.Categories.Where(model => model.CategoryId == id);

            if (categories == null)
            {
                return NotFound($"Category '{id}' not found.");
            }

            return Ok(categories);
        }

        [Route("category/{Name}")]
        [HttpGet]

        public IActionResult GetCategoryByName(string name)
        {
            if (name == null)
            {
                return BadRequest($"Invalid input: {name}");
            }

            var categories = _db.Categories.Where(model => model.CategoryName == name);

            if (categories == null)
            {
                return NotFound($"Category '{name}' not found.");
            }

            return Ok(categories);
        }

        [Route("deletecategory/{id}")]
        [HttpDelete]
        public IActionResult DeleteCategoryByName(int id)
        {
            var delproducts = _db.Products.Where(e => e.CategoryId == id).ToList();
            _db.Products.RemoveRange(delproducts);
            _db.SaveChanges();


            var delList = _db.Categories.Where(e => e.CategoryId == id).FirstOrDefault();
            if (delList != null)
            {
                _db.Categories.Remove(delList);
                _db.SaveChanges();
                return Ok($"Category '{id}' deleted successfully.");
            }
            return NotFound($"Category '{id}' not found.");
        }

        // Add
        [HttpPost]
        public IActionResult AddNewCategory([FromForm] categoryRequestDTO categoryDTO)
        {

            if (categoryDTO.CategoryImage != null)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }
                var filePath = Path.Combine(uploadsFolderPath, categoryDTO.CategoryImage.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    categoryDTO.CategoryImage.CopyToAsync(stream);
                }

                var NewCategory = new Category
                {
                    CategoryName = categoryDTO.CategoryName,
                    CategoryImage = $"{Request.Scheme}://{Request.Host}/Uploads/{categoryDTO.CategoryImage.FileName}"
                };

                _db.Categories.Add(NewCategory);
                _db.SaveChanges();

            return Ok(NewCategory);
            }
            return BadRequest();


        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromForm] categoryRequestDTO CategoryDto)
        {

            var x = _db.Categories.Find(id);


            var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }
            var filePath = Path.Combine(uploadsFolderPath, CategoryDto.CategoryImage.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                CategoryDto.CategoryImage.CopyToAsync(stream);
            }
            x.CategoryName = CategoryDto.CategoryName;
            x.CategoryImage = CategoryDto.CategoryImage.FileName;

            _db.Categories.Update(x);

            _db.SaveChanges();
            return NoContent();


        }



        //[Route("category/{id:int:min(5)}")]
        //[HttpGet]
        //public IActionResult GetCategoryById(int id)
        //{
        //    if (id < 0)
        //    {
        //        return BadRequest($"Invalid input: {id}");
        //    }
        //    var catigory 
        //    return Ok();
        //}

        [HttpGet("CheckTowNumber")]
        public IActionResult towNum(int a, int b)
        {
            if (a == 30 || b == 30 || (a + b) == 30)
            {
                return Ok("True");
            }
            else  
            {
                return Ok("false");
            }
           
        }

        [HttpGet("CheckOneNumber")]
        public IActionResult oneNum(int a)
        {
            if ((a % 3 == 0 || a % 7 == 0) && (a > 0))
            {
                return Ok("True");
            }
            else
            {
                return Ok("False");
            }
        }

        }
}
