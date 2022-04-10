using System.ComponentModel.DataAnnotations;

namespace PamAuthenticator.DTO;

internal record Arguments : IValidatableObject
{
    public string Type { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {

        var errors = new List<ValidationResult>();
        
        if (MyConstants.PamFunctions.Contains(Type) == false) {
            errors.Add(new ValidationResult($"Unsupported pam function {Type}"));
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