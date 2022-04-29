using System.Security.Authentication;
using AccountManager.Stuff;
using Authorization.Source.Helpers;

namespace AccountManager.Middleware;

public sealed class PasswordChecker
{
    private readonly RequestDelegate _next;
    private readonly ApplicationPassword _correctPassword;
    private readonly SignatureValidator _validator;

    public PasswordChecker(
        RequestDelegate next,
        ApplicationPassword correctPassword,
        SignatureValidator validator) =>
        (_next, _correctPassword, _validator) =
        (next, correctPassword, validator);

    public async Task InvokeAsync(HttpContext context) {
        try {
            Check(context);
        }
        catch (Exception) {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }
        await _next(context);
    }

    private void Check(HttpContext context) {
        var (signature, timestamp) = GetHeaders(context);
        CheckPassword(signature, timestamp );
    }
    
    private void CheckPassword(string signature, string timestamp ) {
        _validator.Check(signature, _correctPassword.Value, timestamp);
    }
    
    private static (string, string) GetHeaders(HttpContext context) {
        var signature = GetHeaderValue("authorization", context);
        var timestamp = GetHeaderValue("timestamp", context);
        return (signature, timestamp);
    }

    private static string GetHeaderValue(string key, HttpContext context) {
        if (context.Request.Headers.TryGetValue(key, out var value)) {
            return value.ToString();
        }

        throw new AuthenticationException($"Missing header {key}");
    }
}