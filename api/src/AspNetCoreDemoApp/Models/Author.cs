using Google.Cloud.Firestore;

namespace AspNetCoreDemoApp.Models
{
    [FirestoreData]
    public class Author : IFirestoreDocumentModel
    {
        public string Id { get; set; }

        [FirestoreProperty(name: "firstName")]
        public string FirstName { get; set; }

        [FirestoreProperty(name: "lastName")]
        public string LastName { get; set; }

        [FirestoreProperty(name: "bio")]
        public string Bio { get; set; }

        [FirestoreProperty(name: "imageUrl")]
        public string ImageUrl { get; set; }
    }
}