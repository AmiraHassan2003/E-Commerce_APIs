namespace E_Commerce_APIs.DTO
{
    public class GetProductsDto
    {
        public GetProductsDto()
        {
            
        }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public int QuantitySold { get; set; }
        public int ProductCategoryId { get; set; }
        //public string CategoryName { get; set; }
    }
}
