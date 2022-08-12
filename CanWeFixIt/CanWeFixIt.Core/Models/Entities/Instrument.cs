namespace CanWeFixIt.Core.Models.Entities
{
    public class Instrument
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public string Sedol { get; set; } = string.Empty;
        public string Name { get; set; }
    }
}
