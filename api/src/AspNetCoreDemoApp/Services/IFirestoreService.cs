using System.Collections.Generic;
using System.Threading;
using AspNetCoreDemoApp.Models;
using Google.Cloud.Firestore;

namespace AspNetCoreDemoApp.Services
{
    public interface IFirestoreService
    {
        /// <summary>
        /// Creates a new document in the Firestore database.
        /// </summary>
        /// <param name="collectionId">The ID of the parent collection.</param>
        /// <param name="documentData">The data to be stored.</param>
        /// <param name="options">Optional set of options to be used when creating the document.</param>
        /// <param name="cancellationToken">Optional task cancellation token.</param>
        void Create(string collectionId, object documentData, SetOptions options = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Updates an existing document in the Firestore database.
        /// </summary>
        /// <param name="collectionId">The ID of the parent collection.</param>
        /// <param name="documentId">The ID of the document being updated.</param>
        /// <param name="documentData">The data to be stored.</param>
        /// <param name="options">Optional set of options to be used when creating the document.</param>
        /// <param name="cancellationToken">Optional task cancellation token.</param>
        void Update(string collectionId, string documentId, object documentData, SetOptions options = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Finds all the documents inside the given collection.
        /// </summary>
        /// <typeparam name="DocumentModel">Object which must implement <see cref="IFirestoreDocumentModel" /></typeparam>
        /// <param name="collectionId">The ID of the collection to look for.</param>
        IList<DocumentModel> FindAll<DocumentModel>(string collectionId) where DocumentModel : class, IFirestoreDocumentModel;

        /// <summary>
        /// Finds the document with the given ID from the given collection.
        /// </summary>
        /// <typeparam name="DocumentModel">Object which must implement <see cref="IFirestoreDocumentModel" /></typeparam>
        /// <param name="collectionId">The ID of the collection to look into.</param>
        /// <param name="documentId">The ID of the document to look for.</param>
        DocumentModel FindById<DocumentModel>(string collectionId, string documentId) where DocumentModel : class, IFirestoreDocumentModel;
    }
}