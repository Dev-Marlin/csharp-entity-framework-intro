using exercise.webapi.Models;
using exercise.webapi.ViewModels;

namespace exercise.webapi.Repository
{
    public interface IBookRepository
    {
        public Task<IEnumerable<Book>> GetAllBooks();

        public Task<Book> GetBookById(int id);

        public Task<Book> UpdateBook(int bookId, int AuthorId);

        public Task<bool> DeleteBook(int id);

        public Task<Book> CreateBook(PostBook postBook, int authorId);
    }
}
