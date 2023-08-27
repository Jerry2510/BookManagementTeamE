using ConceptArchitect.BookManagement;
using Microsoft.AspNetCore.Mvc;

namespace BooksWeb02.ApiControllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsApiController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsApiController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Author>>> GetAllAuthors()
        {
            var authors = await _authorService.GetAllAuthors();
            return Ok(authors);
        }

        [HttpGet("{id}", Name = "GetAuthorById")]
        public async Task<ActionResult<Author>> GetAuthorById(string id)
        {
            var author = await _authorService.GetAuthorById(id);

            if (author == null)
                return NotFound();

            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult> AddAuthor(Author author)
        {
            await _authorService.AddAuthor(author);

            return CreatedAtRoute("GetAuthorById", new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthor(string id, Author author)
        {
            if (id != author.Id)
                return BadRequest();

            await _authorService.UpdateAuthor(author);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(string id)
        {
            await _authorService.DeleteAuthor(id);

            return NoContent();
        }
    }
}