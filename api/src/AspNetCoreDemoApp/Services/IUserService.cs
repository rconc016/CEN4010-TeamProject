using System.Collections.Generic;
using System.Threading;
using AspNetCoreDemoApp.Models;
using Google.Cloud.Firestore;

namespace AspNetCoreDemoApp.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Updates an existing user in the storage service.
        /// </summary>
        /// <param name="userId">The ID of the user being updated.</param>
        /// <param name="userData">The data to be stored.</param>
        /// <param name="options">Optional set of options to be used when creating the document.</param>
        /// <param name="cancellationToken">Optional task cancellation token.</param>
        void Update(string userId, object userData, SetOptions options = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Finds all the users.
        /// </summary>
        IList<User> FindAll();

        /// <summary>
        /// Finds the user with the given ID.
        /// </summary>
        /// <param name="userId">The ID of the user to look for.</param>
        User FindById(string userId);
    }
}
