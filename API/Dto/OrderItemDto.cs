namespace API.Dto
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public required string ProdcutName { get; set; }
        public required string ImgUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
