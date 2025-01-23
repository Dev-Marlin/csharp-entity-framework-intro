using exercise.webapi.Models;
using exercise.webapi.Repository;
using exercise.webapi.ViewModels;

namespace exercise.webapi.Endpoints
{
    public static class AuthorApi
    {
        public static void ConfigureAuthorApi(this WebApplication app)
        {
            var author = app.MapGroup("/authors");

            author.MapGet("/getbyid/{id}", GetById);
            author.MapGet("/getall", GetAllAuthors);
        }

        public static async Task<IResult> GetById(IAuthorRepository repository, int id)
        {
            Author a = await repository.GetById(id);

            List<GetBooksByAuthor> authorsBooks = new List<GetBooksByAuthor>();

            foreach (Book b in a.Books)
            {
                GetBooksByAuthor temp = new GetBooksByAuthor()
                {
                    Id = b.Id,
                    Title = b.Title,
                };

                authorsBooks.Add(temp);
            }

            GetFullAuthor auth = new GetFullAuthor();

            auth.Email = a.Email;
            auth.FirstName = a.FirstName;
            auth.LastName = a.LastName;
            auth.Books = authorsBooks;

            return TypedResults.Ok(auth);
        }

        public static async Task<IResult> GetAllAuthors(IAuthorRepository repository)
        {
            List<GetFullAuthor> authors = new List<GetFullAuthor>();

            var dbAuthors = await repository.GetAllAuthors();

            foreach (Author a in dbAuthors)
            {
                List<GetBooksByAuthor> authorsBooks = new List<GetBooksByAuthor>();

                foreach(Book b in a.Books)
                {
                    GetBooksByAuthor temp = new GetBooksByAuthor()
                    {
                        Id = b.Id,
                        Title = b.Title,
                    };

                    authorsBooks.Add(temp);
                }

                GetFullAuthor auth = new GetFullAuthor();

                auth.Email = a.Email;
                auth.FirstName = a.FirstName;
                auth.LastName = a.LastName;
                auth.Books = authorsBooks;

                authors.Add(auth);
            }

            return TypedResults.Ok(authors);
        }

    }
}
