namespace E_Commerce_APIs.DTO
{
    public class GetProductInfoInCarDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int Quantity { get; set; }
    }
}
