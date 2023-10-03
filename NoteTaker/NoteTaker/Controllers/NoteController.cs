using Microsoft.AspNetCore.Http.HttpResults;
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
                Author = new Author { Id = Guid.Empty, Name = "Kawaiivee" },
                Title = "Title",
                Text = "Text",
            }
        };

        public NoteController(ILogger<NoteController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("getNotes")]
        public IActionResult GetNotes()
        {
            return Ok(notes);
        }

        [HttpGet]
        [Route("getNotesByAuthorId")]
        public IActionResult GetNotes(string id)
        {
            return Ok(notes.Where(note => note?.Author?.Id.ToString() == id));
        }


        [HttpPost]
        [Route("addNote")]
        public IActionResult AddNote(AddNoteRequest request)
        {
            var authorExists = notes?.Any((x) => x?.Author?.Name == request?.AuthorName) ?? false;
            var authorGuid = authorExists
                ? notes?
                    .FirstOrDefault(x => x?.Author?.Name == request?.AuthorName)?
                    .Author?.Id
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
        [Route("editNote")]
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
        [Route("deleteNoteByNoteId")]
        public IActionResult DeleteNote(string id)
        {
            notes = notes.Where(note => !(note?.Id.ToString() == id));
            return Ok();
        }
    }
}