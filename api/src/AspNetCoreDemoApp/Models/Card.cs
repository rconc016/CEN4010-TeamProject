using Google.Cloud.Firestore;


namespace AspNetCoreDemoApp.Models
{
    [FirestoreData]
    public class Card : IFirestoreDocumentModel
    {
        public string Id { get; set; }

        [FirestoreProperty(name: "cardNumber")]
        public string CardNumber { get; set; }

        [FirestoreProperty(name: "expirationDate")]
        public string ExpirationDate { get; set; }

        [FirestoreProperty(name: "cvc")]
        public string CVC { get; set; }

        [FirestoreProperty(name: "cardName")]
        public string CardName { get; set; }

    }
}