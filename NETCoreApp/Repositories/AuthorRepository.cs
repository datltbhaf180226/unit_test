using NETCoreApp.Data;
using NETCoreApp.Models;

namespace NETCoreApp.Repositories
{
    public class AuthorRepository : GenericRepository<Author>
    {
        public AuthorRepository(MyDbContext context) : base(context)
        {

        }
    }
}
