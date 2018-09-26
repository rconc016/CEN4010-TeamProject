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
	}
}