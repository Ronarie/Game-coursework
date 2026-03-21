namespace Game.Wiki.Models
{
    public class Category
    {
        public int Id { get; set; }
        public Guid TypeUid { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<Item> Items { get; set; } = new();
    }
}
