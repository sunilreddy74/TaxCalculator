using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSources
{
    public class FileSource : IDataSource
    {
        private readonly string _sourceFolder = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "DataSource"));
        private static Dictionary<string, decimal> taxMappings = new Dictionary<string, decimal>();

        public decimal GetTaxPercentage(string serviceArea)
        {
            if (!taxMappings.ContainsKey(serviceArea.ToLower()))
            {
                ReadFromJsonFile(serviceArea.ToLower());
            }

            return taxMappings[serviceArea.ToLower()];
        }

        private void ReadFromJsonFile(string serviceArea)
        {
            var fileEntries = Directory.EnumerateFiles(_sourceFolder, "*.json");
            foreach (var filePath in fileEntries)
            {
                try
                {
                    var taxPercentages = Helpers.LoadConfigurationFromJson<Dictionary<string, decimal>>(filePath);
                    taxMappings.Append(taxPercentages);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            if (!taxMappings.ContainsKey(serviceArea.ToLower())) {
                throw new ArgumentException($@"Did not find tax percentage for the provided service area or zipcode: {serviceArea}");
            }
        }
    }
}
