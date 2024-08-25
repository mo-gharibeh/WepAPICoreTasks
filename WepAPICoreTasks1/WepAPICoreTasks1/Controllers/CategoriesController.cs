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
    }
}
