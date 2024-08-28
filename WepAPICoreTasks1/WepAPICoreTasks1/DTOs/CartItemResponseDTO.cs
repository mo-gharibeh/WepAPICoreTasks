using WepAPICoreTasks1.Models;

namespace WepAPICoreTasks1.DTOs
{
    public class CartItemResponseDTO
    {
        public int CartItemId { get; set; }

        public int? CartId { get; set; }


        public int Quantity { get; set; }

        public ProducttDTO Product { get; set; }

    }
    public class ProducttDTO
    {
        public int? ProductId { get; set; }

        public string? ProductName { get; set; }

        public string? Description { get; set; }

        public int? Price { get; set; }
    }
}
