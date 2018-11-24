using System.Collections.Generic;
using AspNetCoreDemoApp.Models;
using AspNetCoreDemoApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreDemoApp.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// GET endpoint to retrieve a single user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>The user with the corresponding ID.</returns>
        [HttpGet("{userId}")]
        public ActionResult<User> Get(string userId)
        {
            User user = userService.FindById(userId);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        /// <summary>
        /// PUT endpoint to create or update an existing user.
        /// </summary>
        /// <param name="userId">The ID of ther user being created or updated.</param>
        /// <param name="userData">The user data to use.</param>
        /// <returns>200 OK.</returns>
        [HttpPut("{userId}")]
        public ActionResult Put(string userId, [FromBody]User userData)
        {
            User user = userService.FindById(userId);
            if (user == null)
            {
                userService.Create(userId, userData);
            }
            else
            {
                userService.Update(userId, userData);
            }

            return Ok();
        }

        /// <summary>
        /// GET endpoint to verify if a password is valid.
        /// </summary>
        /// <param name="password">The password to be validated.</param>
        /// <returns>200 OK Status Code. The response body will contain
        /// true if the password is valid, or false otherwise.</returns>
        [HttpGet("validate/password")]
        public ActionResult<bool> GetValidPassword([FromQuery] string password)
        {
            return userService.IsPasswordValid(password);
        }
    }
}
