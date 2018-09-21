using System.Collections.Generic;
using AspNetCoreDemoApp.Models;
using AspNetCoreDemoApp.Utils;

namespace AspNetCoreDemoApp.Wrappers
{
    public interface IQuery
    {
        /// <summary>
        /// Performs a selection query.
        /// </summary>
        /// <param name="field">The name of the field to select by.</param>
        /// <param name="queryOperator">The type of comparison to perform.</param>
        /// <param name="value">The value to compare the field to.</param>
        /// <returns>The query object ready to execute.</returns>
        IQuery Where(string field, QueryOperator queryOperator, string value);

        /// <summary>
        /// Performs a sorting operation.
        /// </summary>
        /// <param name="field">The name of the field to sort by.</param>
        /// <param name="sortDirection">The direction of the sorting operation.</param>
        /// <returns>The query object ready to execute.</returns>
        IQuery OrderBy(string field, SortDirection sortDirection);

        /// <summary>
        /// Executes the query and converts the result to
        /// the specified type.
        /// </summary>
        /// <typeparam name="DocumentModel">Object which must implement <see cref="IFirestoreDocumentModel" /></typeparam>
        IList<DocumentModel> Execute<DocumentModel>() where DocumentModel : class, IFirestoreDocumentModel;
    }
}