using ConceptArchitect.BookManagement;
using Microsoft.AspNetCore.Mvc;

namespace BooksWeb02.ApiControllers
{
    [ApiController]
    [Route("/api/books")]
    public class BooksApiController:ControllerBase
    {

        private readonly IBookService bookService;

        public BooksApiController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>>GetAllBooks()
        {
            var books= await bookService.GetAllBooks();
            return Ok(books);
        }

        [HttpGet("{id}",Name ="GetBookById")]
        public async Task<ActionResult<Book>>GetBookById(string id)
        {
            var book = await bookService.GetBookById(id);
            if(book == null)
            {
                return NotFound();
            }
            else
            return Ok(book);

        }

        [HttpPost]
        public async Task<ActionResult> AddBook(Book book)
        {
            await bookService.AddBook(book);

            return CreatedAtRoute("GetAuthorById", new { id = book.Id }, book);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(string id, Book author)
        {
            if (id != author.Id)
                return BadRequest();

            await bookService.UpdateBook(author);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(string id)
        {
            await bookService.DeleteBook(id);

            return NoContent();
        }

    }
}
