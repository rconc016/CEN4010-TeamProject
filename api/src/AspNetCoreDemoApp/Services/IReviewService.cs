using AspNetCoreDemoApp.Models;
using System.Collections.Generic;

namespace AspNetCoreDemoApp.Services
{
    public interface IReviewService
    {
        /// <summary>
        /// Creates an existing review in the storage service.
        /// </summary>
        /// <param name="reviewData">The data to be stored.</param>
        void Create(Review reviewData);

        /// <summary>
        /// Finds all the reviews associated with the given book ID.
        /// </summary>
        /// <param name="bookId">The ID of the book to look for.</param>
        IList<Review> FindAllByBookId(string bookId);
    }
}
