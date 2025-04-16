using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using JobOffersApi.Infrastructure.Logging;
using JobOffersApi.Infrastructure.Modules;

namespace JobOffersApi.Bootstrapper;

public class Program
{
    public static Task Main(string[] args)
        => CreateHostBuilder(args).Build().RunAsync();

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
            .ConfigureModules()
            .UseLogging();
}

// TODO: 
// - nowe funcjonalnoœci zgonde z prac¹ -> W TRAKCIE
// - poprawiæ Dockera
// - wersjonowanie dodaæ moze - wtedy dopisac do rozdzailu 6 wersjonowanie api