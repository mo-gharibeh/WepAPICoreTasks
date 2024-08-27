using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WepAPICoreTasks1.Models;

namespace WepAPICoreTasks1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly MyDbContext _db;

        public OrdersController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet("AllOrders")]
        public IActionResult GetAllOrders()
        {

            var categories = _db.Orders.ToList();

            return Ok(categories);
        }

        [HttpGet("Order/{id:min(5)}")]

        public IActionResult GetgetAllOrderById(int id)
        {

            if (id < 0)
            {
                return BadRequest($"Invalid input: {id}");
            }

            var products = _db.Orders.Where(model => model.OrderId == id);

            if (products == null)
            {
                return NotFound($"Order '{id}' not found.");
            }

            return Ok(products);
        }



        [HttpDelete("Order/{id}")]
        public IActionResult DeleteOrderById(int id)
        {

            var delOrder = _db.Orders.FirstOrDefault(e => e.OrderId == id);

            if (delOrder != null)
            {
                _db.Orders.Remove(delOrder);
                _db.SaveChanges();
                return Ok($"Order '{id}' deleted successfully.");
            }

            return NotFound($"Order '{id}' not found.");
        }

    }

}
