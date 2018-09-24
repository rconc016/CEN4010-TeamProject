using AspNetCoreDemoApp.Models;

namespace AspNetCoreDemoApp.Binders
{
    public class BookFilterCommandBinder : CustomBinder
    {
        public override object GetModel()
        {
            return new BookFilterCommand();
        }
    }
}