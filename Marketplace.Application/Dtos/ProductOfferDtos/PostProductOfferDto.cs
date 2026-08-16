using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Application.Dtos.ProductOfferDtos
{
    public class PostProductOfferDto
    {
        [JsonIgnore]
        public Guid GuidKey { get; private set; }
        public PostProductOfferDto()
        {
            GuidKey = Guid.NewGuid();
        }
        public Guid ProductId { get; set; }
        public Guid SellerId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Stock { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
