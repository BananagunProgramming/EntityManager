
using System;
using System.ComponentModel.DataAnnotations;

namespace EntityManager.Models
{
    public class Client
    {
        [Required]
        public Guid ClientId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string EntityCode { get; set; }
        public int YearIncorporated { get; set; }
        public string TaxId { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Schedule { get; set; }
        public string YearEndDate { get; set; }
        public string FiscalYearEndDate { get; set; }
        public string Managed { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}