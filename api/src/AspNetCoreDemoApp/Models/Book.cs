using System;
using Google.Cloud.Firestore;

namespace AspNetCoreDemoApp.Models
{
    [FirestoreData]
    public class Book : IFirestoreDocumentModel
    {
        public string Id { get; set; }

        [FirestoreProperty(name: "title")]
        public string Title { get; set; }

        [FirestoreProperty(name: "author")]
        public string Author { get; set; }

        [FirestoreProperty(name: "price")]
        public string Price { get; set; }

        [FirestoreProperty(name: "rating")]
        public string Rating { get; set; }

        [FirestoreProperty(name: "releaseDate")]
        public DateTime ReleaseDate { get; set; }
    }
}