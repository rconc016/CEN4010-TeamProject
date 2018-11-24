using System;
using AspNetCoreDemoApp.Binders;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreDemoApp.Models
{
    [FirestoreData]
    public class Book : IFirestoreDocumentModel
    {
        private const string Zero = "0";

        public string Id { get; set; }

        [FirestoreProperty(name: "title")]
        public string Title { get; set; }

        [FirestoreProperty(name: "authorId")]
        public string AuthorId { get; set; }

        [FirestoreProperty(name: "descriptionId")]
        public string DescriptionId { get; set; }

        [FirestoreProperty(name: "author")]
        public string Author { get; set; }

        [FirestoreProperty(name: "price")]
        public string Price { get; set; } = Zero;

        [FirestoreProperty(name: "publisher")]
        public string Publisher { get; set; }

        [FirestoreProperty(name: "rating")]
        public string Rating { get; set; } = Zero;

        [FirestoreProperty(name: "ratingsCount")]
        public string RatingsCount { get; set; } = Zero;

        [FirestoreProperty(name: "releaseDate")]
        public DateTime ReleaseDate { get; set; }

        [FirestoreProperty(name: "genre")]
        public string Genre { get; set; }

        [FirestoreProperty(name: "topSeller")]
        public bool TopSeller { get; set; }

        [FirestoreProperty(name: "imageUrl")]
        public string ImageUrl { get; set; }
    }
}