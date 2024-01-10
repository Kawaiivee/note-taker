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
            
            _context.Authors.Add(upsertAuthor);
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
            if(_context.Notes.Any(note => note.Id.ToString() == id))
            {
                _context.Notes.Remove(await _context.Notes.FirstOrDefaultAsync(note => note.Id.ToString() == id));
                await _context.SaveChangesAsync();
                return Ok("Deleted");
            }
            await _context.SaveChangesAsync();
            return Ok("Unmodified");
        }
    }
}