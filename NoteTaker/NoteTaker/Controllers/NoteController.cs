using Microsoft.AspNetCore.Mvc;
using NoteTaker.Models.Entities;

namespace NoteTaker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly ILogger<NoteController> _logger;
        public static IEnumerable<Note> notes = Enumerable.Empty<Note>();

        public NoteController(ILogger<NoteController> logger)
        {
            _logger = logger;
            notes = new List<Note>{
                new Note
                {
                    Author = new Author { Id = Guid.NewGuid(), Name = "Kawaiivee" },
                    Title = "Title",
                    Text = "Text",
                }
            };
        }

        [HttpGet]
        [Route("getNotes")]
        public IEnumerable<Note> GetNotes()
        {
            return notes;
        }
    }
}