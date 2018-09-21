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

        public IQuery Where(string field, QueryOperator queryOperator, string value)
        {
            Query query;

            switch (queryOperator)
            {
                case QueryOperator.Equal:
                    query = collectionRef.WhereEqualTo(field, value);
                    break;

                case QueryOperator.LessThan:
                    query = collectionRef.WhereLessThan(field, value);
                    break;

                case QueryOperator.GreaterThan:
                    query = collectionRef.WhereGreaterThan(field, value);
                    break;

                case QueryOperator.LessThanOrEqualTo:
                    query = collectionRef.WhereLessThanOrEqualTo(field, value);
                    break;

                case QueryOperator.GreaterThanOrEqualTo:
                    query = collectionRef.WhereGreaterThanOrEqualTo(field, value);
                    break;

                default:
                    throw new System.ArgumentException($"Query operator not found", queryOperator.ToString());
            }

            return new FirestoreQuery(query);
        }

        public IQuery OrderBy(string field, SortDirection sortDirection)
        {
            Query query;

            switch (sortDirection)
            {
                case SortDirection.Asc:
                    query = collectionRef.OrderBy(field);
                    break;

                case SortDirection.Desc:
                    query = collectionRef.OrderByDescending(field);
                    break;

                default:
                    throw new System.ArgumentException($"Sort direction not found", sortDirection.ToString());
            }

            return new FirestoreQuery(query);
        }
    }
}