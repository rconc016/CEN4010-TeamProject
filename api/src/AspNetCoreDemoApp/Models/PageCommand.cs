using System.Collections.Generic;
using AspNetCoreDemoApp.Binders;
using AspNetCoreDemoApp.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreDemoApp.Models
{
    [ModelBinder(BinderType = typeof(PageCommandBinder))]
    public class PageCommand
    {
        public int Limit { get; set; }

        public int Offset { get; set; }
    }
}