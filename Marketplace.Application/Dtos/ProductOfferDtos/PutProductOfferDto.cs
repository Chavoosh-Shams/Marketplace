namespace Marketplace.Application.Dtos.ProductOfferDtos
{
    public class PutProductOfferDto
    {
        public Guid GuidKey { get; set; }
        public Guid ProductId { get; set; }
        public Guid SellerId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Stock { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
