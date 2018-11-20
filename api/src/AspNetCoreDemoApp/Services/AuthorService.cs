using AspNetCoreDemoApp.Models;

namespace AspNetCoreDemoApp.Services
{
    public class AuthorService : IAuthorService
    {
        private const string CollectionId = "author";

		private IFirestoreService firestoreService;

		public AuthorService(IFirestoreService firestoreService)
		{
			this.firestoreService = firestoreService;
		}

        public Author FindById(string authorId)
        {
            return firestoreService.FindById<Author>(CollectionId, authorId);
        }
    }
}