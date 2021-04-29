using Datalayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer
{
    public static class ServiceExtesions
    {
        public static void AddMyContextFactory(this IServiceCollection services)
        {
            var connectionstring = "Data Source=localhost;User Id=sa;Password=X1nGuXunG1;Initial Catalog=Divendres;Trusted_Connection=False;MultipleActiveResultSets=true";
            services.AddDbContextFactory<BlogContext>(
                opt => opt.UseSqlServer(connectionstring)
            );
        }

    }
}
