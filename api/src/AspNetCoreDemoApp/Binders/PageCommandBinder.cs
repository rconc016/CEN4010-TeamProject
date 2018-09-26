using AspNetCoreDemoApp.Models;

namespace AspNetCoreDemoApp.Binders
{
    public class PageCommandBinder : CustomBinder
    {
        public override object GetModel()
        {
            return new PageCommand();
        }
    }
}