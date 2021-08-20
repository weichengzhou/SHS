using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;



/*  Automatic create dbcontext if use mysql database.
    dotnet ef dbcontext scaffold "server=localhost;Port=3306;Database=shs;User=root;Password=root;" "Pomelo.EntityFrameworkCore.MySql" -o ./MyModels -c context -f
*/
namespace SHS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
