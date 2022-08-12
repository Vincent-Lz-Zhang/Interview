namespace CanWeFixIt.Core.Models
{
    public class MarketDataWithInstrument
    {
        public long MarketDataId { get; set; }
        public long DataValue { get; set; }
        public string Sedol { get; set; }
        public long? InstrumentId { get; set; }
        public bool IsMarketDataActive { get; set; }
        public bool IsInstrumentActive { get; set; }
    }
}
