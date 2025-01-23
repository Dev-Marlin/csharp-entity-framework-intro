using System.Collections.Generic;
using exercise.webapi.Models;
using exercise.webapi.Repository;
using exercise.webapi.ViewModels;
using Microsoft.AspNetCore.Authentication;
using static System.Reflection.Metadata.BlobBuilder;

namespace exercise.webapi.Endpoints
{
    public static class BookApi
    {
        public static void ConfigureBooksApi(this WebApplication app)
        {
            var books = app.MapGroup("/library");
            books.MapGet("/books", GetBooks);
            books.MapGet("/bookbyid/{id}", GetBookById);
            books.MapPut("/update/{bookid}/{authorid}", UpdateBook);
            books.MapDelete("/delete/{id}", DeleteBook);
            books.MapPost("/create/", CreateBook);

        }

        private static async Task<IResult> GetBooks(IBookRepository bookRepository)
        {
            List<GetBook> books = new List<GetBook>();

            var dbBooks = await bookRepository.GetAllBooks();

            foreach (Book book in dbBooks)
            {
                GetAuthor getAuthor = new GetAuthor();

                getAuthor.FirstName = book.Author.FirstName;
                getAuthor.LastName = book.Author.LastName;
                getAuthor.Email = book.Author.Email;

                GetBook tempBook = new GetBook();
                tempBook.Id = book.Id;
                tempBook.Title = book.Title;
                tempBook.Author = getAuthor;

                books.Add(tempBook);
            }

            return TypedResults.Ok(books);
        }

        private static async Task<IResult> GetBookById(IBookRepository bookRepository, int id)
        {
            Book book = bookRepository.GetBookById(id).Result;
            GetBook gb = turnToDTO(book);

            return TypedResults.Ok(gb);
        }

        private static async Task<IResult> UpdateBook(IBookRepository bookRepository, int bookId, int authorId)
        {
            Book book = bookRepository.UpdateBook(bookId, authorId).Result;
            GetBook gb = turnToDTO(book);

            return TypedResults.Ok(gb);
        }

        private static async Task<IResult> DeleteBook(IBookRepository bookRepository, int id)
        {
            bool answer = bookRepository.DeleteBook(id).Result;

            if(!answer)
            {
                return TypedResults.NotFound("Book doesnt exist");
            }

            return TypedResults.Ok();
        }

        public static async Task<IResult> CreateBook(IBookRepository bookRepository, PostBook book, int authorid)
        {
            Book b = bookRepository.CreateBook(book, authorid).Result;

            if (b == null)
                return TypedResults.NotFound();

            return TypedResults.Created();
        }

        private static GetBook turnToDTO(Book book)
        {
            GetBook gb = new GetBook();

            GetAuthor getAuthor = new GetAuthor();

            getAuthor.FirstName = book.Author.FirstName;
            getAuthor.LastName = book.Author.LastName;
            getAuthor.Email = book.Author.Email;

            gb.Id = book.Id;
            gb.Title = book.Title;
            gb.Author = getAuthor;

            return gb;
        }
    }
}
