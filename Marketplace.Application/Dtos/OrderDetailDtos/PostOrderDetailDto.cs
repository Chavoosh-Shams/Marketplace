using System.Text.Json.Serialization;

namespace InvoiceApp.ApplicationServices.Dtos.OrderDetailDtos
{
    public class PostOrderDetailDto
    {
        [JsonIgnore]
        public Guid GuidKey { get; private set; }
        public PostOrderDetailDto()
        {
            GuidKey = Guid.NewGuid();
        }
        public Guid ProductOfferId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
