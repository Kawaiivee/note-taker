namespace NoteTaker.Models.Entities
{
    public class Note
    {
        public Guid Id { get; set; }
        public Author Author { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }
}