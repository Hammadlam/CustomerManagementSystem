namespace CustomerManagementSystemUI.Data.APIUtility
{
    using System.Net.Http.Headers;

    public class ApiService
    {
        private readonly IHttpClientFactory _factory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiService(IHttpClientFactory factory, IHttpContextAccessor accessor)
        {
            _factory = factory;
            _httpContextAccessor = accessor;
        }

        public HttpClient CreateClient()
        {
            var client = _factory.CreateClient("API");

            var token = _httpContextAccessor.HttpContext?.Session.GetString("JWT");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }
    }
}
