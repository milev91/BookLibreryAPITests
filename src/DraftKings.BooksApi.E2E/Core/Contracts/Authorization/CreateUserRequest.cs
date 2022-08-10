namespace DraftKings.BooksApi.E2E.Core.Contracts.Authorization
{
    public class CreateUserRequest
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
