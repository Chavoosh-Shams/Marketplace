namespace InvoiceApp.ApplicationServices.Dtos.ProductDtos
{
    public class PostProductDto
    {
        public Guid GuidKey { get; private set; }
        public string Title { get; set; }
        public string DescriptionRecord { get; set; }
    }
}
