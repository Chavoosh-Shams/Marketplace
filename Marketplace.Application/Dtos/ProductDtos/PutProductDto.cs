using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.ApplicationServices.Dtos.ProductDtos
{
    public class PutProductDto
    {
        public Guid GuidKey { get; set; }
        [Required(ErrorMessage = "CategoryId is required")]
        public Guid CateoryId { get; set; }
        public string Title { get; set; }
        public string DescriptionRecord { get; set; }
    }
}
