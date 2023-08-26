using ConceptArchitect.BookManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace BooksWeb02.Controllers
{
    public class ReviewController : Controller
    {
        IReviewService authorService;

        public ReviewController(IReviewService authors)
        {
            this.authorService = authors;
        }
        public async Task<ViewResult> Index()
        {
            var books = await authorService.GetAllReviews();

            return View(books);
        }

        [HttpGet]
        public async Task<ViewResult> review(string id)
        {
            await Task.Delay(500);
            review author = new review();
            author.book_id = id;
            return View(author);
        }
        [HttpPost]
        public async Task<ActionResult> review(review author)
        {
            await authorService.AddReview(author);

            return RedirectToAction("Index");
        }
    }
}
