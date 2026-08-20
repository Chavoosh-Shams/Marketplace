

namespace Marketplace.Application.Dtos.SellerDtos
{
    public class GetByIdSellerDto
    {
        public Guid Id { get; set; }
        public Guid GuidKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StoreName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
