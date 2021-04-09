namespace NETCoreApp.Models
{
 public class Book : BaseEntity
 {
    public string Title { get; set; }
    public long AuthorId { get; set; }
    public Author Author { get; set; }
 }
}
