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

        public IList<Book> FindAll(SortCommand sortCommand = null, IList<FilterCommand> filterCommands = null)        
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

        private IQuery ApplySortCommand(SortCommand sortCommand, IQuery query = null)
        {
            return (query == null) ? firestoreService.OrderBy(CollectionId, sortCommand.SortKey, sortCommand.SortBy) 
            : query.OrderBy(sortCommand.SortKey, sortCommand.SortBy);
        }

        private IQuery ApplyFilterCommand(FilterCommand filterCommand, IQuery query = null)
        {
            return (query == null) ? firestoreService.Where(CollectionId, filterCommand.FilterKey, filterCommand.Operator, filterCommand.FilterValue) 
            : query.Where(filterCommand.FilterKey, filterCommand.Operator, filterCommand.FilterValue);
        }

        private IQuery ApplyFilterCommands(IList<FilterCommand> filterCommands, IQuery query = null)
        {
            foreach (FilterCommand command in filterCommands)
            {
                query = ApplyFilterCommand(command, query);
            }

            return query;
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

            return commands;
        }
    }
}