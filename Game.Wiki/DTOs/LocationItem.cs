namespace Game.Wiki.DTOs
{
    public class LocationItem
    {
        public Guid ItemUid { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
