namespace DraftKings.BooksApi.E2E.Tests.StepsDefinitions
{
    using System.Threading.Tasks;
    using DraftKings.BooksApi.E2E.Core.ContextContainers;
    using DraftKings.BooksApi.E2E.Core.Contracts.Authorization;
    using DraftKings.BooksApi.E2E.Core.Helpers;
    using TechTalk.SpecFlow;

    [Binding]
    internal class AuthorizationFeatureSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly BooksApiClient _booksApiClient;

        public AuthorizationFeatureSteps(
            ScenarioContext scenarioContext,
            BooksApiClient booksApiClient)
        {
            _scenarioContext = scenarioContext;
            _booksApiClient = booksApiClient;
        }

        [Given(@"I register user with username ""(.*)"", email ""(.*)"" and password ""(.*)""")]
        public async Task CreateUser(string userName, string email, string password)
        {
            var createUserRequest = new CreateUserRequest
            {
                EmailAddress = email,
                UserName = userName,
                Password = password
            };

            _scenarioContext.AddOrUpdateList(ContextKeys.CreateUserRequestsList, createUserRequest);

            await _booksApiClient.CreateUserAsync(createUserRequest);
       
        }

        [Given(@"I login as ""(.*)"" with password ""(.*)""")]
        [When(@"I login as ""(.*)"" with password ""(.*)""")]
        public async Task LogInUser(string email, string password)
        {
            var loginUserRequest = new LoginUserRequest
            {
                EmailAddress = email,
                Password = password
            };

            _scenarioContext.AddOrUpdateList(ContextKeys.LoginUserRequestsList, loginUserRequest);

            var loginUserResponse = await _booksApiClient.LogInAsync(loginUserRequest);

            _scenarioContext.AddOrUpdateList(ContextKeys.LoginUserResponsesList, loginUserResponse);
        }



        [Given(@"I remove registration for user with email ""(.*)""")]
        public async Task DeleteUser(string email)
        {
            var deleteUserRequest = new DeleteUserRequest { EmailAddress = email };

            _scenarioContext.AddOrUpdateList(ContextKeys.DeleteUserRequestsList, deleteUserRequest);

            await _booksApiClient.DeleteUserAsync(deleteUserRequest);
        }
    }
}
