using AspNetCoreDemoApp.Binders;
using AspNetCoreDemoApp.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreDemoApp.Models
{
    [ModelBinder(BinderType = typeof(SortCommandBinder))]
    public class SortCommand
    {
        public string SortKey { get; set; }

        public SortDirection SortBy { get; set; }
    }
}