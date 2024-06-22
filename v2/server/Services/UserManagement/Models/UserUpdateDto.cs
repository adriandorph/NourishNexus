namespace server.Services.UserManagement.Models;

public record UserUpdateDto
{
    public string? Id {get; set;}
    public string? Nickname {get; set;}
    public string? Email {get; set;}
    public string? ProfilePictureBase64 {get; set;}
    
    public string? Password {get; set;}
    public string? ConfirmPassword {get; set;}
}

