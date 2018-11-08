using System;
using Google.Cloud.Firestore;

namespace AspNetCoreDemoApp.Models
{
    [FirestoreData]
    public class User : IFirestoreDocumentModel
    {
        public string Id { get; set; }

        [FirestoreProperty(name: "name")]
        public string Name { get; set; }

        [FirestoreProperty(name: "provider")]
        public string Provider { get; set; }

        [FirestoreProperty(name: "image")]
        public string ImageUrl { get; set; }

        [FirestoreProperty(name: "firstName")]
        public string FirstName { get; set; }

        [FirestoreProperty(name: "lastName")]
        public string LastName { get; set; }

        [FirestoreProperty(name: "nickname")]
        public string Nickname { get; set; }

        [FirestoreProperty(name: "billingAddress")]
        public string BillingAddress { get; set; }

        [FirestoreProperty(name: "shippingAddress")]
        public string[] ShippingAddress { get; set; }

        [FirestoreProperty(name: "email")]
        public string Email { get; set; }

        [FirestoreProperty(name: "creditCards")]
        public string[] CreditCards { get; set; }
    }
}
