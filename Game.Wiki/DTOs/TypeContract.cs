namespace Game.Wiki.DTOs
{
    public class TypeContract
    {
        public required Guid TypeUid { get; init; }
        public string TypeName { get; set; }
        public string TypeDescription { get; set; } = string.Empty;
    }
}
