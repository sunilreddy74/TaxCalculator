using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private readonly CalculatorServiceResolver calculator;

        public TaxController(CalculatorServiceResolver calculator)
        {
            this.calculator = calculator;
        }

        [HttpGet]
        [Route("sales/{area}/{amount}")]
        public ActionResult GetSalesTax(string area, decimal amount)
        {
            try
            {
                var tax = this.calculator.GetTaxTypeService<SalesTaxService>().CalculateTax(area, amount);
                return new OkObjectResult(new TaxResponse { ServiceArea = area, Amount = amount, Tax = tax });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
