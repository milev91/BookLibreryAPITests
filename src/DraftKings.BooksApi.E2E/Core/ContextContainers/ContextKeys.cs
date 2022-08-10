namespace DraftKings.BooksApi.E2E.Core.ContextContainers
{
    public static class ContextKeys
    {
        public static readonly string CreateUserRequestsList = nameof(CreateUserRequestsList);
        public static readonly string DeleteUserRequestsList = nameof(DeleteUserRequestsList);
        public static readonly string LoginUserRequestsList = nameof(LoginUserRequestsList);
        public static readonly string LoginUserResponsesList = nameof(LoginUserResponsesList);

        public static readonly string CreateBookRequestsList = nameof(CreateBookRequestsList);
        public static readonly string CreateBookResponsesList = nameof(CreateBookResponsesList);
        public static readonly string GetBookByIdRequestsList = nameof(GetBookByIdRequestsList);
        public static readonly string GetBookByIdResponsesList = nameof(GetBookByIdResponsesList);
        public static readonly string DeleteBookRequestsList = nameof(DeleteBookRequestsList);
        public static readonly string DeleteBookResponsesList = nameof(DeleteBookResponsesList);
        public static readonly string UpdateBookRequestsList = nameof(UpdateBookRequestsList);
        public static readonly string UpdateBookResponsesList = nameof(UpdateBookResponsesList);

        public static readonly string CreatedAuthorsList = nameof(CreatedAuthorsList);
    }
}