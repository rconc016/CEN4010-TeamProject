using System.Threading;
using System.Threading.Tasks;
using AspNetCoreDemoApp.Models;
using Google.Cloud.Firestore;

namespace AspNetCoreDemoApp.Wrappers
{
    public interface IDocumentReference
    {
        /// <summary>
        /// Updates the document with the given data.
        /// The document and its parent collection are
        /// created if they don't already exist.
        /// </summary>
        /// <param name="documentData">The data to store in this document.</param>
        /// <param name="options">The options used to create or update this document.</param>
        /// <param name="cancellationToken">The task cancellation token.</param>
        /// <returns>The result of the operation.</returns>
        Task<WriteResult> SetAsync(object documentData, SetOptions options = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets a reference to this document's data.
        /// </summary>
        /// <returns></returns>
        Task<DocumentSnapshot> GetSnapshotAsync();

        /// <summary>
        /// Gets a reference to this document's data
        /// and converts it to its respective model.
        /// </summary>
        /// <typeparam name="DocumentModel">Object which must implement <see cref="IFirestoreDocumentModel" /></typeparam>
        DocumentModel GetSnapshotAsync<DocumentModel>() where DocumentModel : class, IFirestoreDocumentModel;
    }
}