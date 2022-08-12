namespace CanWeFixIt.Core.Models.Entities
{
    public class MarketData
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public long DataValue { get; set; }
        public string Sedol { get; set; }
    }
}
