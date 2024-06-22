namespace server.Core.Services.UserManagement.DTOs;
public record PasswordUpdateDTO
{
    [Required, EmailAddress]
    public string? Email {get; set;}

    [Required]
    public string? OldPassword {get; set;}
    [Required, MinLength(6, ErrorMessage = "Password needs to be of minimum 6 characters")]
    public string? NewPassword {get; set;}
    [Required, Compare("NewPassword")]
    public string? ConfirmNewPassword {get; set;}
}