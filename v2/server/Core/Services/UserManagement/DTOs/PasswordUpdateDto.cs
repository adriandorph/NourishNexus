namespace server.Core.Services.UserManagement.DTOs;
public record PasswordUpdateDto
{
    [Required, EmailAddress]
    public string? Email {get; set;}
    [Required, MinLength(6, ErrorMessage = "Password needs to be of minimum 6 characters")]
    public string? Password {get; set;}
    [Required, Compare("Password")]
    public string? ConfirmPassword {get; set;}
}