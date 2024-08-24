using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


    }
}
