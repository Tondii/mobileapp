using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace MobileApp.Database.DTO
{
    public class Receipt
    {
        [Key]
        public long Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string PicturePath { get; set; }
        public Company Company { get; set; }
        public float BruttoSummary { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Comment { get; set; }
        public string GoogleResponse { get; set; }
    }
}
