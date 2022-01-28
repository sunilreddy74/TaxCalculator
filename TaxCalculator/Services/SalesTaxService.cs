using DataSources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator
{
    public class SalesTaxService : ITaxCalculatorService
    {
        private readonly IDataSource dataSource;
        public SalesTaxService(IDataSource dataSource)
        {
            this.dataSource = dataSource;
        }


        public decimal CalculateTax(string serviceArea, decimal salesPrice)
        {
            try
            {
                var taxPercent = dataSource.GetTaxPercentage(serviceArea);

                return salesPrice * taxPercent / 100;
            }
            catch (Exception ex)
            {
                // log exception
                throw ex;
            }
        }
    }
}
