using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Application.Dtos
{
    public class PostCategoryDto
    {
        [JsonIgnore]
        public Guid GuidKey { get; private set; }
        public PostCategoryDto()
        {
            GuidKey = Guid.NewGuid();
        }
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(50, ErrorMessage = "Title cannot exceed 50 characters")]
        public string Title { get; set; }
    }
}
