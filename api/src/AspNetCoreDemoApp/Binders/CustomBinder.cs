using System;
using System.Reflection;
using System.Threading.Tasks;
using AspNetCoreDemoApp.Models;
using AspNetCoreDemoApp.Utils;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspNetCoreDemoApp.Binders
{
    public abstract class CustomBinder : IModelBinder, ICustomBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));
  
            object obj = GetModel();
            PropertyInfo[] properties = obj.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                ValueProviderResult result = bindingContext.ValueProvider.GetValue(property.Name.ToLower());
                if (result.FirstValue != null)
                {
                    if (property.PropertyType == typeof(DateTime))
                    {
                        DateTime date = new DateTime();
                        DateTime.TryParse((string) result.FirstValue, out date);
                        property.SetValue(obj, date.ToUniversalTime());
                    }

                    else if (property.PropertyType == typeof(SortDirection))
                    {
                        object direction = SortDirection.Asc;
                        Enum.TryParse(typeof(SortDirection), (string) result.FirstValue, true, out direction);
                        property.SetValue(obj, direction);
                    }

                    else
                    {
                        property.SetValue(obj, result.FirstValue);
                    }
                }            
            }
  
            bindingContext.Result = ModelBindingResult.Success(obj);
            return Task.CompletedTask;
        }

        public abstract object GetModel();
    }
}