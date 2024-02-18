using System.ComponentModel.DataAnnotations;

namespace EFMemoryExample.Data
{
    public class Item
    {
        [Key]
        public long Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Type { get; set; }

    }
}
