namespace DraftKings.BooksApi.E2E.Core.ContextContainers
{
    using System.Collections.Generic;
    using TechTalk.SpecFlow;

    public static class ScenarioContextExtensions
    {
        public static void AddOrUpdate(this ScenarioContext context, string key, object value)
        {
            if (value == default) return;

            if (!context.ContainsKey(key))
            {
                context.Add(key, value);
            }
            else
            {
                context[key] = value;
            }
        }

        public static T GetOrDefault<T>(this ScenarioContext context, string key)
        {
            if (context.TryGetValue(key, out T value))
            {
                return value;
            }

            return default;
        }

        public static void AddOrUpdateList<TValue>(this ScenarioContext context, string key, TValue value) where TValue : class
        {
            if (!context.ContainsKey(key))
            {
                if (value == default)
                {
                    context.Add(key, new List<TValue>());
                }
                else
                {
                    List<TValue> value2 = new() { value };
                    context.Add(key, value2);
                }
            }
            else
            {
                ((List<TValue>)context[key]).Add(value);
            }
        }
    }
}
