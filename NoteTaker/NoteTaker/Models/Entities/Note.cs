using System.ComponentModel.DataAnnotations;

namespace NoteTaker.Models.Entities
{
    public class Note
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        // Navigation Property
        public Author Author { get; set; }
    }
}