using System.ComponentModel.DataAnnotations;

namespace MobileApp.Database.DTO
{
    public class Company
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string VatIdentificationNumber { get; set; }
    }
}
