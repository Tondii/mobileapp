using System.ComponentModel.DataAnnotations;

namespace MobileApp.Database.DTO
{
    public class Company
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string VatIdentificationNumber { get; set; } = string.Empty;
    }
}
