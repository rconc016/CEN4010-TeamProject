using System;
using Google.Cloud.Firestore;

namespace AspNetCoreDemoApp.Models
{
    [FirestoreData]
    public class Review : IFirestoreDocumentModel
    {
        public string Id { get; set; }

        [FirestoreProperty(name: "bookId")]
        public string BookId { get; set; }

        [FirestoreProperty(name: "userId")]
        public string UserId { get; set; }

        [FirestoreProperty(name: "date")]
        public DateTime Date { get; set; }

        [FirestoreProperty(name: "comment")]
        public string Comment { get; set; }

        [FirestoreProperty(name: "anonymous")]
        public bool Anonymous { get; set; }

        [FirestoreProperty(name: "useNickname")]
        public bool UseNickname { get; set; }
    }
}
