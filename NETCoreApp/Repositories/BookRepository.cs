using System.Collections.Generic;
using System.Linq;
using NETCoreApp.Data;
using NETCoreApp.Models;

namespace NETCoreApp.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        IEnumerable<Book> GetAllByAuthorId(long id);
    }

    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(MyDbContext context) : base(context)
        {

        }

        public IEnumerable<Book> GetAllByAuthorId(long id)
        {
            return Entities.Where(o => o.AuthorId == id).AsEnumerable();
        }
    }
}
