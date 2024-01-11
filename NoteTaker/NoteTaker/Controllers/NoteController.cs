using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteTaker.Api.Models.Contracts;
using NoteTaker.Models.Entities;

namespace NoteTaker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly ILogger<NoteController> _logger;
        private readonly ApplicationDbContext _context;
        public NoteController(ILogger<NoteController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("get-all-notes")]
        public IActionResult GetNotes()
        {
            return Ok(_context.Notes.ToList());
        }

        [HttpGet]
        [Route("get-notes-by-author-id")]
        public IActionResult GetNotes(string id)
        {
            return Ok(_context.Notes.ToList()
                .Where(note => note?.Author?.Id.ToString() == id)
            );
        }

        [HttpPost]
        [Route("add-note")]
        public async Task<IActionResult> AddNote(AddNoteRequest request)
        {
            var author = _context.Authors.FirstOrDefault(x => x.Name == request.AuthorName);
            var upsertAuthor = new Author
            {
                Id = author?.Id ?? Guid.NewGuid(),
                Name = author?.Name ?? request?.AuthorName,
            };

            var newNote = new Note
            {
                Id = Guid.NewGuid(),
                Title = request?.NoteTitle ?? string.Empty,
                Text = request?.NoteText ?? string.Empty,
                Author = upsertAuthor,
            };
            
            if(author == null)
            {
                _context.Authors.Add(upsertAuthor);
            }
            else
            {
                // Detach the existing entity before attaching the new one
                _context.Entry(author).State = EntityState.Detached;

                upsertAuthor.Id = author.Id;
                _context.Authors.Update(upsertAuthor);
            }
            _context.Notes.Add(newNote);
            await _context.SaveChangesAsync();

            return Created(string.Empty, newNote);
        }

        [HttpPut]
        [Route("edit-note")]
        public async Task<IActionResult> UpdateNote(Note note)
        {
            if(string.IsNullOrEmpty(note?.Id.ToString()) || !_context.Notes.Any(x => x.Id == note.Id))
            {
                return BadRequest($"Note with note id: {note?.Id} doesn't exist");
            }

            _context.Notes.Update(note);
            await _context.SaveChangesAsync();
            return Ok(note);
        }

        [HttpDelete]
        [Route("delete-note-by-id/{id}")]
        public async Task<IActionResult> DeleteNote(string id)
        {
            var note = _context.Notes.Where(n => id == n.Id.ToString()).Include(n => n.Author).FirstOrDefault();

            if (note == null)
            {
                return NotFound("Note not found");
            }

            // Check if there are no more notes for the author
            var authorId = note.Author.Id;
            var hasOtherNotes = _context.Notes.Any(n => n.Author.Id == authorId && n.Id.ToString() != id);

            if (!hasOtherNotes)
            {
                // No more notes for the author, delete the author
                var author = _context.Authors.Where(a => authorId.ToString() == a.Id.ToString()).FirstOrDefault();
                if (author != null)
                {
                    _context.Authors.Remove(author);
                }
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return Ok("Deleted");
        }
    }
}