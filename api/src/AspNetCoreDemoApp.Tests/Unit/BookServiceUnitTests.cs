using System.Collections.Generic;
using System.Threading;
using AspNetCoreDemoApp.Models;
using AspNetCoreDemoApp.Services;
using AspNetCoreDemoApp.Utils;
using AspNetCoreDemoApp.Wrappers;
using FakeItEasy;
using FluentAssertions;
using Google.Cloud.Firestore;
using Xunit;

namespace AspNetCoreDemoApp.Tests.Unit
{
    public class BookServiceUnitTests
    {
        private IFirestoreService firestoreService;
        private IFilterService filterService;
        private IQuery query;
        private BookService bookService;

        public BookServiceUnitTests()
        {
            firestoreService = A.Fake<IFirestoreService>();
            filterService = A.Fake<IFilterService>();
            query = A.Fake<IQuery>();

            bookService = new BookService(firestoreService, filterService);
        }

        [Fact]
        public void UpdateShouldCallServiceUpdate()
        {
            // Arrange
            string testId = "testId";
            Book testBook = new Book();
            A.CallTo(() => firestoreService.Update(A<string>._, A<string>._, A<object>._, A<SetOptions>._, A<CancellationToken>._));

            // Act
            bookService.Update("testId", testBook);

            // Assert
            A.CallTo(() => firestoreService.Update(A<string>._, testId, testBook, A<SetOptions>._, A<CancellationToken>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void FindByIdShouldReturnBookOrNull()
        {
            // Arrange
            Book expectedBook = new Book();
            A.CallTo(() => firestoreService.FindById<Book>(A<string>._, A<string>._)).Returns(expectedBook);

            // Act
            Book actualBook = bookService.FindById("testId");

            // Assert
            actualBook.Should().BeSameAs(expectedBook);
        }

        [Fact]
        public void FindAllShouldReturnAllBooks()
        {
            // Arrange
            IList<Book> expectedBooks = new List<Book>();
            A.CallTo(() => firestoreService.FindAll<Book>(A<string>._)).Returns(expectedBooks);

            // Act
            IList<Book> actualBooks = bookService.FindAll();

            // Assert
            actualBooks.Should().BeSameAs(expectedBooks);
        }

        [Fact]
        public void FindAllWithParamsShouldReturnAllBooks()
        {
            // Arrange
            IList<Book> expectedBooks = new List<Book>();
            A.CallTo(() => firestoreService.FindAll<Book>(A<string>._)).Returns(expectedBooks);

            // Act
            IList<Book> actualBooks = bookService.FindAll(null, null, null);

            // Assert
            actualBooks.Should().BeSameAs(expectedBooks);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("title")]
        public void FindAllWithSortCommandShouldReturnAllBooksOrExecuteQuery(string sortKey)
        {
            // Arrange
            SortCommand sortCommand = new SortCommand { SortKey = sortKey };
            IList<Book> expectedFirestoreServiceBooks = new List<Book>();
            IList<Book> expectedQueryBooks = new List<Book>();
            
            A.CallTo(() => firestoreService.FindAll<Book>(A<string>._)).Returns(expectedFirestoreServiceBooks);
            A.CallTo(() => query.Execute<Book>()).Returns(expectedQueryBooks);
            A.CallTo(() => firestoreService.OrderBy(A<string>._, A<string>._, A<SortDirection>._)).Returns(query);

            // Act
            IList<Book> actualBooks = bookService.FindAll(sortCommand, null, null);

            // Assert
            actualBooks.Should().BeSameAs(string.IsNullOrWhiteSpace(sortKey) ? expectedFirestoreServiceBooks : expectedQueryBooks);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void FindAllWithFilterCommandShouldReturnAllBooksOrExecuteQuery(int count)
        {
            // Arrange
            IList<FilterCommand> filterCommands = null;
            if (count > -1)
            {
                filterCommands = new List<FilterCommand>();

                for (int i = 0; i < count; i++)
                {
                    filterCommands.Add(new FilterCommand {});
                }
            }

            IList<Book> expectedFirestoreServiceBooks = new List<Book>();
            IList<Book> expectedQueryBooks = new List<Book>();
            
            A.CallTo(() => firestoreService.FindAll<Book>(A<string>._)).Returns(expectedFirestoreServiceBooks);
            A.CallTo(() => query.Execute<Book>()).Returns(expectedQueryBooks);
            A.CallTo(() => firestoreService.Where(A<string>._, A<string>._, A<QueryOperator>._, A<object>._)).Returns(query);

            // Act
            IList<Book> actualBooks = bookService.FindAll(null, filterCommands, null);

            // Assert
            actualBooks.Should().BeSameAs(ListUtils.IsListNullOrEmpty(filterCommands) ? expectedFirestoreServiceBooks : expectedQueryBooks);
        }

        [Theory]
        [InlineData(0, -1)]
        [InlineData(-1, 0)]
        [InlineData(-1, -1)]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        public void FindAllWithPageCommandShouldReturnAllBooksOrExecuteQuery(int limit, int offset)
        {
            // Arrange
            PageCommand pageCommand = new PageCommand { Limit = limit, Offset = offset };
            IList<Book> expectedFirestoreServiceBooks = new List<Book>();
            IList<Book> expectedQueryBooks = new List<Book>();
            
            A.CallTo(() => firestoreService.FindAll<Book>(A<string>._)).Returns(expectedFirestoreServiceBooks);
            A.CallTo(() => query.Limit(A<int>._)).Returns(query);
            A.CallTo(() => query.Offset(A<int>._)).Returns(query);
            A.CallTo(() => query.Execute<Book>()).Returns(expectedQueryBooks);
            A.CallTo(() => firestoreService.Limit(A<string>._, A<int>._)).Returns(query);
            A.CallTo(() => firestoreService.Offset(A<string>._, A<int>._)).Returns(query);

            // Act
            IList<Book> actualBooks = bookService.FindAll(null, null, pageCommand);

            // Assert
            actualBooks.Should().BeSameAs((limit > 0 || offset > 0) ? expectedQueryBooks : expectedFirestoreServiceBooks);
        }

        [Fact]
        public void FindAllWithSortAndFilterCommandShouldReturnExecuteQuery()
        {
            // Arrange
            SortCommand sortCommand = new SortCommand { SortKey = "test" };
            IList<FilterCommand> filterCommands = new List<FilterCommand>();
            IList<Book> expectedBooks = new List<Book>();
            
            A.CallTo(() => firestoreService.OrderBy(A<string>._, A<string>._, A<SortDirection>._)).Returns(query);
            A.CallTo(() => query.Where(A<string>._, A<QueryOperator>._, A<object>._)).Returns(query);
            A.CallTo(() => query.Execute<Book>()).Returns(expectedBooks);

            // Act
            IList<Book> actualBooks = bookService.FindAll(sortCommand, filterCommands, null);

            // Assert
            actualBooks.Should().BeSameAs(expectedBooks);
        }

        [Fact]
        public void FindAllWithSortAndPageCommandShouldReturnExecuteQuery()
        {
            // Arrange
            SortCommand sortCommand = new SortCommand { SortKey = "testKey" };
            PageCommand pageCommand = new PageCommand { Limit = 1, Offset = 1 };
            IList<Book> expectedBooks = new List<Book>();
            
            A.CallTo(() => firestoreService.OrderBy(A<string>._, A<string>._, A<SortDirection>._)).Returns(query);
            A.CallTo(() => query.Limit(A<int>._)).Returns(query);
            A.CallTo(() => query.Offset(A<int>._)).Returns(query);
            A.CallTo(() => query.Execute<Book>()).Returns(expectedBooks);

            // Act
            IList<Book> actualBooks = bookService.FindAll(sortCommand, null, pageCommand);

            // Assert
            actualBooks.Should().BeSameAs(expectedBooks);
        }
    }
}