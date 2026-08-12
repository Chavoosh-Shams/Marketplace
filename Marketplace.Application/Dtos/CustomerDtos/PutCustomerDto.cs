namespace InvoiceApp.ApplicationServices.Dtos.CustomerDtos
{
    public class PutCustomerDto
    {
        public Guid GuidKey { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}
