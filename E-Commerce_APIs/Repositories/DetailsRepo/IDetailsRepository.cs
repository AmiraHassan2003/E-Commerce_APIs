namespace E_Commerce_APIs.Repositories.DetailsRepo
{
    public interface IDetailsRepository
    {
        public string GetProductName(int productId);
        public string GetProductDescription(int productId);
        public double GetProductPrice(int productId);
        public int GetStockQuantity(int productId);
        public int GetQuantitySold(int productId);
        public string GetCategoryName(int productId);
        public string ShowImage(int productId);
        //public int GetProductCategoryCount(int productId);
        
    }
}
