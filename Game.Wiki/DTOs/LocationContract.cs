namespace Game.Wiki.DTOs
{
    public class LocationContract
    {
        public required Guid LocationUid { get; init; }
        public string LocationName { get; set; }
        public string LocationDescription { get; set; } = string.Empty;
    }
}
