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
    public class FirestoreServiceUnitTests
    {
        private IFirestoreDb firestoreDb;
        private ICollectionReference collectionRef;
        private IDocumentReference documentRef;
        private IQuery query;
        private FirestoreService firestoreService;

        public FirestoreServiceUnitTests()
        {
            firestoreDb = A.Fake<IFirestoreDb>();
            collectionRef = A.Fake<ICollectionReference>();
            documentRef = A.Fake<IDocumentReference>();
            query = A.Fake<IQuery>();
            firestoreService = new FirestoreService(firestoreDb);
        }

        [Fact]
        public void CreateShouldCreateDocumentWithAutoId()
        {   
            // Arrange
            A.CallTo(() => firestoreDb.Collection(A<string>._)).Returns(collectionRef);
            A.CallTo(() => collectionRef.Document(A<string>._)).Returns(documentRef);

            // Act
            firestoreService.Create("testCollectionId", "testData");

            // Assert
            A.CallTo(() => firestoreDb.Collection(A<string>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => collectionRef.Document(null)).MustHaveHappenedOnceExactly();
            A.CallTo(() => documentRef.SetAsync(A<object>._, A<SetOptions>._, A<CancellationToken>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void CreateShouldCreateDocumentWithGivenId()
        {   
            // Arrange
            string documentId = "testDocumentId";

            A.CallTo(() => firestoreDb.Collection(A<string>._)).Returns(collectionRef);
            A.CallTo(() => collectionRef.Document(A<string>._)).Returns(documentRef);

            // Act
            firestoreService.CreateWithId("testCollectionId", documentId, "testData");

            // Assert
            A.CallTo(() => firestoreDb.Collection(A<string>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => collectionRef.Document(documentId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => documentRef.SetAsync(A<object>._, A<SetOptions>._, A<CancellationToken>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void UpdateShouldUpdateDocumentWithGivenId()
        {   
            // Arrange
            string documentId = "testDocumentId";

            A.CallTo(() => firestoreDb.Collection(A<string>._)).Returns(collectionRef);
            A.CallTo(() => collectionRef.Document(A<string>._)).Returns(documentRef);

            // Act
            firestoreService.Update("testCollectionId", documentId, "testData");

            // Assert
            A.CallTo(() => firestoreDb.Collection(A<string>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => collectionRef.Document(documentId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => documentRef.SetAsync(A<object>._, A<SetOptions>._, A<CancellationToken>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void FindAllShouldReturnAllItemsInCollection()
        {   
            // Arrange
            string collectionId = "testCollectionId";
            IList<Book> expectedBooks = new List<Book>();

            A.CallTo(() => firestoreDb.Collection(A<string>._)).Returns(collectionRef);
            A.CallTo(() => collectionRef.GetSnapshotAsync<Book>()).Returns(expectedBooks);

            // Act
            IList<Book> actualBooks = firestoreService.FindAll<Book>(collectionId);

            // Assert
            actualBooks.Should().BeSameAs(expectedBooks);
        }

        [Fact]
        public void FindByIdShouldReturnItemWithGivenId()
        {   
            // Arrange
            string collectionId = "testCollectionId";
            string documentId = "testDocumentId";
            Book expectedBook = new Book();

            A.CallTo(() => firestoreDb.Collection(A<string>._)).Returns(collectionRef);
            A.CallTo(() => collectionRef.Document(A<string>._)).Returns(documentRef);
            A.CallTo(() => documentRef.GetSnapshotAsync<Book>()).Returns(expectedBook);

            // Act
            Book actualBook = firestoreService.FindById<Book>(collectionId, documentId);

            // Assert
            actualBook.Should().BeSameAs(expectedBook);
        }

        [Fact]
        public void WhereShouldReturnQuery()
        {   
            // Arrange
            string collectionId = "testCollectionId";
            string data = "testData";

            A.CallTo(() => firestoreDb.Collection(A<string>._)).Returns(collectionRef);
            A.CallTo(() => collectionRef.Where(A<string>._, A<QueryOperator>._, A<object>._)).Returns(query);

            // Act
            IQuery actualQuery = firestoreService.Where(collectionId, "title", QueryOperator.Contains, data);

            // Assert
            actualQuery.Should().BeSameAs(query);
        }

        [Fact]
        public void OrderByShouldReturnQuery()
        {   
            // Arrange
            string collectionId = "testCollectionId";

            A.CallTo(() => firestoreDb.Collection(A<string>._)).Returns(collectionRef);
            A.CallTo(() => collectionRef.OrderBy(A<string>._, A<SortDirection>._)).Returns(query);

            // Act
            IQuery actualQuery = firestoreService.OrderBy(collectionId, "title", SortDirection.Asc);

            // Assert
            actualQuery.Should().BeSameAs(query);
        }

        [Fact]
        public void LimitShouldReturnQuery()
        {   
            // Arrange
            string collectionId = "testCollectionId";

            A.CallTo(() => firestoreDb.Collection(A<string>._)).Returns(collectionRef);
            A.CallTo(() => collectionRef.Limit(A<int>._)).Returns(query);

            // Act
            IQuery actualQuery = firestoreService.Limit(collectionId, 1);

            // Assert
            actualQuery.Should().BeSameAs(query);
        }

        [Fact]
        public void OffsetShouldReturnQuery()
        {   
            // Arrange
            string collectionId = "testCollectionId";

            A.CallTo(() => firestoreDb.Collection(A<string>._)).Returns(collectionRef);
            A.CallTo(() => collectionRef.Offset(A<int>._)).Returns(query);

            // Act
            IQuery actualQuery = firestoreService.Offset(collectionId, 1);

            // Assert
            actualQuery.Should().BeSameAs(query);
        }
    }
}