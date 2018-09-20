namespace AspNetCoreDemoApp.Models
{
    public interface IFirestoreDocumentModel
    {
        /// <summary>
        /// Firestore document ID.
        /// </summary>
        /// <value></value>
        string Id { get; set; }
    }
}