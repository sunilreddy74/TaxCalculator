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
        [Route("sales")]
        public ActionResult GetSalesTax([FromQuery]TaxRequest request)
        {
            var tax = this.calculator.GetTaxTypeService<SalesTaxService>().CalculateTax(request.Area, request.Amount);
            return new OkObjectResult(new TaxResponse { ServiceArea = request.Area, Amount = request.Amount, Tax = tax });
        }
    }
}
