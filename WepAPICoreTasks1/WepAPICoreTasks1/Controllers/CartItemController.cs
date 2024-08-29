using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WepAPICoreTasks1.DTOs;
using WepAPICoreTasks1.Models;

namespace WepAPICoreTasks1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private MyDbContext _db;
        public CartItemController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet("GetAllItemWithoutSelect")]
        public ActionResult GetData()
        {
            var data = _db.CartItems.ToList();
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllData()
        {
            var getData = _db.CartItems.Select(x =>
            new CartItemResponseDTO
            {
                CartId = x.CartId,
                CartItemId = x.CartItemId,  
                Quantity = x.Quantity,  
                Product = new ProducttDTO
                {
                    ProductId = x.Product.ProductId,
                    Price = x.Product.Price,
                    Description = x.Product.Description,
                    ProductName = x.Product.ProductName,

                }
            }
            ).ToList();
            return Ok(getData);    
        }

        // Add
        [HttpPost]
        public IActionResult AddNewItem([FromBody] AddCartItemRequestDTO cartItemDTO)
        {              
                var NewCartItem = new CartItem
                {
                    CartId = cartItemDTO.CartId,
                    Quantity = cartItemDTO.Quantity,
                    ProductId = cartItemDTO.ProductId,
                };

                _db.CartItems.Add(NewCartItem);
                _db.SaveChanges();

                return Ok(NewCartItem);
        }

        [HttpDelete]
        [Route("DeleteItem/{id}")]
        public IActionResult deleteItem(int id)
        {

            var deleteItem = _db.CartItems.FirstOrDefault(i => i.CartItemId == id);
            if (deleteItem != null)
            {
                _db.CartItems.Remove(deleteItem);
                _db.SaveChanges();
                return Ok($"Order '{id}' deleted successfully.");
            }
            return NotFound($"Order '{id}' not found.");
        }

        [HttpPut]
        [Route("UpdateItem/{id}")]
        public IActionResult addItem(int id, [FromBody] CartItemRequestDTO cartItemDTO)
        {

            var item = _db.CartItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            item.Quantity = cartItemDTO.Quantity;
            _db.CartItems.Update(item);
            _db.SaveChanges();
            return NoContent();

        }

    }
}
