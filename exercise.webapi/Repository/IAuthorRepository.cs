using exercise.webapi.Models;

namespace exercise.webapi.Repository
{
    public interface IAuthorRepository
    {
        public Task<Author> GetById(int id);

        public Task<IEnumerable<Author>> GetAllAuthors();
    }
}
