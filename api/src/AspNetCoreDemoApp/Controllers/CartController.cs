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
        /// <param name="cartId"></param>
        /// <returns>The user with the corresponding ID.</returns>
        [HttpGet("{cartId}")]
        public ActionResult<Cart> Get(string cartId)
        {
            Cart cart = cartService.FindById(cartId);
            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        [HttpPut("{cartId}")]
        public ActionResult Put(string cartId, [FromBody]Cart cartData)
        {
            Cart cart = cartService.FindById(cartId);
            if (cart == null)
            {
                cartService.Create(cartId, cartData);
            }
            else
            {
                cartService.Update(cartId, cartData);
            }

            return Ok();
        }
    }
}