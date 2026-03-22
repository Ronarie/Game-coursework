namespace Game.Kafka.Contracts
{
    public class LocationItemFound
    {
        public Guid LocationId { get; set; }
        public Guid ItemId { get; set; }
        public string Location { get; set; } = string.Empty;
    }
}
