using AspNetCoreDemoApp.Models;

namespace AspNetCoreDemoApp.Binders
{
    public class SortCommandBinder : CustomBinder
    {
        public override object GetModel()
        {
            return new SortCommand();
        }
    }
}