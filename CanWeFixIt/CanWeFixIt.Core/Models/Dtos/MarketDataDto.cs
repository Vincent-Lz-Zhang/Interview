namespace CanWeFixIt.Core.Models.Dtos
{
    public class MarketDataDto
    {
        public long Id { get; set; }
        public long DataValue { get; set; }
        public long? InstrumentId { get; set; }
        public bool Active { get; set; }
    }
}
