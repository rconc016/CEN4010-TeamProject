using System.Collections.Generic;
using AspNetCoreDemoApp.Models;
using AspNetCoreDemoApp.Utils;

namespace AspNetCoreDemoApp.Services
{
    public class BookService : IBookService
    {
        private const string CollectionId = "book";

		private IFirestoreService firestoreService;

		public BookService(IFirestoreService firestoreService)
		{
			this.firestoreService = firestoreService;
		}

        public void Update(string bookId, Book bookData)
        {
            firestoreService.Update(CollectionId, bookId, bookData);
        }

        public IList<Book> FindAll()
        {
            return firestoreService.FindAll<Book>(CollectionId);
        }

        public IList<Book> FindAll(string field, SortDirection sortDirection)
        {
            return firestoreService.OrderBy(CollectionId, field, sortDirection).Execute<Book>();
        }

        public Book FindById(string bookId)
        {
            return firestoreService.FindById<Book>(CollectionId, bookId);
        }
    }
}