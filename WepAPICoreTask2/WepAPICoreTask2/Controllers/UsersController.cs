using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WepAPICoreTask2.Models;

namespace WepAPICoreTask2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _db;

        public UsersController(MyDbContext db)
        {
            _db = db;
        }

        [Route("AllNames")]
        [HttpGet]
        public IActionResult getAllNames()
        {

            var categories = _db.Users.ToList();

            return Ok(categories);
        }


        //////////////////////////////////////////////////////////////////////////////


        [Route("name/{id:min(5)}")]
        [HttpGet]

        public IActionResult GetNameById(int id)
        {
            if (id < 0)
            {

                return BadRequest($"Invalid input: {id}");
            }

            var categories = _db.Users.Where(model => model.UserId == id);

            if (categories == null)
            {
                return NotFound($"Category '{id}' not found.");
            }

            return Ok(categories);
        }



        ////////////////////////////////////////////////////////////////////////////////////


        [Route("{name}")]
        [HttpGet]

        public IActionResult GetCategoryByName(string name)
        {
            if (name == null)
            {
                return BadRequest($"Invalid input: {name}");
            }

            var categories = _db.Users.Where(model => model.Username == name);

            if (categories == null)
            {
                return NotFound($"Name '{name}' not found.");
            }

            return Ok(categories);
        }



        ///////////////////////////////////////////////////////////////////////////////////////////


        [Route("deletename/{name}")]
        [HttpDelete]
        public IActionResult DeleteCategoryByName(string name)
        {

            var delName = _db.Users.FirstOrDefault(e => e.Username == name);

            if (delName != null)
            {
                _db.Users.Remove(delName);
                _db.SaveChanges();
                return Ok($"Name '{name}' deleted successfully.");
            }
            return NotFound($"Name '{name}' not found.");
        }
    }
}
