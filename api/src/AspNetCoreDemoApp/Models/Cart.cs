using System;
using Google.Cloud.Firestore;
using AspNetCoreDemoApp.Models;
using AspNetCoreDemoApp.Controllers;
using AspNetCoreDemoApp.Services;
using System.Collections.Generic;

namespace AspNetCoreDemoApp.Models
{
    [FirestoreData]
    public class Cart : IFirestoreDocumentModel
    {
        public string Id { get; set; }

        [FirestoreProperty(name: "cart")]
        public List<Book> Products { get; set; }
                
    }
}