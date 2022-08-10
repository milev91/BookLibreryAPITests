namespace DraftKings.BooksApi.E2E.Core.Contracts.Books
{
    public class GetBooksRequest
    {
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public int PageNumber { get; set; } = 1;
    }
}
