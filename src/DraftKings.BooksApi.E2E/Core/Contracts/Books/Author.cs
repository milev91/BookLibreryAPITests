namespace DraftKings.BooksApi.E2E.Core.Contracts.Books
{
    using System;

    public class Author
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
