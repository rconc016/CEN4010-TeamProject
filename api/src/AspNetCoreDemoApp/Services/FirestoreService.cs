using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreDemoApp.Models;
using AspNetCoreDemoApp.Utils;
using AspNetCoreDemoApp.Wrappers;
using Google.Cloud.Firestore;

namespace AspNetCoreDemoApp.Services
{
    public class FirestoreService : IFirestoreService
    {
        private IFirestoreDb firestoreDb;

        public FirestoreService(IFirestoreDb firestoreDb)
        {
            this.firestoreDb = firestoreDb;
            this.firestoreDb.Create(FirestoreConfig.ProjectId);
        }

        public void Create(string collectionId, object documentData, SetOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            firestoreDb.Collection(collectionId).Document().SetAsync(documentData, options, cancellationToken).Wait();
        }

        public void CreateWithId(string collectionId, string documentId, object documentData, SetOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            firestoreDb.Collection(collectionId).Document(documentId).SetAsync(documentData, options, cancellationToken).Wait();
        }

        public void Update(string collectionId, string documentId, object documentData, SetOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            firestoreDb.Collection(collectionId).Document(documentId).SetAsync(documentData, options, cancellationToken).Wait();
        }

        public IList<DocumentModel> FindAll<DocumentModel>(string collectionId) where DocumentModel : class, IFirestoreDocumentModel
        {
            return firestoreDb.Collection(collectionId).GetSnapshotAsync<DocumentModel>();
        }

        public DocumentModel FindById<DocumentModel>(string collectionId, string documentId) where DocumentModel : class, IFirestoreDocumentModel
        {
            return firestoreDb.Collection(collectionId).Document(documentId).GetSnapshotAsync<DocumentModel>();
        }

        public IQuery Where(string collectionId, string field, QueryOperator queryOperator, object value)
        {
            return firestoreDb.Collection(collectionId).Where(field, queryOperator, value);
        }

        public IQuery OrderBy(string collectionId, string field, SortDirection sortDirection)
        {
            return firestoreDb.Collection(collectionId).OrderBy(field, sortDirection);
        }
    }
}