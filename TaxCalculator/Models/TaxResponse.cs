using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator
{
    public class TaxResponse
    {
        public string ServiceArea { get; set; }
        public decimal Amount { get; set; }
        public decimal Tax { get; set; }
    }
}
