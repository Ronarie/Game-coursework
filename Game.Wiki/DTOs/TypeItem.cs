namespace Game.Wiki.DTOs
{
    public class TypeItem
    {
        public Guid ItemUid { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
