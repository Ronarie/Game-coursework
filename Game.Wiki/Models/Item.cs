using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Game.Wiki.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        public Guid ItemUid { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public Location Location { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
