namespace DataSources
{
    public interface IDataSource
    {
        decimal GetTaxPercentage(string serviceArea);
    }
}
