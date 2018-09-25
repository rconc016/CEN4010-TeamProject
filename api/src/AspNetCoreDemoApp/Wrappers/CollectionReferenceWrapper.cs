using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreDemoApp.Models;
using AspNetCoreDemoApp.Utils;
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

        public IList<DocumentModel> GetSnapshotAsync<DocumentModel>() where DocumentModel : class, IFirestoreDocumentModel
        {
            IList<DocumentModel> items = new List<DocumentModel>();

            QuerySnapshot snapshot = GetSnapshotAsync().Result;
            foreach(DocumentSnapshot documentSnapshot in snapshot.Documents)
            {
                DocumentModel item = documentSnapshot.ConvertTo<DocumentModel>();
                item.Id = documentSnapshot.Id;
                items.Add(item);
            }

            return items;
        }

        public IQuery Where(string field, QueryOperator queryOperator, object value)
        {
            return new FirestoreQuery(collectionRef).Where(field, queryOperator, value);
        }

        public IQuery OrderBy(string field, SortDirection sortDirection)
        {
            return new FirestoreQuery(collectionRef).OrderBy(field, sortDirection);
        }
    }
}