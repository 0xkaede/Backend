using KaedeBackend.Utils;

namespace KaedeBackend.Exceptions
{
    public class Program
    {
        public static void Main(string[] args)
            => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://*:4010");
                    webBuilder.UseStartup<Startup>();

                    Logger.Log($"Backend listening on port 4010");
                    Logger.Log($"Backend Created by kaede");
                });
    }
}