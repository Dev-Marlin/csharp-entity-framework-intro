using exercise.webapi.Models;
using System.Text.Json.Serialization;

namespace exercise.webapi.ViewModels
{
    public class GetFullAuthor
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<GetBooksByAuthor> Books { get; set; } = new List<GetBooksByAuthor>();
    }
}
