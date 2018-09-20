using System;
using System.Collections.Generic;
using AspNetCoreDemoApp.Models;
using AspNetCoreDemoApp.Services;
using Microsoft.AspNetCore.Http.Extensions;
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
		/// <returns>A list of all <see href="Book">s available.</returns>
        [HttpGet]
		public IEnumerable<Book> Get()
		{
			return bookService.FindAll();
		}

		/// <summary>
		/// GET endpoint to retrieve a single book.
		/// </summary>
		/// <param name="bookId"></param>
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