using System.Collections.Generic;
using AspNetCoreDemoApp.Models;
using AspNetCoreDemoApp.Wrappers;

namespace AspNetCoreDemoApp.Services
{
    public class ReviewService : IReviewService
    {
        private const string CollectionId = "review";

        public IFirestoreService firestoreService;

        public ReviewService(IFirestoreService firestoreService)
        {
            this.firestoreService = firestoreService;
        }

        public void Create(Review reviewData)
        {
            firestoreService.Create(CollectionId, reviewData);
        }

        public IList<Review> FindAllByBookId(string bookId)
        {
            IQuery query = firestoreService.OrderBy(CollectionId, "date", Utils.SortDirection.Desc)
                .Where("bookId", Utils.QueryOperator.Equal, bookId);

            return query.Execute<Review>();
        }
    }
}
