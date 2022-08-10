namespace DraftKings.BooksApi.E2E.Core.Contracts.Authorization
{
    using System;

    public class LoginUserResponse
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
