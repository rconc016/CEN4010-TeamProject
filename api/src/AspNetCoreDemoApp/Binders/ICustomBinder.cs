using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspNetCoreDemoApp.Binders
{
    public interface ICustomBinder
    {
        /// <summary>
        /// Retrieves the object the model object that needs to be binded.
        /// It is important that the object implements its fields as
        /// properties in order for reflection to properly work.
        /// </summary>
        /// <returns>The model object to be binded.</returns>
        object GetModel();
    }
}