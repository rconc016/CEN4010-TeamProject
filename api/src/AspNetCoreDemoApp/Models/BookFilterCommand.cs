using System;
using System.Collections.Generic;
using AspNetCoreDemoApp.Binders;
using AspNetCoreDemoApp.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreDemoApp.Models
{
    [ModelBinder(BinderType = typeof(BookFilterCommandBinder))]
    public class BookFilterCommand
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string MinPrice { get; set; }

        public string MaxPrice { get; set; }

        public string Rating { get; set; }

        public DateTime MinReleaseDate { get; set; }

        public DateTime MaxReleaseDate { get; set; }

        public string Genre { get; set; }

        public bool TopSeller { get; set; }
    }
}