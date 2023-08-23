using ConceptArchitect.Data;
using ConceptArchitect.Utils;
using System.Data;

namespace ConceptArchitect.BookManagement.Repositories.Ado
{
    public class AdoBookRepository : IRepository<Book, string>
    {
        DbManager dbManager;

        public AdoBookRepository(DbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        private Book BookExtractor(IDataReader reader)
        {
            Book book = new Book();
            book.Id = (string)reader["id"];
            book.Title = (string)reader["title"];
            book.Description = (string)reader["description"];
            book.AuthorId = (string)reader["author_id"];
            book.CoverPhoto = (string)reader["cover_photo"];
            return book;
        }

        public async Task<Book> Add(Book book)
        {
            var query = $"insert into books(id,title,description,author_id,cover_photo) " +
                $"values('{book.Id}','{book.Title}','{book.Description}','{book.AuthorId}','{book.CoverPhoto}')";
            
            await dbManager.ExecuteUpdateAsync(query);
            
            return book;
        }

        public async Task Delete(string id)
        {
            await dbManager.ExecuteUpdateAsync($"delete from books where id='{id}'");
        }

        public async Task<List<Book>> GetAll()
        {
            return await dbManager.QueryAsync("select * from books", BookExtractor);
        }

        public async Task<List<Book>> GetAll(Func<Book, bool> predicate)
        {
            var books = await GetAll();
            return (from book in books
                    where predicate(book)
                    select book).ToList();
        }

            public async Task<Book> GetById(string id)
        {
            return await dbManager.QueryOneAsync($"select * from books where id='{id}'", BookExtractor);
        }

        public async Task<Book> Update(Book book, Action<Book, Book> mergeOldNew)
        {
            var oldBook = await GetById(book.Id);
            if(oldBook != null)
            {
                mergeOldNew(oldBook, book);
                var query = $"update books set " +
                            $"title='{oldBook.Title}'," +
                            $"description='{oldBook.Description}'," +
                            $"author_id='{oldBook.AuthorId}'," +
                            $"cover_photo='{oldBook.CoverPhoto}' " +
                            $"where id = '{oldBook.Id}'";
                await dbManager.ExecuteUpdateAsync(query);
            }
            return oldBook;
        }
    }
}
