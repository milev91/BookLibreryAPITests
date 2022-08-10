namespace DraftKings.BooksApi.E2E.Tests.StepsDefinitions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DraftKings.BooksApi.E2E.Core.ContextContainers;
    using DraftKings.BooksApi.E2E.Core.Contracts.Authorization;
    using DraftKings.BooksApi.E2E.Core.Contracts.Books;
    using DraftKings.BooksApi.E2E.Core.Helpers;
    using FluentAssertions;
    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;

    [Binding]
    public class BooksFeatureSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly BooksApiClient _booksApiClient;

        public BooksFeatureSteps(
            ScenarioContext scenarioContext,
            BooksApiClient booksClient)
        {
            _scenarioContext = scenarioContext;
            _booksApiClient = booksClient;
        }

        [When(@"I create an author")]
        [Given(@"I create an author")]
        public void CreateAuthor(Table authorTable)
        {
            var author = authorTable.CreateInstance<Author>();
            _scenarioContext.AddOrUpdateList(ContextKeys.CreatedAuthorsList, author);
        }

        [When(@"I create a book")]
        [Given(@"I create a book")]
        public async Task CreateBook(Table bookTable)
        {
            var createBookRequest = bookTable.CreateInstance<CreateBookRequest>();
            createBookRequest.Author = _scenarioContext.Get<List<Author>>(ContextKeys.CreatedAuthorsList)
                .LastOrDefault();

            var loginUserResponse = _scenarioContext.Get<List<LoginUserResponse>>(ContextKeys.LoginUserResponsesList)
                .LastOrDefault();

            _scenarioContext.AddOrUpdateList(ContextKeys.CreateBookRequestsList, createBookRequest);

            var createBookResponse = await _booksApiClient.CreateBookAsync(
                createBookRequest, 
                loginUserResponse.Token);

            _scenarioContext.AddOrUpdateList(ContextKeys.CreateBookResponsesList, createBookResponse);
        }

        [When(@"I update last created a book")]
        public async Task UpdateBook(Table bookTable)
        {
            var bookToUpdate = bookTable.CreateInstance<BookToUpdate>();

            var lastCreatedBookResponse = _scenarioContext.Get<List<CreateBookResponse>>(ContextKeys.CreateBookResponsesList)
                .LastOrDefault();

            var loginUserResponse = _scenarioContext.Get<List<LoginUserResponse>>(ContextKeys.LoginUserResponsesList)
                .LastOrDefault();

            bookToUpdate.Id = lastCreatedBookResponse.BookId;

            var updateBookRequest = new UpdateBookRequest { BookToUpdate = bookToUpdate };

            _scenarioContext.AddOrUpdateList(ContextKeys.UpdateBookRequestsList, updateBookRequest);

            var updateBookResponse = await _booksApiClient.UpdateBookAsync(
                updateBookRequest, 
                loginUserResponse.Token);

            _scenarioContext.AddOrUpdateList(ContextKeys.UpdateBookResponsesList, updateBookResponse);
        }


        [When(@"I search for last created book")]
        public async Task GetBook()
        {
            var loginUserResponse = _scenarioContext.Get<List<LoginUserResponse>>(ContextKeys.LoginUserResponsesList)
                .LastOrDefault();

            var lastCreatedBookId = _scenarioContext
                .Get<List<CreateBookResponse>>(ContextKeys.CreateBookResponsesList)
                .Last().BookId;

            var getBookByIdRequest = new GetBookByIdRequest { BookId = lastCreatedBookId };

            _scenarioContext.AddOrUpdateList(ContextKeys.GetBookByIdRequestsList, getBookByIdRequest);

            var getBookByIdResponse = await _booksApiClient.GetBookAsync(
                getBookByIdRequest,
                loginUserResponse.Token);

            _scenarioContext.AddOrUpdateList(ContextKeys.GetBookByIdResponsesList, getBookByIdResponse);
        }

        [When(@"I search for non existing book")]
        public async Task GetNonExistingBook()
        {
            var loginUserResponse = _scenarioContext.Get<List<LoginUserResponse>>(ContextKeys.LoginUserResponsesList)
                .LastOrDefault();

            var nonExistingBookId = _scenarioContext
                .Get<List<CreateBookResponse>>(ContextKeys.CreateBookResponsesList)
                .Last().BookId + 1;

            var getBookByIdRequest = new GetBookByIdRequest { BookId = nonExistingBookId };

            _scenarioContext.AddOrUpdateList(ContextKeys.GetBookByIdRequestsList, getBookByIdRequest);

            var getBookByIdResponse = await _booksApiClient.GetBookAsync(
                getBookByIdRequest,
                loginUserResponse.Token);

            _scenarioContext.AddOrUpdateList(ContextKeys.GetBookByIdResponsesList, getBookByIdResponse);
        }

        [Then(@"I verify that the book was retrieved")]
        public void VerifyBookHasBeenRetrieved(Table bookTable)
        {
            var getBookByIdRequests = _scenarioContext
                .GetOrDefault<List<GetBookByIdRequest>>(ContextKeys.GetBookByIdRequestsList)
                .Select(r => r.BookId);

            var getBookByIdResponses = _scenarioContext
                .GetOrDefault<List<Book>>(ContextKeys.GetBookByIdResponsesList);

            getBookByIdRequests.Should().BeEquivalentTo(
                getBookByIdResponses.Select(r => r.Id));

            getBookByIdResponses.Should().ContainEquivalentOf(
                bookTable.CreateInstance<Book>(),
                options => options.Excluding(book => book.Id).Excluding(book => book.Author));
        }

        [Then(@"I verify that the book was not retrieved")]
        public void VerifyBookHasNotBeenRetrieved()
        {
            var getBookByIdRequests = _scenarioContext
                .Get<List<GetBookByIdRequest>>(ContextKeys.GetBookByIdRequestsList)
                .Select(r => r.BookId);

            var getBookByIdResponses = _scenarioContext
                .Get<List<Book>>(ContextKeys.GetBookByIdResponsesList);

            getBookByIdRequests.Should().NotBeEquivalentTo(
                getBookByIdResponses.Select(r => r.Id));
        }

        [Then(@"I verify that the book was created")]
        public void VerifyBookHasBeenCreated()
        {
            var createBookRequests = _scenarioContext
                .Get<List<CreateBookRequest>>(ContextKeys.CreateBookRequestsList);

            var createBookResponsesList = _scenarioContext
                .Get<List<CreateBookResponse>>(ContextKeys.CreateBookResponsesList);

            createBookRequests.Count.Should().Be(createBookResponsesList.Count);
        }

        [Then(@"I verify that the book was not created")]
        public void VerifyBookHasNotBeenCreated()
        {
            var createBookRequests = _scenarioContext
                .Get<List<CreateBookRequest>>(ContextKeys.CreateBookRequestsList);

            var createBookResponsesList = _scenarioContext
                .Get<List<CreateBookResponse>>(ContextKeys.CreateBookResponsesList)
                .ToList();

            createBookRequests.Count.Should().NotBe(createBookResponsesList.Count);
        }

        [Then(@"I verify that the book was updated")]
        public void VerifyBookHasBeenUpdated()
        {
            var createBookRequests = _scenarioContext
                .Get<List<UpdateBookRequest>>(ContextKeys.UpdateBookRequestsList);

            var createBookResponsesList = _scenarioContext
                .Get<List<UpdateBookResponse>>(ContextKeys.UpdateBookResponsesList);

            createBookRequests.Count.Should().Be(createBookResponsesList.Count);
        }

        [Then(@"I verify that the book was not updated")]
        public void ThenIVerifyThatTheBookWasNotUpdated()
        {
            var createBookRequests = _scenarioContext
                .Get<List<UpdateBookRequest>>(ContextKeys.UpdateBookRequestsList);

            var createBookResponsesList = _scenarioContext
                .Get<List<UpdateBookResponse>>(ContextKeys.UpdateBookResponsesList);

            createBookRequests.Count.Should().NotBe(createBookResponsesList.Count);
        }

    }
}
