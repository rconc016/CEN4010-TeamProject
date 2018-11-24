using System.Collections.Generic;
using System.Threading;
using AspNetCoreDemoApp.Models;
using Google.Cloud.Firestore;

namespace AspNetCoreDemoApp.Services
{
    public interface ICartService
    {
        /// <summary>
        /// Creates an existing user in the storage service.
        /// </summary>
        /// <param name="userId">The ID of the user being updated.</param>
        /// <param name="cartData">The data to be stored.</param>
        void Create(string userId, Cart cartData);

        /// <summary>
        /// Updates an existing user in the storage service.
        /// </summary>
        /// <param name="userId">The ID of the user being updated.</param>
        /// <param name="cartData">The data to be stored.</param>
        void Update(string userId, Cart cartData);

        /// <summary>
        /// Finds all the users.
        /// </summary>
        IList<Cart> FindAll();

        /// <summary>
        /// Finds the user with the given ID.
        /// </summary>
        /// <param name="userId">The ID of the user to look for.</param>
        Cart FindById(string userId);
    }
}
