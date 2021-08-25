using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DatingWebAppScratch.Models
{
    public class StockApplication
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("AppUser")]
        public int UserId { get; set; }
        public string ApplicationName { get; set; }

        [DataType(DataType.Url)]
        public string Link { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int InvestmentValue { get; set; }

        public int ValueTillDate { get; set; }

        public virtual AppUser User { get; set; }

    }
}
