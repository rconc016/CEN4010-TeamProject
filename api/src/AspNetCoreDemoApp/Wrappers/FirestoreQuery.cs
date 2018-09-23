using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreDemoApp.Models;
using AspNetCoreDemoApp.Utils;
using Google.Cloud.Firestore;

namespace AspNetCoreDemoApp.Wrappers
{
    public class FirestoreQuery : IQuery
    {
        public Query query;

        public FirestoreQuery(Query query)
        {
            this.query = query;
        }

        public IQuery Where(string field, QueryOperator queryOperator, string value)
        {
            switch (queryOperator)
            {
                case QueryOperator.Equal:
                    query = query.WhereEqualTo(field, value);
                    break;

                case QueryOperator.LessThan:
                    query = query.WhereLessThan(field, value);
                    break;

                case QueryOperator.GreaterThan:
                    query = query.WhereGreaterThan(field, value);
                    break;

                case QueryOperator.LessThanOrEqualTo:
                    query = query.WhereLessThanOrEqualTo(field, value);
                    break;

                case QueryOperator.GreaterThanOrEqualTo:
                    query = query.WhereGreaterThanOrEqualTo(field, value);
                    break;

                default:
                    throw new System.ArgumentException($"Query operator not found", queryOperator.ToString());
            }

            return this;
        }

        public IQuery OrderBy(string field, SortDirection sortDirection)
        {
            switch (sortDirection)
            {
                case SortDirection.Asc:
                    query = query.OrderBy(field);
                    break;

                case SortDirection.Desc:
                    query = query.OrderByDescending(field);
                    break;

                default:
                    throw new System.ArgumentException($"Sort direction not found", sortDirection.ToString());
            }

            return this;
        }

        public IList<DocumentModel> Execute<DocumentModel>() where DocumentModel : class, IFirestoreDocumentModel
        {
            IList<DocumentModel> items = new List<DocumentModel>();

            QuerySnapshot snapshot = query.GetSnapshotAsync().Result;
            foreach(DocumentSnapshot documentSnapshot in snapshot.Documents)
            {
                DocumentModel item = documentSnapshot.ConvertTo<DocumentModel>();
                item.Id = documentSnapshot.Id;
                items.Add(item);
            }

            return items;
        }
    }
}