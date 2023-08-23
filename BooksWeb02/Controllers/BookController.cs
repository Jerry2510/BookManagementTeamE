using ConceptArchitect.BookManagement;
using Microsoft.AspNetCore.Mvc;

namespace BooksWeb02.Controllers
{
    public class BookController:Controller
    {
        IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }


        public async Task<ViewResult> Index()
        {
            var books = await bookService.GetAllBooks();
            return View(books);
        }

        public async Task<ViewResult> Details(string id)
        {
            var author = await bookService.GetBookById(id);
            return View(author);
        }

        [HttpGet]
        public ViewResult Add()
        {
            return View(new Book());
        }

        [HttpPost]
        public async Task<ActionResult> Add(Book book)
        {
            await bookService.AddBook(book);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ViewResult> Edit(string id)
        {
            var author = await bookService.GetBookById(id);
            return View(author);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Book book)
        {
            await bookService.UpdateBook(book);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            await bookService.DeleteBook(id);
            Index();
            return RedirectToAction("Index");
            
        }

        //[HttpPost]
        //public async Task<ActionResult> Delete()
        //{
            
            
        //}

    }
}
