using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator
{
    public interface ITaxCalculatorService
    {
        decimal CalculateTax(string serviceArea, decimal salesPrice);
    }
}
