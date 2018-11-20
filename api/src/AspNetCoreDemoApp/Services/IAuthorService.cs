using System.Collections.Generic;
using AspNetCoreDemoApp.Models;

namespace AspNetCoreDemoApp.Services
{
    public interface IAuthorService
    {
        /// <summary>
        /// Finds the author with the given ID.
        /// </summary>
        /// <param name="authorId">The ID of the author to look for.</param>
        Author FindById(string authorId);
    }
}