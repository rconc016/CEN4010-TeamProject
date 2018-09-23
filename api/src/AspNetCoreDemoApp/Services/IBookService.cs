using System.Collections.Generic;
using AspNetCoreDemoApp.Models;
using AspNetCoreDemoApp.Utils;

namespace AspNetCoreDemoApp.Services
{
    public interface IBookService
    {
        /// <summary>
        /// Updates an existing book in the storage service.
        /// </summary>
        /// <param name="bookId">The ID of the book being updated.</param>
        /// <param name="bookData">The data to be stored.</param>
        void Update(string bookId, Book bookData);

        /// <summary>
        /// Finds all the books.
        /// </summary>
        IList<Book> FindAll();

        /// <summary>
        /// Finds all the books sorted by the specified field.
        /// </summary>
        /// <param name="field">The field to sort the books on.</param>
        /// <param name="sortDirection">The sorting direction in ascending or descending order.</param>
        /// <returns></returns>
        IList<Book> FindAll(string field, SortDirection sortDirection);

        /// <summary>
        /// Finds the book with the given ID.
        /// </summary>
        /// <param name="bookId">The ID of the book to look for.</param>
        Book FindById(string bookId);
    }
}