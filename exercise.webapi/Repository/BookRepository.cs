using System.Collections.Generic;
using exercise.webapi.Data;
using exercise.webapi.Models;
using exercise.webapi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace exercise.webapi.Repository
{
    public class BookRepository: IBookRepository
    {
        DataContext _db;
        
        public BookRepository(DataContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _db.Books.Include(b => b.Author).ToListAsync();
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _db.Books.Include(b => b.Author).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Book> UpdateBook(int bookId, int authorId)
        {
            Book book = await _db.Books.Include(b => b.Author).FirstAsync(x => x.Id == bookId);

            var temp = _db.Books.Update(book);

            var author = _db.Authors.FirstOrDefault(x => x.Id == authorId);

            temp.Entity.Author = author;
            temp.Entity.AuthorId = author.Id;

            await _db.SaveChangesAsync();

            return book;
        }

        public async Task<bool> DeleteBook(int id)
        {
            Book book = await _db.Books.Include(b => b.Author).SingleOrDefaultAsync(x => x.Id == id);

            if(book == null) 
                return false;

            _db.Books.Remove(book);

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<Book> CreateBook(PostBook postBook, int authorId)
        {
            Author a = await _db.Authors.FirstAsync(author => author.Id == authorId);

            Book book = new Book();

            book.Author = a;
            book.AuthorId = authorId;
            book.Id = _db.Books.Max(b => b.Id) +1;
            book.Title = postBook.Title;

            if (a == null)
                return null;

            await _db.Books.AddAsync(book);
            await _db.SaveChangesAsync();

            return book;
        }
    }
}
