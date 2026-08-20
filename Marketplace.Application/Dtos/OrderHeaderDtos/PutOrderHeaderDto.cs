using InvoiceApp.ApplicationServices.Dtos.OrderDetailDtos;

namespace InvoiceApp.ApplicationServices.Dtos.OrderHeaderDtos
{
    public class PutOrderHeaderDto
    {
        public Guid GuidKey { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShipCity { get; set; }
        public string ShipAddress { get; set; }
        public List<PutOrderDetailDto> PutOrderDetailDtos { get; set; }
    }
}
