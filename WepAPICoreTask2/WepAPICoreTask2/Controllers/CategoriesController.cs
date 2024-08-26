using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WepAPICoreTask2.DTOs;
using WepAPICoreTask2.Models;

namespace WepAPICoreTask2.Controllers
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

        [Route("AllCategories")]
        [HttpGet]
        public IActionResult getAllCategories1()
        {

            var categories = _db.Categories.ToList();

            return Ok(categories);
        }


        //////////////////////////////////////////////////////////////////////////////


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



        ////////////////////////////////////////////////////////////////////////////////////


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



        ///////////////////////////////////////////////////////////////////////////////////////////


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

        /////////////////////////////

        // Add
        [HttpPost]
        public IActionResult AddNewCategory([FromForm] categoryRequestDTO categoryDTO)
        {

            if (categoryDTO.CategroyImage != null)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }
                var filePath = Path.Combine(uploadsFolderPath, categoryDTO.CategroyImage.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    categoryDTO.CategroyImage.CopyToAsync(stream);
                }
                var NewCategory = new Category
                {
                    CategoryName = categoryDTO.CategroyName,
                    CategoryImage = categoryDTO.CategroyImage.FileName
                };
                _db.Categories.Add(NewCategory);
                _db.SaveChanges();

            }
            return Ok();



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
            var filePath = Path.Combine(uploadsFolderPath, CategoryDto.CategroyImage.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                CategoryDto.CategroyImage.CopyToAsync(stream);
            }
            x.CategoryName = CategoryDto.CategroyName;
            x.CategoryImage = CategoryDto.CategroyImage.FileName;

            _db.Categories.Update(x);

            _db.SaveChanges();
            return NoContent();


        }

    }
}
