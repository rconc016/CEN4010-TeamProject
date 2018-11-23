using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AspNetCoreDemoApp.Models;

namespace AspNetCoreDemoApp.Services
{
    public class UserService : IUserService
    {
        private const string CollectionId = "user";
        private const string PasswordPattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$";

        private IFirestoreService firestoreService;
        private Regex passwordRegex;

        public UserService(IFirestoreService firestoreService)
        {
            this.firestoreService = firestoreService;

            passwordRegex = new Regex(PasswordPattern);
        }

        public void Create(string userId, User userData)
        {
            firestoreService.CreateWithId(CollectionId, userId, userData);
        }

        public void Update(string userId, User userData)
        {
            firestoreService.Update(CollectionId, userId, userData);
        }

        public IList<User> FindAll()
        {
            return firestoreService.FindAll<User>(CollectionId);
        }

        public User FindById(string userId)
        {
            return firestoreService.FindById<User>(CollectionId, userId);
        }

        public bool IsPasswordValid(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            MatchCollection matches = passwordRegex.Matches(password);
            return matches.Count > 0;            
        }
    }
}
