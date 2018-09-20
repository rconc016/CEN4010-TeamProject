using System.Threading;
using System.Threading.Tasks;
using AspNetCoreDemoApp.Models;
using Google.Cloud.Firestore;

namespace AspNetCoreDemoApp.Wrappers
{
    public class DocumentReferenceWrapper : IDocumentReference
    {
        private DocumentReference documentRef;

        public DocumentReferenceWrapper(DocumentReference documentRef)
        {
            this.documentRef = documentRef;
        }

        public Task<WriteResult> SetAsync(object documentData, SetOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return documentRef.SetAsync(documentData, options, cancellationToken);
        }

        public Task<DocumentSnapshot> GetSnapshotAsync()
        {
            return documentRef.GetSnapshotAsync();
        }

        public T GetSnapshotAsync<T>() where T : class, IFirestoreDocumentModel
        {
            DocumentSnapshot documentSnapshot = GetSnapshotAsync().Result;
            if (documentSnapshot.Exists)
            {
                T item = documentSnapshot.ConvertTo<T>();
                item.Id = documentSnapshot.Id;
                return item;
            }

            return null;
        }
    }
}