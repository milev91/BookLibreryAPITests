namespace DraftKings.BooksApi.E2E.Core.Support
{
    using BoDi;
    using DraftKings.BooksApi.E2E.Core.Config;
    using DraftKings.BooksApi.E2E.Core.Helpers;
    using Microsoft.Extensions.Configuration;
    using RestSharp;
    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;
    using TechTalk.SpecFlow.Assist.ValueRetrievers;

    [Binding]
    public class GlobalHooks
    {
        [BeforeTestRun]
        public static void BeforeTestRun(IObjectContainer objectContainer)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("specflowConfig.json")
                .Build()
                .Get<BaseConfig>();

            var client = new RestClient(config.BooksApiBaseUrl);

            objectContainer.RegisterFactoryAs(e => new BooksApiClient(client));
            Service.Instance.ValueRetrievers.Unregister<StringValueRetriever>();
            Service.Instance.ValueRetrievers.Register(new StringValueRetriver());
        }
    }
}
