namespace InvoiceApp.ApplicationServices.Dtos.OrderDetailDtos
{
    public class PutOrderDetailDto
    {
        public Guid ProductOfferId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
