using System.Net.Http.Headers;

namespace CustomerManagementSystemUI.Data.JWT_Handler
{
    public class JwtHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContext;

        public JwtHandler(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _httpContext.HttpContext.Session.GetString("JWT");

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }   

            return await base.SendAsync(request, cancellationToken);
        }
    }
    
}
