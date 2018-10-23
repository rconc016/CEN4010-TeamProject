using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreDemoApp.Models;
using AspNetCoreDemoApp.Services;
using AspNetCoreDemoApp.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreDemoApp.Controllers
{
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private ICartService cartService;

        public CartController (ICartService cartService)
        {
            this.cartService = cartService;
        }

        /// <summary>
        /// GET endpoint to retrieve a single user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>The user with the corresponding ID.</returns>
        [HttpGet("{userId}")]
        public ActionResult<Cart> Get(string userId)
        {
            Cart cart = cartService.FindById(userId);
            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        [HttpPut("{userId}")]
        public ActionResult Put(string userId, [FromBody]Cart cartData)
        {
            Cart cart = cartService.FindById(userId);
            if (cart == null)
            {
                cartService.Create(userId, cartData);
            }
            else
            {
                cartService.Update(userId, cartData);
            }

            return Ok();
        }
    }
}