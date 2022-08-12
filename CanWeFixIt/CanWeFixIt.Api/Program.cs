using CanWeFixIt.Service.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CanWeFixIt.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Generate some initial data
            using (var scope = host.Services.CreateScope())
            {
                var scopedProvider = scope.ServiceProvider;
                var dbContext = scopedProvider.GetRequiredService<CanWeFixItContext>();

                CanWeFixItContextSeeder.Seed(dbContext);
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://localhost:5010");
                });
    }
}