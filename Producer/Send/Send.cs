using System;
using RabbitMQ.Client;
using System.Text;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Producer
{
    public class Send
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
    }
}
