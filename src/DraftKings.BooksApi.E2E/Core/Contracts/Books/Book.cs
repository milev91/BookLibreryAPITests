namespace DraftKings.BooksApi.E2E.Core.Contracts.Books
{
    using System;

    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Author Author { get; set; }

        public string Publisher { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}