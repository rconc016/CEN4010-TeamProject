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

        public void Update(string collectionId, string documentId, object documentData, SetOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            firestoreDb.Collection(collectionId).Document(documentId).SetAsync(documentData, options, cancellationToken).Wait();
        }

        public IList<T> FindAll<T>(string collectionId) where T : class, IFirestoreDocumentModel
        {
            return firestoreDb.Collection(collectionId).GetSnapshotAsync<T>();
        }

        public T FindById<T>(string collectionId, string documentId) where T : class, IFirestoreDocumentModel
        {
            return firestoreDb.Collection(collectionId).Document(documentId).GetSnapshotAsync<T>();
        }
    }
}