using Microsoft.EntityFrameworkCore;

namespace Datalayer
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options)
        : base(options)
            {
            }

        public DbSet<Blog> Blogs => Set<Blog>();
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<AuditEntry> AuditEntries => Set<AuditEntry>();

    }

}
