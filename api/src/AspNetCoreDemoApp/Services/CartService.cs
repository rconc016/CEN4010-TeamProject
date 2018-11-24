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
    public class CartService : ICartService
    {
        private const string CollectionId = "cart";

        public IFirestoreService firestoreService;

        public CartService(IFirestoreService firestoreService)
        {
            this.firestoreService = firestoreService;
        }

        public void Create(string userId, Cart cartData)
        {
            firestoreService.CreateWithId(CollectionId, userId, cartData);
        }

        public void Update(string userId, Cart cartData)
        {
            firestoreService.Update(CollectionId, userId, cartData);
        }

        public IList<Cart> FindAll()
        {
            return firestoreService.FindAll<Cart>(CollectionId);
        }

        public Cart FindById(string userId)
        {
            return firestoreService.FindById<Cart>(CollectionId, userId);
        }
    }
}