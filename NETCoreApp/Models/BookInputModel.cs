namespace NETCoreApp.Models
{
    public class BookInputModel
    {
        public string Title { get; set; }
        public long AuthorId { get; set; }
    }

    public class BookUpdateModel : BookInputModel
    {
        public long Id { get; set; }
    }
}
