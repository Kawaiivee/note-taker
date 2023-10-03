namespace NoteTaker.Api.Models.Contracts
{
    public class AddNoteRequest
    {
        public string AuthorName { get; set; }
        public string NoteTitle { get; set; }
        public string NoteText { get; set; }
    }
}
