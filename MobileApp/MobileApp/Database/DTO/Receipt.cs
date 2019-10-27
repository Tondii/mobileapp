using System;
using System.ComponentModel.DataAnnotations;

namespace MobileApp.Database.DTO
{
    public class Receipt
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string PicturePath { get; set; }
        public Company Company { get; set; }
        public double BruttoSummary { get; set; }
    }
}
