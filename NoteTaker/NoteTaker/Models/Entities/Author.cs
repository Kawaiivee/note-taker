using System.ComponentModel.DataAnnotations;

namespace NoteTaker.Models.Entities
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
