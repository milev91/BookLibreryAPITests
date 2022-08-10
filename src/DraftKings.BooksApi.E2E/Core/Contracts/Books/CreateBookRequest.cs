namespace DraftKings.BooksApi.E2E.Core.Contracts.Books
{
    using System;

    public class CreateBookRequest
    {
        public string Title { get; set; }
        public Author Author { get; set; }
        public string Publisher { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
