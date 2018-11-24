using Google.Cloud.Firestore;

namespace AspNetCoreDemoApp.Models
{
    [FirestoreData]
    public class BookDescription : IFirestoreDocumentModel
    {
        public string Id { get; set; }

        [FirestoreProperty(name: "description")]
        public string Description { get; set; }
    }
}