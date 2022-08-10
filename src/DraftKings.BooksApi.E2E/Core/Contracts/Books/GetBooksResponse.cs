namespace DraftKings.BooksApi.E2E.Core.Contracts.Books
{
    using System.Collections.Generic;

    public class GetBooksResponse
    {
        public List<Book> Books { get; set; }

        public string PageInfo { get; set; }
    }
}
