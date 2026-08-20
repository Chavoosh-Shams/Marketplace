using InvoiceApp.ApplicationServices.Dtos.OrderDetailDtos;
using System.Text.Json.Serialization;

namespace InvoiceApp.ApplicationServices.Dtos.OrderHeaderDtos
{
    public class PostOrderHeaderDto
    {
        [JsonIgnore]
        public Guid GuidKey { get; private set; }
        public PostOrderHeaderDto()
        {
            GuidKey = Guid.NewGuid();
        }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShipCity { get; set; }
        public string ShipAddress { get; set; }
        public List<PostOrderDetailDto> PostOrderDetailDtos { get; set; }
    }
}
