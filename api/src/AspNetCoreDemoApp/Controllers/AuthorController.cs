using AspNetCoreDemoApp.Models;
using AspNetCoreDemoApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreDemoApp.Controllers
{
    [Route("api/[controller]")]
	public class AuthorController : ControllerBase
	{	
		private IAuthorService authorService;

		public AuthorController(IAuthorService authorService)
		{
			this.authorService = authorService;
		}

		/// <summary>
		/// GET endpoint to retrieve a single author.
		/// </summary>
		/// <param name="authorId">The ID of the author to look for.</param>
		/// <returns>The author with the corresponding ID.</returns>
		[HttpGet("{authorId}")]
		public ActionResult<Author> Get(string authorId)
		{
			Author author = authorService.FindById(authorId);
			if (author == null)
			{
				return NotFound();
			}

			return author;
		}
	}
}