using DataSources;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using TaxCalculator;

namespace TaxCalculatorTest
{
    public class SalesTaxCalculatorTest
    {
        private Mock<IDataSource> _dataSource;
        private ITaxCalculatorService _service;

        [OneTimeSetUp]
        public void Setup()
        {
            _dataSource = new Mock<IDataSource>();
            _service = new SalesTaxService(_dataSource.Object);
        }

        [Test]
        [TestCase("Alamance", "6.75", "1000", "67.50")]
        [TestCase("Mecklenburg", "7.25", "570", "41.325")]
        [TestCase("Watauga", "6.75", "999.99", "67.499325")]
        public void TaxCalculator_Sales_ExpectedBehavior_Equal(string area, string taxPercent, string price, string output)
        {
            _dataSource.Setup(d => d.GetTaxPercentage(area)).Returns(Convert.ToDecimal(taxPercent));
            var tax = _service.CalculateTax(area, Convert.ToDecimal(price));

            Assert.AreEqual(tax, Convert.ToDecimal(output));
        }

        [Test]
        [TestCase("Durham", "7.5", "99", "7.5")]
        [TestCase("Wake", "7.25", "570", "41.32")]
        [TestCase("Yadkin", "6.75", "999.99", "67.4993")]
        public void TaxCalculator_Sales_ExpectedBehavior_NotEqual(string area, string taxPercent, string price, string output)
        {
            _dataSource.Setup(d => d.GetTaxPercentage(area)).Returns(Convert.ToDecimal(taxPercent));
            var tax = _service.CalculateTax(area, Convert.ToDecimal(price));

            Assert.AreNotEqual(tax, Convert.ToDecimal(output));
        }

        [Test]
        [TestCase("Invalid")]
        [TestCase("")]
        public void TaxCalculator_Sales_ExpectedBehavior_Invalid(string area)
        {
            _dataSource.Setup(d => d.GetTaxPercentage(area)).Throws(new InvalidDataException());
            Assert.Throws<InvalidDataException>(() => _service.CalculateTax(area, 0m));
        }
    }
}