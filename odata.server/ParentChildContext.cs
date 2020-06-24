using Microsoft.EntityFrameworkCore;
using odata.common;

namespace odata.server
{
    public class ParentChildContext : DbContext
    {
        public ParentChildContext(DbContextOptions<ParentChildContext> options) : base(options) { }

        public DbSet<Parent> Parents { get; set; }
        public DbSet<Child> Children { get; set; }
    }
}
