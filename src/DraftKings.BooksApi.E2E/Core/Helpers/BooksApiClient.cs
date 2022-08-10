namespace DraftKings.BooksApi.E2E.Core.Helpers
{
    using System;
    using System.Threading.Tasks;
    using DraftKings.BooksApi.E2E.Core.Contracts.Authorization;
    using DraftKings.BooksApi.E2E.Core.Contracts.Books;
    using RestSharp;

    public class BooksApiClient
    {
        private readonly RestClient _client;

        public BooksApiClient(RestClient client)
        {
            _client = client;
        }

        public async Task<LoginUserResponse> LogInAsync(LoginUserRequest loginUserRequest)
        {
            var request = new RestRequest("Authentication/login", Method.Post);
            request.AddBody(loginUserRequest);
            var response = await _client.ExecuteAsync<LoginUserResponse>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return response.Data;

            //if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            //    throw new Exception(response.ErrorMessage);

            return null;
        }

        public Task CreateUserAsync(CreateUserRequest createUserRequest)
        {
            var request = new RestRequest("Authentication/create-user", Method.Post);
            request.AddBody(createUserRequest);
            return _client.ExecuteAsync(request);
        }

        public Task DeleteUserAsync(DeleteUserRequest deleteUserRequest)
        {
            var request = new RestRequest("Authentication/create-user", Method.Post);
            request.AddBody(deleteUserRequest);
            return _client.ExecuteAsync(request);
        }

        public async Task<CreateBookResponse> CreateBookAsync(CreateBookRequest createBookRequest, string token)
        {
            var request = new RestRequest("Books", Method.Post);
            request.AddOrUpdateHeader("Authorization", string.Format("Bearer {0}", token));
            request.AddBody(createBookRequest);
            var response = await _client.ExecuteAsync<CreateBookResponse>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK && response.Data.BookId > 0)
                return response.Data;

            //if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            //    throw new Exception(response.ErrorMessage);

            return null;
        }

        public Task DeleteBookAsync(DeleteBookRequest deleteBookRequest, string token)
        {
            var request = new RestRequest("Books", Method.Post);
            request.AddOrUpdateHeader("Authorization", string.Format("Bearer {0}", token));
            request.AddBody(deleteBookRequest);
            return _client.ExecuteAsync(request);
        }

        public async Task<Book> GetBookAsync(GetBookByIdRequest getBookByIdRequest, string token)
        {
            var request = new RestRequest($"Books/{getBookByIdRequest.BookId}", Method.Get);
            request.AddOrUpdateHeader("Authorization", string.Format("Bearer {0}", token));
            var response = await _client.ExecuteAsync<Book>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return response.Data;

            //if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            //    throw new Exception(response.ErrorMessage);

            return null;
        }

        public async Task<UpdateBookResponse> UpdateBookAsync(UpdateBookRequest updateBookRequest, string token)
        {
            var request = new RestRequest("Books", Method.Put);
            request.AddOrUpdateHeader("Authorization", string.Format("Bearer {0}", token));
            request.AddBody(updateBookRequest);
            var response = await _client.ExecuteAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return new UpdateBookResponse { BookId = updateBookRequest.BookToUpdate.Id };

            //if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            //    throw new Exception(response.ErrorMessage);

            return null;
        }

        public async Task<GetBooksResponse> GetBooksAsync(GetBooksRequest getBooksRequest, string token)
        {
            var request = new RestRequest("Books", Method.Get);
            request.AddOrUpdateHeader("Authorization", string.Format("Bearer {0}", token));
            request.AddOrUpdateParameter(nameof(getBooksRequest.AuthorFirstName), getBooksRequest.AuthorFirstName);
            request.AddOrUpdateParameter(nameof(getBooksRequest.AuthorLastName), getBooksRequest.AuthorLastName);
            request.AddOrUpdateParameter(nameof(getBooksRequest.PageNumber), getBooksRequest.PageNumber);
            var response = await _client.ExecuteAsync<GetBooksResponse>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return response.Data;

            //if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            //    throw new Exception(response.ErrorMessage);

            return null;
        }
    }
}
