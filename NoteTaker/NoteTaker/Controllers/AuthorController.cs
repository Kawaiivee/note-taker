using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NoteTaker.Api.Models.Contracts;
using NoteTaker.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NoteTaker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<AuthorController> _logger;
        private readonly ApplicationDbContext _context;

        public AuthorController(ILogger<AuthorController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("get-all-authors")]
        public IActionResult GetAuthors()
        {
            return Ok(_context.Authors);
        }

        [HttpGet]
        [Route("get-author-by-id/{id}")]
        public IActionResult GetAuthorById(Guid id)
        {
            var author = _context.Authors.FirstOrDefault(a => a.Id == id);
            if (author != null)
            {
                return Ok(author);
            }
            return NotFound($"Author with id: {id} not found");
        }

        [HttpPost]
        [Route("add-author")]
        public async Task<IActionResult> AddAuthor(string name)
        {
            var existingAuthor = _context.Authors.FirstOrDefault(a => a.Name == name);
            if (existingAuthor != null)
            {
                return Conflict($"Author with name: {name} already exists");
            }

            var newAuthor = new Author
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            _context.Authors.Add(newAuthor);
            await _context.SaveChangesAsync();
            return Created(string.Empty, newAuthor);
        }

        [HttpPut]
        [Route("edit-author")]
        public async Task<IActionResult> UpdateAuthor(Author author)
        {
            var existingAuthor = _context.Authors.FirstOrDefault(a => a.Id == author.Id);
            if (existingAuthor == null)
            {
                return NotFound($"Author with id: {author.Id} not found");
            }

            _context.Authors.Remove(existingAuthor);
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return Ok(author);
        }

        [HttpDelete]
        [Route("delete-author-by-id/{id}")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            var existingAuthor = _context.Authors.FirstOrDefault(a => a.Id == id);
            if (existingAuthor != null)
            {
                _context.Authors.Remove(existingAuthor);
                await _context.SaveChangesAsync();
                return Ok("Deleted");
            }

            return Ok("Unmodified");
        }
    }
}