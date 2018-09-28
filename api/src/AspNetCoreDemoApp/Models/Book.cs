using System;
using System.Collections.Genreric;
using System.Linq;
using AspNetCoreDemoApp.Binders;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreDemoApp.Models
{
    [FirestoreData]
    public class Book : IFirestoreDocumentModel
    {
        public string Id { get; set; }

        [FirestoreProperty(name: "title")]
        public string Title { get; set; }

        [FirestoreProperty(name: "author")]
        public string Author { get; set; }

        [FirestoreProperty(name: "price")]
        public string Price { get; set; }

        [FirestoreProperty(name: "rating")]
        public string Rating { get; set; }

        [FirestoreProperty(name: "releaseDate")]
        public DateTime ReleaseDate { get; set; }

        [FirestoreProperty(name: "genre")]
        public string Genre { get; set; }

        [FirestoreProperty(name: "topSeller")]
        public bool TopSeller { get; set; }
    }

    public class BookView
    {
        public string Id {get; get; }

        [FirestoreProperty(name: "title")]
        public string Title { get; set; }

        [FirestoreProperty(name: "author")]
        public string Author { get; set; }

        [FirestoreProperty(name: "price")]
        public string Price { get; set; }

        [FirestoreProperty(name: "rating")]
        public string Rating { get; set; }

        [FirestoreProperty(name: "releaseDate")]
        public DateTime ReleaseDate { get; set; }

        [FirestoreProperty(name: "genre")]
        public string Genre { get; set; }

        [FirestoreProperty(name: "topSeller")]
        public bool TopSeller { get; set; }
    }

    public class Query
    {
        public string query {set; get; }
        public int? type { set; get; }
    }

    public class FilteredView
    {
        public IEnumerable<BookView> FilterItems {get; set; }
        public IEnumerable<BookView> Items {get; set; }
        public List<Query> Queries { set; get; }
        public string SortName { set; get; }
        public Pager Pager { set; get; }
        public int ItemsPerPage { set; get; }
    }

    public class SearchViewModel
    {
        public IEnumerable<BookView> Items {get; set; }
        public Query query { set; get; }
        public string SortName { set; get; }
        public Pager Pager { set; get; }
        public int ItemsPerPage { set; get; }
        public int totalCount { set; get; }
    }

    public class Pager
    {
         public Pager(int totalItems, int? page, int pageSize)
        {
            // calculate total, start and end pages
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }

        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
    }

}