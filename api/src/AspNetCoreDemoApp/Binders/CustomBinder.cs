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
                        if (DateTime.TryParse((string) result.FirstValue, out date))
                        {
                            property.SetValue(obj, date.ToUniversalTime());
                        }
                    }

                    else if (property.PropertyType == typeof(SortDirection))
                    {
                        object direction = SortDirection.Asc;
                        if (Enum.TryParse(typeof(SortDirection), (string) result.FirstValue, true, out direction))
                        {
                            property.SetValue(obj, direction);
                        }
                    }

                    else if (property.PropertyType == typeof(Int32))
                    {
                        int value = 0;
                        if (Int32.TryParse(result.FirstValue, out value))
                        {
                            property.SetValue(obj, value);
                        }
                    }

                    else if (property.PropertyType == typeof(Boolean))
                    {
                        bool value = false;
                        if (Boolean.TryParse(result.FirstValue, out value))
                        {
                            property.SetValue(obj, value);
                        }
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