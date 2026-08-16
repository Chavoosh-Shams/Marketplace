

namespace Marketplace.Application.Dtos.SellerDtos
{
    public class GetAllSellerDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StoreName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
