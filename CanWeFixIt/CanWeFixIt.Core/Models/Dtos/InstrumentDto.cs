namespace CanWeFixIt.Core.Models.Dtos
{
    public class InstrumentDto
    {
        public long Id { get; set; }
        public string? Sedol { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
