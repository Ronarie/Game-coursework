namespace Game.Kafka.Contracts
{
    public class TypeItemFound
    {
        public Guid TypeId { get; set; }
        public Guid ItemId { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}
