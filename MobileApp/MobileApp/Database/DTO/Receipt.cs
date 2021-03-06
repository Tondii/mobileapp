﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileApp.Database.DTO
{
    public class Receipt
    {
        [Key]
        public long Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string PicturePath { get; set; }
        public DateTime? SaleDate { get; set; }
        [ForeignKey("Company")]
        public long CompanyId { get; set; }
        public Company Company { get; set; }
        public float BruttoSummary { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Comment { get; set; } = string.Empty;
        public string GoogleResponse { get; set; }
    }
}
