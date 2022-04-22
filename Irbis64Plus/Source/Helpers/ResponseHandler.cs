using Irbis64Plus.Exceptions;
using Irbis64Plus.Internal;

namespace Irbis64Plus.Helpers;

internal static class ResponseHandler
{
    public static void CheckError(Response response) {
        if (response.Error == "0" || response.Error == "-3337") {
            return;
        }

        throw new ServerException( Errors.GetMessage(response.Error) );
    }
}