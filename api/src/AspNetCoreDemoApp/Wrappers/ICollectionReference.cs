using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreDemoApp.Models;
using Google.Cloud.Firestore;

namespace AspNetCoreDemoApp.Wrappers
{
    public interface ICollectionReference
    {
        /// <summary>
        /// Creates a reference to a document with the given ID
        /// which exists inside this collection.
        /// </summary>
        /// <param name="documentId">The ID of the document to use.</param>
        /// <returns>The document's reference object.</returns>
        IDocumentReference Document(string documentId = null);

        /// <summary>
        /// Gets a list of all documents inside this collection.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>A query snapshot containing the collection's documents.</returns>
        Task<QuerySnapshot> GetSnapshotAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets a list of all documents inside this collection
        /// and converts them to their respective models.
        /// </summary>
        /// <typeparam name="DocumentModel">Object which must implement <see cref="IFirestoreDocumentModel" /></typeparam>
        IList<DocumentModel> GetSnapshotAsync<DocumentModel>() where DocumentModel : class, IFirestoreDocumentModel;
    }
}