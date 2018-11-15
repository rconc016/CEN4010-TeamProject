using System.Collections.Generic;
using System.Threading;
using AspNetCoreDemoApp.Models;
using Google.Cloud.Firestore;

namespace AspNetCoreDemoApp.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Creates an existing user in the storage service.
        /// </summary>
        /// <param name="userId">The ID of the user being updated.</param>
        /// <param name="userData">The data to be stored.</param>
        void Create(string userId, User userData);

        /// <summary>
        /// Updates an existing user in the storage service.
        /// </summary>
        /// <param name="userId">The ID of the user being updated.</param>
        /// <param name="userData">The data to be stored.</param>
        void Update(string userId, User userData);

        /// <summary>
        /// Finds all the users.
        /// </summary>
        IList<User> FindAll();

        /// <summary>
        /// Finds the user with the given ID.
        /// </summary>
        /// <param name="userId">The ID of the user to look for.</param>
        User FindById(string userId);

        /// <summary>
        /// Determines whether the given password is valid or not.
        /// </summary>
        /// <param name="password">The password string to be validated.</param>
        /// <returns>True if the password is valid, false otherwise.</returns>
        bool IsPasswordValid(string password);
    }
}
