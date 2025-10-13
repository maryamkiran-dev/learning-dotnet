namespace DTO.Model
{
    public class CreateProductDTO
    { 
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock {  get; set; }  
        public decimal Price { get; set; }

    }

    public class ProductResponseDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
