using Google.Cloud.Firestore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace AspNetCoreDemoApp
{
    public class Program
    { 
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}