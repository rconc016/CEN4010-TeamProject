using AspNetCoreDemoApp.Models;
using AspNetCoreDemoApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AspNetCoreDemoApp.Controllers
{
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private IReviewService reviewService;

        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        /// <summary>
        /// GET endpoint to retrieve a list of reviews associated with the given book ID.
        /// </summary>
        /// <param name="bookId">The ID of the book to search for.</param>
        /// <returns>The reviews linked to the book.</returns>
        [HttpGet("{bookId}")]
        public IEnumerable<Review> Get(string bookId)
        {
            return reviewService.FindAllByBookId(bookId);
        }

        /// <summary>
        /// POST endpoint to create a new review.
        /// </summary>
        /// <param name="reviewData">The reivew data to be stored.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] Review reviewData)
        {
            reviewService.Create(reviewData);
            return Ok();
        }
    }
}
