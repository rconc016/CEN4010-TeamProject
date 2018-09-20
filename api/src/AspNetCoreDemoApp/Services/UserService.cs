using System;
using System.Collections.Generic;
using System.Threading;
using AspNetCoreDemoApp.Models;
using AspNetCoreDemoApp.Services;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreDemoApp.Services
{
    public class UserService : IUserService
    {
        private const string CollectionId = "user";

        public IFirestoreService firestoreService;

        public UserService(IFirestoreService firestoreService)
        {
            this.firestoreService = firestoreService;
        }

        public void Update(string userId, object userData, SetOptions options, CancellationToken cancellationToken)
        {
            firestoreService.Update(CollectionId, userId, userData, options, cancellationToken);
        }

        public IList<User> FindAll()
        {
            return firestoreService.FindAll<User>(CollectionId);
        }

        public User FindById(string userId)
        {
            return firestoreService.FindById<User>(CollectionId, userId);
        }
    }
}
