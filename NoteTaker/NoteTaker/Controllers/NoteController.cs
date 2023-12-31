using Microsoft.AspNetCore.Mvc;
using NoteTaker.Api.Models.Contracts;
using NoteTaker.Models.Entities;

namespace NoteTaker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly ILogger<NoteController> _logger;
        private static IEnumerable<Note> notes = new List<Note>{
            new Note
            {
                Author = new Author { Id = Guid.Empty, Name = "DragonForce" },
                Title = "Through the Fire and Flames",
                Text = "Now here we stand with their blood on our hands\r\nWe fought so hard, now can we understand?\r\nI'll break the seal of this curse if I possibly can\r\nFor freedom of every man\r\nSo far away, we wait for the day\r\nFor the lives all so wasted and gone\r\nWe feel the pain of a lifetime lost in a thousand days\r\nThrough the fire and the flames, we carry on",
            },
        };

        public NoteController(ILogger<NoteController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("get-all-notes")]
        public IActionResult GetNotes()
        {
            return Ok(notes);
        }

        [HttpGet]
        [Route("get-notes-by-author-id")]
        public IActionResult GetNotes(string id)
        {
            return Ok(notes.Where(note => note?.Author?.Id.ToString() == id));
        }

        [HttpPost]
        [Route("add-note")]
        public IActionResult AddNote(AddNoteRequest request)
        {
            var authorExists = notes?.Any((x) => x?.Author?.Name == request?.AuthorName) ?? false;
            var authorGuid = authorExists
                ? notes?.FirstOrDefault(x => x?.Author?.Name == request?.AuthorName)?.Author?.Id
                :
                Guid.NewGuid();

            var note = new Note
            {
                Author = new Author
                {
                    Id = authorGuid ?? Guid.NewGuid(),
                    Name = request?.AuthorName ?? string.Empty,
                },
                Id = Guid.NewGuid(),
                Title = request?.NoteTitle ?? string.Empty,
                Text = request?.NoteText ?? string.Empty,
            };

            notes = notes.Append(note);
            return Created(string.Empty, note);
        }

        [HttpPut]
        [Route("edit-note")]
        public IActionResult UpdateNote(Note note)
        {
            if(
                !(notes?.Any(x => x?.Id == note?.Id)) ??
                false //for now as a static variable, notes will always be populated
            )
            {
                return BadRequest($"Note with note id: {note?.Id} doesn't exist");
            }

            notes = notes?.Where(x => !(x?.Id.ToString() == note?.Id.ToString()));
            notes = notes.Append(note);
            return Ok(note);
        }

        [HttpDelete]
        [Route("delete-note-by-id/{id}")]
        public IActionResult DeleteNote(string id)
        {
            if(notes.Any(note => (note?.Id.ToString() == id)))
            {
                notes = notes.Where(note => !(note?.Id.ToString() == id));
                return Ok("Deleted");
            }
            return Ok("Unmodified");
        }
    }
}