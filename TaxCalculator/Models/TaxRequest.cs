using Microsoft.AspNetCore.Mvc;

namespace TaxCalculator
{
    [BindProperties(SupportsGet = true)]
    public class TaxRequest
    {
        public string Area { get; set; }

        public decimal Amount { get; set; }
        public string State { get; set; } = "NC";
    }
}
