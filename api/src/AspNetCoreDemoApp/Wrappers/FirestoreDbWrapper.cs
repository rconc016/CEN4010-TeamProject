using AspNetCoreDemoApp.Utils;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1Beta1;

namespace AspNetCoreDemoApp.Wrappers
{
    public class FirestoreDbWrapper : IFirestoreDb
    {
        private FirestoreDb firestoreDb;

        public IFirestoreDb Create(string projectId = null, string databaseId = null, FirestoreClient client = null)
        {
            firestoreDb = FirestoreDb.Create(FirestoreConfig.ProjectId);
            return this;
        }

        public ICollectionReference Collection(string path)
        {
            return new CollectionReferenceWrapper(firestoreDb.Collection(path));
        }
    }
}