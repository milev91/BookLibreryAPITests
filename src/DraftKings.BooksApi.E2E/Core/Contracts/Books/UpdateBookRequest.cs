namespace DraftKings.BooksApi.E2E.Core.Contracts.Books
{
    using System;

    public class UpdateBookRequest
    {
        public BookToUpdate BookToUpdate { get; set; }
    }

    public class BookToUpdate
    {
        public int Id { get; set; }
        public string Title { get; init; }
        public string Publisher { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
