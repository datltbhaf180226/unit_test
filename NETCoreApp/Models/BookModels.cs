namespace NETCoreApp.Models
{
    public class BookModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public AuthorModel Author { get; set; }
    }

    public class BookCreateModel
    {
        public string Title { get; set; }
        public long AuthorId { get; set; }
    }

    public class BookUpdateModel : BookCreateModel
    {
        public long Id { get; set; }
    }
}
