using System.Collections.Generic;
using AspNetCoreDemoApp.Models;

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
        /// Finds the book with the given ID.
        /// </summary>
        /// <param name="bookId">The ID of the book to look for.</param>
        Book FindById(string bookId);
    }
}