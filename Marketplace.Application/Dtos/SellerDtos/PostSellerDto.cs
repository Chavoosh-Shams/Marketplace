using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Marketplace.Application.Dtos.SellerDtos
{
    public class PostSellerDto
    {
        [JsonIgnore]
        public Guid GuidKey { get; private set; }
        public PostSellerDto()
        {
            GuidKey = Guid.NewGuid();
        }
        [Required(ErrorMessage = "FirstName is required")]
        [MaxLength(50, ErrorMessage = "FirstName cannot exceed 50 characters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        [MaxLength(50, ErrorMessage = "LastName cannot exceed 50 characters")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "StoreName is required")]
        [MaxLength(50, ErrorMessage = "StoreName cannot exceed 50 characters")]
        public string StoreName { get; set; }
        [Required(ErrorMessage = "BirthDate is required")]
        public DateTime BirthDate { get; set; }
    }
}
