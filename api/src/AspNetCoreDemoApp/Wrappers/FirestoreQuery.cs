using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreDemoApp.Models;
using AspNetCoreDemoApp.Services;
using AspNetCoreDemoApp.Utils;
using Google.Cloud.Firestore;

namespace AspNetCoreDemoApp.Wrappers
{
    public class FirestoreQuery : IQuery
    {
        private CollectionReference collection;
        
        private IList<FilterCommand> filterCommands;
        
        private IList<Query> queries;

        private PageCommand pageCommand;

        public FirestoreQuery(CollectionReference collection)
        {
            this.collection = collection;
            filterCommands = new List<FilterCommand>();
            queries = new List<Query>();
        }

        public IQuery Where(string field, QueryOperator queryOperator, object value)
        {
            switch (queryOperator)
            {
                case QueryOperator.Equal:
                    queries.Add(collection.WhereEqualTo(field, value));
                    break;

                case QueryOperator.LessThan:
                    queries.Add(collection.WhereLessThan(field, value));
                    break;

                case QueryOperator.GreaterThan:
                    queries.Add(collection.WhereGreaterThan(field, value));
                    break;

                case QueryOperator.LessThanOrEqualTo:
                    queries.Add(collection.WhereLessThanOrEqualTo(field, value));
                    break;

                case QueryOperator.GreaterThanOrEqualTo:
                    queries.Add(collection.WhereGreaterThanOrEqualTo(field, value));
                    break;

                case QueryOperator.Contains:
                    // Because Firestore does not support this query operation
                    // we need to include this sorting query to retrieve the list
                    // of values to be filtered
                    OrderBy(field, SortDirection.Asc);

                    filterCommands.Add(new FilterCommand
                    {
                        FilterKey = field,
                        Operator = queryOperator,
                        FilterValue = value
                    });
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
                    queries.Add(collection.OrderBy(field));
                    break;

                case SortDirection.Desc:
                    queries.Add(collection.OrderByDescending(field));
                    break;

                default:
                    throw new System.ArgumentException($"Sort direction not found", sortDirection.ToString());
            }

            return this;
        }

        public IQuery Limit(int limit)
        {
            if (pageCommand == null)
            {
                pageCommand = new PageCommand();
            }

            pageCommand.Limit = limit;
            return this;
        }

        public IQuery Offset(int offset)
        {
            if (pageCommand == null)
            {
                pageCommand = new PageCommand();
            }

            pageCommand.Offset = offset;
            return this;
        }

        public IList<DocumentModel> Execute<DocumentModel>() where DocumentModel : class, IFirestoreDocumentModel
        {
            IList<DocumentModel> items = new List<DocumentModel>();
            int documentCounter = 0;
            int documentOffsetCounter = 0;

            IList<DocumentSnapshot> result = ExecuteQueries();
            foreach(DocumentSnapshot documentSnapshot in result)
            {
                if (ShouldInclude(documentSnapshot))
                {
                    if (pageCommand == null)
                    {
                        AddConvertedDocument(items, documentSnapshot);
                    }

                    else if (pageCommand.Offset <= 0 || ++documentOffsetCounter > pageCommand.Offset)
                    {
                        AddConvertedDocument(items, documentSnapshot);

                        if (pageCommand.Limit > 0 && ++documentCounter > pageCommand.Limit - 1)
                        {
                            break;
                        }
                    }
                }
            }

            return items;
        }

        private IList<DocumentSnapshot> ExecuteQueries()
        {
            IList<DocumentSnapshot> result = null;

            // Firestore does not currently support performing multiple
            // inequality filters on different properties for the same query
            // As a workaround, we execute each query separately and then
            // intersect the results
            foreach (Query query in queries)
            {
                QuerySnapshot querySnapshot = query.GetSnapshotAsync().Result;
                IList<DocumentSnapshot> documentSnapshots = new List<DocumentSnapshot>(querySnapshot.Documents);

                if (result == null)
                {
                    result = documentSnapshots;
                }
                else
                {
                    result = ListUtils.IntersectWith(result, documentSnapshots);
                }
            }

            if (result == null)
            {
                result = new List<DocumentSnapshot>(collection.GetSnapshotAsync().Result.Documents);
            }

            return result;
        }

        /// <summary>
        /// Decides if the given snapshot should be including in
        /// the query's final result based on the "Contains" filters.
        /// Note: Firestore queries do not currently support this
        /// type of filtering.
        /// </summary>
        /// <param name="snapshot"></param>
        /// <returns></returns>
        private bool ShouldInclude(DocumentSnapshot snapshot)
        {
            foreach (FilterCommand command in filterCommands)
            {
                string snapshotValue = snapshot.GetValue<string>(command.FilterKey).Trim().ToLower();
                string filterValue = ((string) command.FilterValue).Trim().ToLower();

                if (!snapshotValue.Contains(filterValue))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Converts the given document and adds it
        /// to the given list of items.
        /// </summary>
        /// <typeparam name="DocumentModel"></typeparam>
        /// <param name="items">The list of items to append to.</param>
        /// /// <param name="snapshot">The document to convert and append.</param>
        private void AddConvertedDocument<DocumentModel>(IList<DocumentModel> items, DocumentSnapshot snapshot) where DocumentModel : class, IFirestoreDocumentModel
        {
            DocumentModel item = snapshot.ConvertTo<DocumentModel>();
            item.Id = snapshot.Id;
            items.Add(item);
        }
    }
}