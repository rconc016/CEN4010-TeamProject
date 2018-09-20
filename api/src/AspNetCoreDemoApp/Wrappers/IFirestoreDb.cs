using Google.Cloud.Firestore.V1Beta1;

namespace AspNetCoreDemoApp.Wrappers
{
    public interface IFirestoreDb
    {
        /// <summary>
        /// Creates an instance of the FIrestore database.
        /// </summary>
        /// <param name="projectId">The ID of the project to connect to.</param>
        /// <param name="databaseId">The optional ID of the database to connect to.</param>
        /// <param name="client">The optional Firestore client to use.</param>
        /// <returns>The instance of the Firestore database.</returns>
        IFirestoreDb Create(string projectId = null, string databaseId = null, FirestoreClient client = null);

        /// <summary>
        /// Creates a reference to the given collection.
        /// </summary>
        /// <param name="collectionId">The ID of the collection to reference.</param>
        /// <returns>A reference to the database's collection.</returns>
        ICollectionReference Collection(string collectionId);
    }
}