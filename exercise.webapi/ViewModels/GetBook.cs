namespace exercise.webapi.ViewModels
{
    public class GetBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public GetAuthor Author { get; set; }
    }
}
