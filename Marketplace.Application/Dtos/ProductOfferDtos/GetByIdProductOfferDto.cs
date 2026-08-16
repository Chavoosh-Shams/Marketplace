namespace Marketplace.Application.Dtos.ProductOfferDtos
{
    public class GetByIdProductOfferDto
    {
        public Guid GuidKey { get; set; }
        public Guid ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductDescriptionRecord { get; set; }
        public string ProductCategoryTitle { get; set; }
        public Guid SellerId { get; set; }
        public string SellerFirstName { get; set; }
        public string SellerLastName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Stock { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
