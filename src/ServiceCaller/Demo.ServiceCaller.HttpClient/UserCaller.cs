using Masa.Contrib.Service.Caller.HttpClient;

namespace Demo.ServiceCaller.HttpClient
{
    public class UserCaller : HttpClientCallerBase
    {
        protected override string Prefix { get; set; } = "api/users";

        protected override string BaseAddress { get; set; } = "https://backapi";

        public async Task<object?> GetAsync(int id)
        {
            return await Caller.GetAsync<object>($"{id}");
        }
    }
}
