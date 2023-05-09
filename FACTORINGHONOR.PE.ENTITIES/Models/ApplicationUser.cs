using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using FACTORINGHONOR.PE.CORE.Helpers;

namespace FACTORINGHONOR.PE.ENTITIES.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string PaternalSurname { get; set; }

        [Required]
        public string MaternalSurname { get; set; }

        [NotMapped]
        public string FullName => $"{Name}, {PaternalSurname} {MaternalSurname}";

        [Required]
        public string Dni { get; set; }

        public DateTime? BirthDate { get; set; }

        public Guid CurrencyTypeId { get; set; }

        public CurrencyType CurrencyType { get; set; }

        public int RateType { get; set; } = ConstantHelpers.Rate.Type.EFFECTIVE;

        public IEnumerable<FeeReceipt> FeeReceipts { get; set; }
    }
}
