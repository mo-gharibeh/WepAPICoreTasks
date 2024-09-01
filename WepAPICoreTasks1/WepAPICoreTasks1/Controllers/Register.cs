using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WepAPICoreTasks1.DTOs;
using WepAPICoreTasks1.Models;

namespace WepAPICoreTasks1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Register : ControllerBase
    {
        private readonly MyDbContext _db;

        public Register(MyDbContext db)
        {
            _db = db;
        }

        [HttpPost("register")]
        public IActionResult addUser([FromForm] UserRequestDTO userDTO)
        {
            byte[] passwordHash;
            byte[] salt;

            PasswordHasherNew.createPasswordHash(userDTO.Password, out passwordHash, out salt);

            var user = new User
            {
                Email = userDTO.Email,
                Password = userDTO.Password,
                PasswordHash = passwordHash,
                PasswordSalt = salt,
                Username = userDTO.Username
            };


            _db.Users.Add(user);
            _db.SaveChanges();
            return Ok(user);
        }
        [HttpPost("login")]
        public IActionResult Login([FromForm] UserRequestDTO model)
        {
            var user = _db.Users.FirstOrDefault(x => x.Email == model.Email && x.Username == model.Username);
            if (user == null || !PasswordHasherNew.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized("Invalid username or password.");
            }
            return Ok("User logged in successfully");
        }
    }
}
