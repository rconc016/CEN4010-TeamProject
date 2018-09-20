using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreDemoApp.Models;
using Google.Cloud.Firestore;

namespace AspNetCoreDemoApp.Wrappers
{
    public class CollectionReferenceWrapper : ICollectionReference
    {
        private CollectionReference collectionRef;

        public CollectionReferenceWrapper(CollectionReference collectionRef)
        {
            this.collectionRef = collectionRef;
        }

        public IDocumentReference Document(string path = null)
        {
            DocumentReference documentRef = (path != null) ? collectionRef.Document(path) : collectionRef.Document();
            return new DocumentReferenceWrapper(documentRef);
        }

        public Task<QuerySnapshot> GetSnapshotAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return collectionRef.GetSnapshotAsync();
        }

        public IList<T> GetSnapshotAsync<T>() where T : class, IFirestoreDocumentModel
        {
            IList<T> items = new List<T>();

            QuerySnapshot snapshot = GetSnapshotAsync().Result;
            foreach(DocumentSnapshot documentSnapshot in snapshot.Documents)
            {
                T item = documentSnapshot.ConvertTo<T>();
                item.Id = documentSnapshot.Id;
                items.Add(item);
            }

            return items;
        }
    }
}