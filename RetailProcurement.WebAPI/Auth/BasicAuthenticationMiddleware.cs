using System.Net;
using System.Net.Http.Headers;
using System.Text;

public class BasicAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private const string AuthorizationHeaderName = "Authorization";

    public BasicAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string authHeader = context.Request.Headers[AuthorizationHeaderName]!;

        if (authHeader != null)
        {
            var authHeaderValue = AuthenticationHeaderValue.Parse(authHeader);
            if (authHeaderValue.Scheme.Equals(AuthenticationSchemes.Basic.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeaderValue.Parameter ?? string.Empty)).Split(':', 2);
                if (credentials.Length == 2)
                {
                    var username = credentials[0];
                    var password = credentials[1];

                    // Validate the username and password here. Replace "YourUsername" and "YourPassword" with your actual username and password.
                    if (username == "admin" && password == "password")
                    {
                        await _next.Invoke(context);
                        return;
                    }
                }
            }
        }

        context.Response.StatusCode = 401; // Unauthorized
        context.Response.Headers.Append("WWW-Authenticate", $"Basic realm=\"{context.Request.Path}\"");
        await context.Response.WriteAsync("Unauthorized");
    }
}
