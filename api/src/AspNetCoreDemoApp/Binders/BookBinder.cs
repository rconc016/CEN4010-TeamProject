using AspNetCoreDemoApp.Models;

namespace AspNetCoreDemoApp.Binders
{
    public class BookBinder : CustomBinder
    {
        public override object GetModel()
        {
            return new Book();
        }
    }
}