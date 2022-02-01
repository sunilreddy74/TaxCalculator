using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TaxCalculator
{
    [BindProperties(SupportsGet = true)]
    public class TaxRequest
    {
        [Required]
        public string Area { get; set; }

        [Required]
        public decimal Amount { get; set; }
        public string State { get; set; } = "NC";
    }
}
