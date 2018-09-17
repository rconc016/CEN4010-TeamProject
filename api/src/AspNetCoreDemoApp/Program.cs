using Google.Cloud.Firestore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace AspNetCoreDemoApp
{
    public class Program
    {
        public static void InitializeProjectId(string project)
        {
            FirestoreDb db = FirestoreDb.Create(project);
            Console.WriteLine("Created Cloud Firestore client with project ID: {0}", project);
        }
        
           
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run();
            InitializeProjectId("tutorial-959a2");
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}