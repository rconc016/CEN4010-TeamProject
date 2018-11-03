using System.Collections.Generic;
using AspNetCoreDemoApp.Models;
using AspNetCoreDemoApp.Services;
using AspNetCoreDemoApp.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreDemoApp.Controllers
{
    [Route("api/[controller]")]
	public class BookController : ControllerBase
	{
		private IBookService bookService;

		public BookController(IBookService bookService)
		{
			this.bookService = bookService;
		}

        /// <summary>
        /// Returns an action result with status code 500 Internal Server Error.
        /// </summary>
        private ActionResult InternalServerError()
        {
            return StatusCode(500);
        }

		/// <summary>
		/// GET endpoint to retrieve the list of all the books.
		/// </summary>
		/// <param name="sortCommand">The sorting key and order to use.</param>
		/// <param name="filterCommand">The book property filters to apply.</param>
		/// <returns>A list of all <see href="Book">s available after filtering and sorting have been applied.</returns>
        [HttpGet]
		public IEnumerable<Book> Get(SortCommand sortCommand, BookFilterCommand filterCommand, PageCommand pageCommand)
		{
			return bookService.FindAll(sortCommand, bookService.GetFilterCommands(filterCommand), pageCommand);
		}

		/// <summary>
		/// GET endpoint to retrieve a single book.
		/// </summary>
		/// <param name="bookId">The ID of the book to look for.</param>
		/// <returns>The book with the corresponding ID.</returns>
		[HttpGet("{bookId}")]
		public ActionResult<Book> Get(string bookId)
		{
			Book book = bookService.FindById(bookId);
			if (book == null)
			{
				return NotFound();
			}

			return book;
		}

        /// <summary>
        /// POST endpoint to update a book's rating.
        /// </summary>
        /// <param name="command">Contains book ID and new rating score to add.</param>
        /// <returns>200 if the update was sucessful, 500 otherwise.</returns>
        [HttpPost("rate")]
        public ActionResult Post([FromBody] UpdateBookRatingCommand command)
        {
            bool success = bookService.UpdateRating(command.BookId, command.Rating);
            return success ? Ok() : InternalServerError();
        }
	}
}