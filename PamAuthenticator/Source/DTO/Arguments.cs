using System.ComponentModel.DataAnnotations;

namespace PamAuthenticator.DTO;

internal record Arguments : IValidatableObject
{
    public string AuthenticationType { get; init; }
    public string PamType { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {

        var errors = new List<ValidationResult>();
        
        if (MyConstants.PamFunctions.Contains(PamType) == false) {
            errors.Add(new ValidationResult($"Unsupported pam function {PamType}"));
        }
        
        CheckForNull(errors, Username, nameof(Username));
        CheckForNull(errors, Password, nameof(Password));
        
        return errors;
    }

    private static void CheckForNull(List<ValidationResult> errors, string value, string name) {
        if (string.IsNullOrWhiteSpace(value)) {
            errors.Add(new ValidationResult($"{name} cannot be null or empty"));
        }
    }
    
}