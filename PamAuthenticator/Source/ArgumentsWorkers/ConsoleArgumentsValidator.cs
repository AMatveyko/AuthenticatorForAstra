using System.ComponentModel.DataAnnotations;

namespace PamAuthenticator.ArgumentsWorkers;

internal static class ConsoleArgumentsValidator
{
    public static void Validate(DTO.Arguments arguments) => ValidateObject(arguments);

    private static void ValidateObject(IValidatableObject obj) {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(obj);
        if (Validator.TryValidateObject(obj, context, results, true)) {
            return;
        }

        var message = string.Join(" ", results.Select( r => r.ErrorMessage));

        throw new ArgumentOutOfRangeException(message);
    }
}