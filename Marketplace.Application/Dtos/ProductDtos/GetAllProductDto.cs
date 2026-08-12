namespace InvoiceApp.ApplicationServices.Dtos.ProductDtos
{
    public class GetAllProductDto
    {
        public Guid GuidKey { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public string Title { get; set; }
        public string DescriptionRecord { get; set; }
    }
}
