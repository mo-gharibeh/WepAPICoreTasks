using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WepAPICoreTasks1.DTOs;
using WepAPICoreTasks1.Models;

namespace WepAPICoreTasks1.Controllers
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

        ///////////////////////////
        ///

        // Add
        [HttpPost]
        public IActionResult CreateUser([FromForm] UserRequestDTO UserDTO)
        {

            var newUser = new User
            {
                Username = UserDTO.Username,
                Email = UserDTO.Email,
                Password = UserDTO.Password
            };
            _db.Users.Add(newUser);
            _db.SaveChanges();
            return Ok();

        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromForm] UserRequestDTO UserDTO)
        {

            var x = _db.Users.Find(id);
            if (x == null)
            {
                return NotFound();
            }

            x.Username = UserDTO.Username ?? x.Username;
            x.Email = UserDTO.Email ?? x.Username;

            x.Password = UserDTO.Password ?? x.Password;


            _db.Users.Update(x);
            _db.SaveChanges();
            return NoContent();
        }


    }
}
