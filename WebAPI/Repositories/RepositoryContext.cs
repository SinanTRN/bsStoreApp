using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class RepositoryContext: DbContext
    {
        public DbSet<Book>Books { get; set; }
    }
}
