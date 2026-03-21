namespace Game.Wiki.Models
{
    public class Location
    {
        public int Id { get; set; }
        public Guid LocationUid { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;
        public string Description {  get; set; } = string.Empty;
        public List<Item> Items { get; set; } = new();
    }
}
