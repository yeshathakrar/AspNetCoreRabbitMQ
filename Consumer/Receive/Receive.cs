using System;
using System.Text;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Consumer
{
    public class Receive
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
    }
}
