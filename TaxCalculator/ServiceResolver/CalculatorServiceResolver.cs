using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator
{
    public class CalculatorServiceResolver
    {
        private readonly IServiceProvider serviceProvider;
        public CalculatorServiceResolver(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ITaxCalculatorService GetTaxTypeService<T>() where T: ITaxCalculatorService
        {
            return (ITaxCalculatorService)
                    serviceProvider
                    .GetService(typeof(T));
        }
    }
}
