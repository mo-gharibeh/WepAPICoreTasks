namespace WepAPICoreTasks1.DTOs
{
    public class ProductRequestDTO
    {
        public int? CategoryId { get; set; }

        public string? ProductName { get; set; }

        public string? Description { get; set; }

        public IFormFile? ProductImage { get; set; }

        public int? Price { get; set; }
    }
}
