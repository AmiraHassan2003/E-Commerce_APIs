using E_Commerce_APIs.Models.Entities;

namespace E_Commerce_APIs.DTO
{
    public class GetCategoryWithProductDto
    {
        public GetCategoryWithProductDto()
        {
            
        }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public List<GetProductsDto> products { get; set; }

    }
}
