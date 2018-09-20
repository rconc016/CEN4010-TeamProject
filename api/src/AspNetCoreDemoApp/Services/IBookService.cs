using System.Collections.Generic;
using System.Threading;
using AspNetCoreDemoApp.Models;
using Google.Cloud.Firestore;

namespace AspNetCoreDemoApp.Services
{
    public interface IBookService
    {
        /// <summary>
        /// Updates an existing book in the storage service.
        /// </summary>
        /// <param name="bookId">The ID of the book being updated.</param>
        /// <param name="bookData">The data to be stored.</param>
        /// <param name="options">Optional set of options to be used when creating the document.</param>
        /// <param name="cancellationToken">Optional task cancellation token.</param>
        void Update(string bookId, object bookData, SetOptions options = null, CancellationToken cancellationToken = default(CancellationToken));

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