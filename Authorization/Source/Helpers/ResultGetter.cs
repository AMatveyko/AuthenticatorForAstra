using UserServiceInterface.DTO;

namespace Authorization.Source.Helpers;

internal static class ResultGetter
{
    public static Result Get(Action action) {
        try {
            action();
            return Result.Ok();
        }
        catch (Exception e) {
            return Result.Fail(e.Message);
        }
    }

    public static Result<T> Get<T>(Func<T> func) where T : class {
        try {
            var result = func();
            return Result<T>.Ok(result);
        }
        catch (Exception e) {
            return Result<T>.Fail(e.Message);
        }
    }
}