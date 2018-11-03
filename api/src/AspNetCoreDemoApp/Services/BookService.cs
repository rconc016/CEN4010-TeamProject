using System.Collections.Generic;
using AspNetCoreDemoApp.Models;
using AspNetCoreDemoApp.Utils;
using AspNetCoreDemoApp.Wrappers;

namespace AspNetCoreDemoApp.Services
{
    public class BookService : IBookService
    {
        private const string CollectionId = "book";

		private IFirestoreService firestoreService;
        private IFilterService filterService;

		public BookService(IFirestoreService firestoreService, IFilterService filterService)
		{
			this.firestoreService = firestoreService;
            this.filterService = filterService;
		}

        public void Update(string bookId, Book bookData)
        {
            firestoreService.Update(CollectionId, bookId, bookData);
        }

        public IList<Book> FindAll()
        {
            return firestoreService.FindAll<Book>(CollectionId);
        }

        public IList<Book> FindAll(SortCommand sortCommand = null, IList<FilterCommand> filterCommands = null, PageCommand pageCommand = null)        
        {
            IQuery query = null;

            if (!string.IsNullOrWhiteSpace(sortCommand?.SortKey))
            {
                query = ApplySortCommand(sortCommand, query);
            }

            if (!ListUtils.IsListNullOrEmpty(filterCommands))
            {
                query = ApplyFilterCommands(filterCommands, query);
            }

            if (pageCommand?.Limit > 0 || pageCommand?.Offset > 0)
            {
                query = ApplyPageCommand(pageCommand, query);
            }

            if (query != null)
            {
                return query.Execute<Book>();
            }
            
            return FindAll();
        }

        public Book FindById(string bookId)
        {
            return firestoreService.FindById<Book>(CollectionId, bookId);
        }

        /// <summary>
        /// Creates a sorting query.
        /// </summary>
        /// <param name="sortCommand">The sort command to use.</param>
        /// <param name="query">Optional query to append to.</param>
        /// <returns>A query with the sorting command applied.</returns>
        private IQuery ApplySortCommand(SortCommand sortCommand, IQuery query = null)
        {
            return (query == null) ? firestoreService.OrderBy(CollectionId, sortCommand.SortKey, sortCommand.SortBy) 
            : query.OrderBy(sortCommand.SortKey, sortCommand.SortBy);
        }

        /// <summary>
        /// Creates a filter query.
        /// </summary>
        /// <param name="filterCommand">The filter command to use.</param>
        /// <param name="query">Optional query to append to.</param>
        /// <returns>A query with the filtering command applied.</returns>
        private IQuery ApplyFilterCommand(FilterCommand filterCommand, IQuery query = null)
        {
            return (query == null) ? firestoreService.Where(CollectionId, filterCommand.FilterKey, filterCommand.Operator, filterCommand.FilterValue) 
            : query.Where(filterCommand.FilterKey, filterCommand.Operator, filterCommand.FilterValue);
        }

        /// <summary>
        /// Creates a filtering query from multiple filter commands.
        /// </summary>
        /// <param name="filterCommands">The filter commands to use.</param>
        /// <param name="query">Optional query to append to.</param>
        /// <returns>A query with all the filtering commands applied.</returns>
        private IQuery ApplyFilterCommands(IList<FilterCommand> filterCommands, IQuery query = null)
        {
            foreach (FilterCommand command in filterCommands)
            {
                query = ApplyFilterCommand(command, query);
            }

            return query;
        }

        /// <summary>
        /// Creates a paging query.
        /// </summary>
        /// <param name="pageCommand">The page command to use.</param>
        /// <param name="query">Optional query to append to.</param>
        /// <returns>A query with the paging command applied.</returns>
        private IQuery ApplyPageCommand(PageCommand pageCommand, IQuery query = null)
        {
            return (query == null) ? firestoreService.Limit(CollectionId, pageCommand.Limit).Offset(pageCommand.Offset)
            : query.Limit(pageCommand.Limit).Offset(pageCommand.Offset);
        }

        public IList<FilterCommand> GetFilterCommands(BookFilterCommand command)
        {
            IList<FilterCommand> commands = new List<FilterCommand>();
            filterService.AddFilterCommand(commands, "title", QueryOperator.Contains, command.Title);
            filterService.AddFilterCommand(commands, "author", QueryOperator.Contains, command.Author);
            filterService.AddFilterCommand(commands, "price", QueryOperator.GreaterThanOrEqualTo, command.MinPrice);
            filterService.AddFilterCommand(commands, "price", QueryOperator.LessThanOrEqualTo, command.MaxPrice);
            filterService.AddFilterCommand(commands, "rating", QueryOperator.GreaterThanOrEqualTo, command.Rating);
            filterService.AddFilterCommand(commands, "releaseDate", QueryOperator.GreaterThanOrEqualTo, command.MinReleaseDate);
            filterService.AddFilterCommand(commands, "releaseDate", QueryOperator.LessThanOrEqualTo, command.MaxReleaseDate);
            filterService.AddFilterCommand(commands, "genre", QueryOperator.Contains, command.Genre);
            filterService.AddFilterCommand(commands, "topSeller", QueryOperator.Equal, command.TopSeller);

            return commands;
        }

        public bool UpdateRating(string bookId, double rating)
        {
            Book book = FindById(bookId);

            if (book != null)
            {
                bool parsedRating = double.TryParse(book.Rating, out double oldRating);
                bool parsedRatingsCount = int.TryParse(book.RatingsCount, out int oldRatingsCount);

                if (!parsedRating || !parsedRatingsCount)
                {
                    return false;
                }

                if (oldRatingsCount == 0)
                {
                    book.Rating = rating.ToString();
                    book.RatingsCount = 1.ToString();
                }

                else
                {
                    int newRatingsCount = oldRatingsCount + 1;
                    double newRating = ((oldRating * oldRatingsCount) + rating) / newRatingsCount;

                    book.Rating = newRating.ToString();
                    book.RatingsCount = newRatingsCount.ToString();
                }

                Update(bookId, book);

                return true;
            }

            return false;
        }
    }
}