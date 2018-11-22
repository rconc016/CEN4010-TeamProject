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
        /// <param name="sortCommand">The key and order to sort by.</param>
        /// <param name="filterCommands">The list of keys and values to filter by.</param>
        /// <returns></returns>
        IList<Book> FindAll(SortCommand sortCommand = null, IList<FilterCommand> filterCommands = null, PageCommand pageCommand = null);

        /// <summary>
        /// Finds the book with the given ID.
        /// </summary>
        /// <param name="bookId">The ID of the book to look for.</param>
        Book FindById(string bookId);

        /// <summary>
        /// Finds the book description with the given ID.
        /// </summary>
        /// <param name="descriptionId">The ID of the description to look for.</param>
        BookDescription FindDescriptionById(string descriptionId);

        /// <summary>
        /// Converts the given book filter command to a generic list of filter commands.
        /// </summary>
        /// <param name="command">The book specific filter command.</param>
        /// <returns></returns>
        IList<FilterCommand> GetFilterCommands(BookFilterCommand command);
    }
}