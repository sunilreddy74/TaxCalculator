using DataSources;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using TaxCalculator;

namespace TaxCalculatorTest
{
    public class FileSourceTest
    {
        private IDataSource _dataSource;

        [OneTimeSetUp]
        public void Setup()
        {
            _dataSource = new FileSource();
        }

        [Test]
        [TestCase("Alamance", "6.75")]
        [TestCase("mecklenburg", "7.25")]
        [TestCase("Watauga", "6.75")]
        public void Read_DataSource_ExpectedBehavior_Equal(string area, string taxPercent)
        {
            var percent = _dataSource.GetTaxPercentage(area);
            Assert.AreEqual(percent, Convert.ToDecimal(taxPercent));
        }

        [Test]
        [TestCase("durham", "3.5")]
        [TestCase("Wake", "3.25")]
        [TestCase("Yadkin", "3.75")]
        public void Read_DataSource_ExpectedBehavior_NotEqual(string area, string taxPercent)
        {
            var percent = _dataSource.GetTaxPercentage(area);
            Assert.AreNotEqual(percent, Convert.ToDecimal(taxPercent));
        }

        [Test]
        [TestCase("Invalid")]
        [TestCase("")]
        public void TaxCalculator_Sales_ExpectedBehavior_Invalid(string area)
        {
            //Assert.Throws<InvalidDataException>(() => _service.CalculateTax(area, 0m));
        }
    }
}
