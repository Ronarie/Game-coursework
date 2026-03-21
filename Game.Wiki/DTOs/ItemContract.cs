namespace Game.Wiki.DTOs
{
    public class ItemContract
    {
        public required Guid ItemUid { get; init; }
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
