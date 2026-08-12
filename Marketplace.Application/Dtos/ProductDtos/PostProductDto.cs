using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.ApplicationServices.Dtos.ProductDtos
{
    public class PostProductDto
    {
        [JsonIgnore]
        public Guid GuidKey { get; private set; }
        public PostProductDto()
        {
            GuidKey = Guid.NewGuid();
        }
        [Required(ErrorMessage = "CategoryId is required")]
        public Guid CateoryId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(50, ErrorMessage = "Title cannot exceed 50 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "DescriptionRecord is required")]
        [MaxLength(50, ErrorMessage = "DescriptionRecord cannot exceed 50 characters")]
        public string DescriptionRecord { get; set; }
    }
}
