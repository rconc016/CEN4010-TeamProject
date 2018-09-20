using System;
using System.Collections.Generic;
using System.Threading;
using AspNetCoreDemoApp.Models;
using AspNetCoreDemoApp.Services;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

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

        public void Update(string bookId, object bookData, SetOptions options, CancellationToken cancellationToken)
        {
            firestoreService.Update(CollectionId, bookId, bookData, options, cancellationToken);
        }

        public IList<Book> FindAll()
        {
            return firestoreService.FindAll<Book>(CollectionId);
        }

        public Book FindById(string bookId)
        {
            return firestoreService.FindById<Book>(CollectionId, bookId);
        }
    }
}