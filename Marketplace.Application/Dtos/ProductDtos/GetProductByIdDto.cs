namespace InvoiceApp.ApplicationServices.Dtos.ProductDtos
{
    public class GetProductByIdDto
    {
        public Guid GuidKey { get; set; }
        public string Title { get; set; }
        public string DescriptionRecord { get; set; }
    }
}
