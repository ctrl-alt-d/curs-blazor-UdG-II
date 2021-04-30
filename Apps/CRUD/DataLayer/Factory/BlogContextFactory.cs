using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Datalayer
{
    public class BlogContextFactory : IDesignTimeDbContextFactory<BlogContext>
    {
        public BlogContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlogContext>();
            optionsBuilder.UseSqlServer("Data Source=localhost;User Id=sa;Password=X1nGuXunG1;Initial Catalog=Divendres;Trusted_Connection=False;MultipleActiveResultSets=true");
            return new BlogContext(optionsBuilder.Options);
        }
    }

}
