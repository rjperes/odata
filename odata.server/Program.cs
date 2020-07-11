using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace odata.server
{
    public static class Program
    {
        public static void Main(string[] args) =>
            CreateWebHostBuilder(args)
                .Build()
                .CreateDbIfNotExists()
                .Run();

        private static IHost CreateDbIfNotExists(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var parentChildContext = services.GetRequiredService<ParentChildContext>();
                parentChildContext.Database.EnsureCreated();
                //parentChildContext.Database.Migrate();

                var blogContext = services.GetRequiredService<BlogContext>();
                blogContext.Database.EnsureCreated();
                //blogContext.Database.Migrate();
            }

            return host;
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseStartup<Startup>();
                });
    }
}
